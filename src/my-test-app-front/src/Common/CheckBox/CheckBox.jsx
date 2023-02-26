import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import "./CheckBox.css";
import { Input, Label } from "reactstrap";
import PropTypes from "prop-types";
import TextField from "../TextField/TextField";

const feature = {
	TEXT_FIELD: "textField",
};

const CheckBox = ({
	// общее хранилище redux
	state,
	// текущий reducer - для возможности использовния компонента
	// через redux в любом месте
	reducer,
	// имя свойства, которое смотрим
	propertyName,
	// текст к данному свойству
	label,
	// доп. свойство к данному компоненту
	featureType,
	// имя свойства при изменении для доп. компонента
	featurePropertyName,
	// стиль всего компонента
	style,
	// стиль для доп. компонента
	featureStyle,
	// обработка изменения из родительского компонента
	customOnChange,
	// кастомный компонент
	customFeature,
	featureOnChange,
	featurePlaceholder,
	featureTypeOfValue,
	isFeatureRequired,
	// обновление по значениям из Redux, а не локального state
	useReduxValue = false,
	updateReduxValue = f => f,
}) => {
	// для заполнения уникальных полей
	const keyFiller = Array.isArray(propertyName)
		? `${reducer}-checkBox-${propertyName.slice(-1)}`
		: propertyName;
	let initValue = false;
	let mutableValue = null;
	// парсинг начального значения из redux
	try {
		if (reducer && propertyName) {
			mutableValue = state[reducer].instance;
			if (Array.isArray(propertyName)) {
				propertyName.forEach(pathPart => {
					mutableValue = mutableValue[pathPart];
				});
			} else {
				mutableValue = mutableValue[propertyName];
			}
		}
	} catch {
		// console.log(`Property ${propertyName} not found`);
		// ignore
		// TODO: далее добавить обработку
	} finally {
		initValue = typeof mutableValue !== "undefined" && mutableValue !== null;
		// console.log(propertyName, initValue)
	}

	/**
	 * Обработчик изменений в redux
	 */
	const updateReduxValueInner = newValue => {
		updateReduxValue(reducer, propertyName, newValue);
	};

	// сохранение значения в локальное хранилище
	const [localStateValue, setLocalStateValue] = useState(initValue ?? false);

	useEffect(() => {
		// если включено отслеживание изменений redux
		if (useReduxValue && localStateValue != initValue) {
			handleChange(initValue);
		}
	}, [initValue]);

	const handleInnerChange = ev => {
		const newValue = !localStateValue;
		handleChange(newValue);
	};

	/**
	 * Метод, принимающий новое значение, запускает изменения состояния
	 * как хранилища redux, так и локального
	 */
	const handleChange = newValue => {
		setLocalStateValue(newValue);
		if (typeof customOnChange === "undefined") {
			updateReduxValueInner(newValue);
		} else if (customOnChange == "none") {
			// ignore
		} else if (typeof customOnChange !== "undefined") {
			customOnChange(newValue);
		} else {
			console.log("Change not handled");
		}
	};

	/**
	 * метод обрабатывает тип особенности компонента, выдает доп. компонент
	 */
	const getFeature = () => {
		switch (featureType) {
			case feature.TEXT_FIELD:
				return (
					<TextField
						propertyName={featurePropertyName}
						reducer={reducer}
						style={featureStyle}
						clearOnDisable
						customOnChange={featureOnChange}
						disabled={!localStateValue ?? true}
						placeholder={featurePlaceholder}
						typeOfValue={featureTypeOfValue}
						isRequired={localStateValue ?? false}
						feedBack={false}
						useReduxValues
					/>
				);
		}

		return customFeature ? customFeature : null;
	};

	return (
		<React.Fragment>
			<div
				style={style}
				className="check-box"
			>
				<Label>
					<Input
						id={keyFiller}
						name={keyFiller}
						type="checkbox"
						onChange={handleInnerChange}
						checked={localStateValue}
					/>
					<span style={{ margin: "0 0 0 0.5rem" }}>{label}</span>
				</Label>
				{getFeature()}
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

export default connect(mapStateToProps, mapDispatchToProps)(CheckBox);

CheckBox.propTypes = {
	state: PropTypes.object.isRequired,
	updateReduxValue: PropTypes.func.isRequired,
};
