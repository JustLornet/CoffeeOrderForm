import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import "./TextField.css";
import { Input, FormFeedback, InputGroup } from "reactstrap";
import PropTypes from "prop-types";
import "bootstrap/dist/css/bootstrap.min.css";

// типы значения для поля
const validationType = {
	INTEGER: "integer",
	POSITIVE_INTEGER: "positiveInteger",
	TEXT: "text",
};

const TextField = ({
	// общее хранилище redux
	state,
	// текущий reducer - для возможности использовния компонента
	// через redux в любом месте
	reducer,
	// имя свойства, которое смотрим
	propertyName,
	style,
	inputStyle,
	inputClassName,
	// должно ли поле быть заполненным
	isRequired = false,
	// валидация значения согласно заданному типу
	// хоть у input есть prop inputMode, валидация
	// выполнена вручну для большей гибкости и контроля
	typeOfValue = validationType.TEXT,
	// блокировка поля, true - заблокаировано
	disabled,
	// размер
	size = "",
	wrapped = false,
	placeholder,
	// true - включена проверка валидации
	validCheck = true,
	// наличие feedback под формой
	feedBack = true,
	// очищает значение, если поле стало заблокаиовано
	clearOnDisable = false,
	// использование значения из redux вместо локального state
	useReduxValues = false,
	updateReduxValue = f => f,
	// для возможности изменения способа задания значения из родительского компонента
	customOnChange,
}) => {
	// для заполнения уникальных полей
	const keyFiller = Array.isArray(propertyName ?? reducer)
		? `${reducer}-textField-${propertyName.slice(-1)}`
		: propertyName;
	let initValue = null;
	try {
		if (reducer && propertyName) {
			initValue = state[reducer].instance;
			if (Array.isArray(propertyName)) {
				propertyName.forEach(pathPart => {
					initValue = initValue[pathPart];
				});
			} else {
				initValue = initValue[propertyName];
			}
		}
	} catch {
		// ignore
		// TODO: далее добавить обработку
	}

	useEffect(() => {
		if (disabled && clearOnDisable) {
			handleChange(null);
		}
	}, [disabled]);

	useEffect(() => {
		setIsValid(checkIsValid(localStateValue));
		setIsInvalid(checkIsInvalid(localStateValue));
	}, [isRequired]);

	useEffect(() => {
		// в случае использования значений из redux
		if (useReduxValues && localStateValue != initValue) {
			handleChange(initValue);
		}
	}, [initValue]);

	/**
	 * Обработчик изменений в redux
	 */
	const updateReduxValueInner = newValue => {
		updateReduxValue(reducer, propertyName, newValue);
	};

	/**
	 * проверка на то, валидно ли данное значение
	 * true - если валидно
	 */
	const checkIsValid = valueToCheck => {
		if (!disabled) {
			switch (typeOfValue) {
				case validationType.TEXT:
					return valueToCheck ? valueToCheck.length > 0 : false;
				case validationType.INTEGER:
					if (typeof valueToCheck === "string" && valueToCheck.length === 0) {
						return false;
					} else if (typeof valueToCheck === "number") {
						return true;
					}

					return false;
				case validationType.POSITIVE_INTEGER:
					if (typeof valueToCheck === "string" && valueToCheck.length === 0) {
						return false;
					} else if (typeof valueToCheck == "number" && valueToCheck > 0) {
						return true;
					}
			}
		}

		return false;
	};

	/**
	 * проверка на то, невалидно ли данное значение
	 * true - если невалидно
	 */
	const checkIsInvalid = valueToCheck => {
		if (!disabled) {
			switch (typeOfValue) {
				case validationType.TEXT:
					return isRequired && (!valueToCheck || valueToCheck.toString().length == 0);
				case validationType.INTEGER:
					return (isRequired || valueToCheck !== "") && typeof valueToCheck !== "number";
				case validationType.POSITIVE_INTEGER:
					// TODO: переписать отдельными bool const, а то слишком длинно и непонятно
					return (
						((isRequired || valueToCheck !== "") && typeof valueToCheck !== "number") ||
						(typeof valueToCheck === "number" && valueToCheck <= 0)
					);
			}
		}

		return false;
	};

	// сохранение значения в локальное хранилище
	const [localStateValue, setLocalStateValue] = useState(initValue);
	// проверка на валидность поля; если валидно, поле подсвечивается зеленым
	const [isValid, setIsValid] = useState(checkIsValid(initValue));
	// проверка на невалидность поля; если невалидно, поле подсвечивается красным
	const [isInvalid, setIsInvalid] = useState(checkIsInvalid(initValue));

	const handleInnerChange = ev => {
		ev.preventDefault();
		const newValue = parseViaType(ev.target.value, typeOfValue);
		handleChange(newValue);
	};

	/**
	 * Парсит значение в зависимости от требуемого типа
	 */
	const parseViaType = (value, type) => {
		if (!disabled) {
			switch (type) {
				case validationType.TEXT:
					return value;
				case validationType.INTEGER:
					if (value.length > 0) {
						return !isNaN(value) ? parseInt(value) : localStateValue;
					} else {
						return 0;
					}
				case validationType.POSITIVE_INTEGER:
					if (value.length > 0) {
						return !isNaN(value) ? parseInt(value) : localStateValue;
					} else {
						return 0;
					}
				// доп функционал - запрещает ввод целочисленного значения меньше 1
				// TODO: потом реализовать
				// if (isNaN(value)) {
				// 	return localStateValue;
				// } else {
				// 	const intValue = parseInt(value);
				// 	return intValue > 0 ? intValue : localStateValue;
				// }
			}
		}
		console.log(`${type} - отсутствует парсинг для данного типа`);

		return null;
	};

	/**
	 * Метод, принимающий новое значение, запускает изменения состояния
	 * как хранилища redux, так и локального
	 */
	const handleChange = newValue => {
		setIsValid(checkIsValid(newValue));
		setIsInvalid(checkIsInvalid(newValue));
		setLocalStateValue(newValue);
		if (typeof customOnChange === "undefined") {
			updateReduxValueInner(newValue);
		} else if (typeof customOnChange !== "undefined") {
			customOnChange(newValue);
		} else {
			console.log("Change not handled");
		}
	};

	return (
		<React.Fragment>
			<div
				style={style}
				className="text-field"
			>
				<InputGroup className="text-field__input-group">
					<Input
						id={keyFiller}
						name={keyFiller}
						type={wrapped ? "textarea" : "text"}
						disabled={disabled}
						placeholder={placeholder ?? "Введите значение..."}
						valid={validCheck ? isValid : false}
						invalid={validCheck ? isInvalid : false}
						onChange={handleInnerChange}
						value={localStateValue ?? ""}
						bsSize={size}
						style={inputStyle}
						className={inputClassName ? inputClassName : `text-field__input`}
					/>
					{feedBack ? (
						<FormFeedback
							tooltip
							className="text-field__feedback"
						>
							{!localStateValue || localStateValue == ""
								? "Обязательное поле"
								: "Невалидное значение"}
						</FormFeedback>
					) : null}
				</InputGroup>
			</div>
		</React.Fragment>
	);
};

const mapStateToProps = state => {
	return {
		state: state,
	};
};

const mapDispatchToProps = dispatch => {
	// в actionCreator для каждого reducer должен быть реализован
	// метод updateReduxSimpleValue, принимающий имя свойства и значение
	// для реализации динамического импорта
	return {
		updateReduxValue: (reducer, propertyName, value) => {
			if (reducer && propertyName) {
				import(`../../store/actions/${reducer}`).then(actionCreator =>
					dispatch(actionCreator.updateReduxSimpleValue(propertyName, value))
				);
			}
		},
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(TextField);

TextField.propTypes = {
	state: PropTypes.object.isRequired,
	updateReduxValue: PropTypes.func.isRequired,
};
