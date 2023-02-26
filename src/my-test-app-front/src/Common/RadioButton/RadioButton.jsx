import React, { useState } from "react";
import { connect } from "react-redux";
import "./RadioButton.css";
import { Input, FormGroup, Label } from "reactstrap";
import PropTypes from "prop-types";

const RadioType = {
	BOOL: "bool",
	DICT: "dict",
};

const RadioButton = ({
	// общее хранилище redux
	state,
	// текущий reducer - для возможности использовния компонента
	// через redux в любом месте
	reducer,
	// имя свойства, которое смотрим
	propertyName,
	// имя словаря с элементами
	selectionName,
	// Надпись в поле, пока ввод не начался, при этом первый элемент с id = -1 не ставится
	style,
	// стиль для каждого элмента - radio button
	itemStyle,
	// Тип компонента определяет его вид и особенности
	// на данный момент тип может быть 'bool' И 'dict'
	// bool - только true false, dict - значения берутся словаря
	type = RadioType.BOOL,
	updateReduxValue = f => f,
}) => {
	// TODO: переписать - много стекла
	const currentRadioType = type != RadioType.BOOL ? type : selectionName ? RadioType.DICT : type;
	let initValue = null;
	let selection = null;
	let valueChangeViaTypeParser = null;

	/**
	 * Определение типа текущего компонента
	 */
	switch (currentRadioType) {
		case RadioType.BOOL:
			try {
				initValue = state[reducer].instance[propertyName];
			} catch {
				console.log(`Property ${propertyName} not found`);
				// ignore
				// TODO: далее добавить обработку
			}
			valueChangeViaTypeParser = newValue => newValue == "true";
			break;
		case RadioType.DICT:
			try {
				initValue = state[reducer].instance[propertyName];
				selection = state[reducer].selections[selectionName];
			} catch {
				console.log(`Property ${propertyName} or ${selectionName} not found`);
				// ignore
				// TODO: далее добавить обработку
			}
			valueChangeViaTypeParser = newValueId =>
				selection.find(item => item.id == newValueId) ?? null;
			break;
		default:
			console.log("Неверный тип RadioButton");
	}

	/**
	 * Обработчик изменений в redux
	 */
	const updateReduxValueInner = newValue => {
		if (currentRadioType == RadioType.BOOL) {
			updateReduxValue(reducer, propertyName, Boolean(newValue));
		} else {
			updateReduxValue(reducer, propertyName, newValue);
		}
	};

	// сохранение значения в локальное хранилище
	const [localStateValue, setLocalStateValue] = useState(initValue ?? null);

	const parseToRadio = item => {
		return (
			<FormGroup
				key={`${propertyName} - ${item.id}`}
				check
				style={itemStyle}
			>
				<Label>
					<Input
						name={propertyName}
						value={item.id}
						checked={localStateValue ? item.id == localStateValue.id : false}
						type="radio"
						onChange={handleInnerChange}
						onClick={handleAlreadyChecked}
					/>

					<span style={{ margin: "0 0 0 0.1rem" }}>{item.name}</span>
				</Label>
			</FormGroup>
		);
	};

	const handleInnerChange = ev => {
		// ignore
		// заглушка, чтобы react не ругался на отсутствие onChange
		// выполнено через onClick, чтобы иметь возможность отжать кнопку
	};

	/**
	 * отжимает кнопку при повторном клике на то же поле
	 */
	const handleAlreadyChecked = ev => {
		const newValueUnparsed = ev.target.value;
		const newValue = valueChangeViaTypeParser(newValueUnparsed);
		handleChange(newValue == localStateValue ? null : newValue);
	};

	/**
	 * Метод, принимающий новое значение, запускает изменения состояния
	 * как хранилища redux, так и локального
	 */
	const handleChange = newValue => {
		setLocalStateValue(newValue);
		updateReduxValueInner(newValue);
	};

	/**
	 * Возвращает компонент в зависимости от типа Radio button
	 */
	const getRadioButtonViaType = () => {
		switch (currentRadioType) {
			case RadioType.BOOL:
				return (
					<React.Fragment>
						<FormGroup
							check
							style={itemStyle}
						>
							<Label>
								<Input
									name={propertyName}
									value={true}
									checked={localStateValue == true}
									type="radio"
									onChange={handleInnerChange}
									onClick={handleAlreadyChecked}
								/>
								Да
							</Label>
						</FormGroup>
						<FormGroup
							check
							style={itemStyle}
						>
							<Label>
								<Input
									name={propertyName}
									value={false}
									checked={localStateValue == false}
									type="radio"
									onChange={handleInnerChange}
									onClick={handleAlreadyChecked}
								/>
								Нет
							</Label>
						</FormGroup>
					</React.Fragment>
				);
			case RadioType.DICT:
				return selection.map(item => {
					return parseToRadio(item);
				});
			default:
				return null;
		}
	};

	return (
		<React.Fragment>
			{selection || currentRadioType == RadioType.BOOL ? (
				<div
					style={style}
					className="radio-button"
				>
					<FormGroup className="radio-button__group-container">{getRadioButtonViaType()}</FormGroup>
				</div>
			) : null}
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
			import(`../../store/actions/${reducer}`).then(actionCreator =>
				dispatch(actionCreator.updateReduxSimpleValue(propertyName, value))
			);
		},
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(RadioButton);

RadioButton.propTypes = {
	state: PropTypes.object.isRequired,
	reducer: PropTypes.string.isRequired,
	propertyName: PropTypes.string.isRequired,
	updateReduxValue: PropTypes.func.isRequired,
};
