import React, { useState } from "react";
import { connect } from "react-redux";
import "./SelectField.css";
import { Input } from "reactstrap";
import PropTypes from "prop-types";

const SelectField = ({
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
	// TODO: протестировать
	placeholder,
	// дефолтный текст, который пишется как первый элемент с id = -1
	defaultText,
	// какое значение будет использоваться - из redux или локального хранилища
	useReduxValue = false,
	style,
	updateReduxValue = f => f,
}) => {
	let initValue = null;
	let selection = null;
	try {
		initValue = state[reducer].instance[propertyName];
		selection = state[reducer].selections[selectionName];
	} catch {
		console.log(`Property ${propertyName} or ${selectionName} not found`);
		// ignore
		// TODO: далее добавить обработку
	}

	/**
	 * Обработчик изменений в redux
	 */
	const updateReduxValueInner = newValue => {
		updateReduxValue(reducer, propertyName, newValue);
	};

	// сохранение значения в локальное хранилище
	const [localStateValue, setLocalStateValue] = useState(initValue);

	// Значение, которое идет в компонент
	const currentValue = useReduxValue ? initValue : localStateValue;

	const parseToOption = item => {
		return (
			<option
				id={item.id}
				key={item.id}
				value={item.id}
			>
				{item.name}
			</option>
		);
	};

	const handleInnerChange = ev => {
		const newValueId = Number(ev.target.value);
		const newValue = selection.find(item => item.id === newValueId) ?? null;
		handleChange(newValue);
	};

	/**
	 * Метод, принимающий новое значение, запускает изменения состояния
	 * как хранилища redux, так и локального
	 */
	const handleChange = newValue => {
		setLocalStateValue(newValue);
		updateReduxValueInner(newValue);
	};

	return (
		<React.Fragment>
			{selection ? (
				<div className="select-field">
					<Input
						id={propertyName}
						name={propertyName}
						style={style}
						placeholder={placeholder}
						type="select"
						onChange={handleInnerChange}
						value={currentValue ? currentValue.id : -1}
					>
						{placeholder ? null : (
							<option
								id={-1}
								value={-1}
							>
								{defaultText}
							</option>
						)}
						{selection.map(item => {
							return parseToOption(item);
						})}
					</Input>
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

export default connect(mapStateToProps, mapDispatchToProps)(SelectField);

SelectField.propTypes = {
	state: PropTypes.object.isRequired,
	reducer: PropTypes.string.isRequired,
	propertyName: PropTypes.string.isRequired,
	selectionName: PropTypes.string.isRequired,
	updateReduxValue: PropTypes.func.isRequired,
};
