import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import "./DateTime.css";
import PropTypes from "prop-types";
import moment from "moment";
import DatePicker, { registerLocale } from "react-datepicker";
import ru from "date-fns/locale/ru";
import "react-datepicker/dist/react-datepicker.css";
import { parseISO } from "date-fns";
import { FormFeedback } from "reactstrap";

const componentType = {
	DATE: "date",
	TIME: "time",
	DATE_TIME: "dateTime",
};

/**
 * Компонент отдает дату в формате moment
 */
const DateTime = ({
	// общее хранилище redux
	state,
	// текущий reducer - для возможности использовния компонента
	// через redux в любом месте
	reducer,
	// имя свойства, которое смотрим
	propertyName,
	className,
	style,
	datePickerStyle,
	// обработка изменений из родительского компонента
	customOnChange,
	// если передаем значение вручную (передавать в ISO)
	value,
	// является ли поле обязательным
	isRequired,
	type = componentType.DATE,
	updateReduxValue = f => f,
}) => {
	// подключение локализации
	registerLocale("ru", ru);

	let initValue = null;
	try {
		if (reducer && propertyName) {
			initValue = state[reducer].instance[propertyName];
		}
	} catch {
		console.log(`Property ${propertyName} not found`);
		// ignore
		// TODO: далее добавить обработку
	}

	useEffect(() => {
		if (value && parseISO(value) != localStateValue) {
			handleChange(parseISO(value));
		}
	}, [value]);

	/**
	 * Обработчик изменений в redux
	 */
	const updateReduxValueInner = newValue => {
		updateReduxValue(reducer, propertyName, newValue);
	};

	// сохранение значения в локальное хранилище
	const [localStateValue, setLocalStateValue] = useState(initValue);

	/**значение передаем в формате ISO */
	const handleInnerChange = newDateTime => {
		handleChange(newDateTime);
	};

	/**
	 * Метод, принимающий новое значение, запускает изменения состояния
	 * как хранилища redux, так и локального
	 */
	const handleChange = newDateTime => {
		// обработка значения в завсимости от типа
		const momentValue = moment(newDateTime);
		setLocalStateValue(parseISO(momentValue.format()));

		if (typeof customOnChange === "undefined") {
			updateReduxValueInner(momentValue);
		} else if (typeof customOnChange !== "undefined") {
			customOnChange(momentValue);
		} else {
			console.log("Change not handled");
		}
	};

	const handleType = currentType => {
		switch (currentType) {
			case componentType.DATE:
				return (
					<DatePicker
						id={propertyName}
						name={propertyName}
						onChange={handleInnerChange}
						selected={localStateValue}
						style={datePickerStyle}
						className="date-time__picker"
						locale="ru"
						dateFormat="dd.MM.yyyy"
					/>
				);
			case componentType.TIME:
				return (
					<DatePicker
						id={propertyName}
						name={propertyName}
						selected={localStateValue}
						onChange={handleInnerChange}
						showTimeSelect
						showTimeSelectOnly
						timeIntervals={10}
						locale="ru"
						timeCaption="Time"
						dateFormat="HH:mm"
						style={datePickerStyle}
						className="date-time__picker"
					/>
				);
		}
	};

	return (
		<React.Fragment>
			<div
				style={style}
				className={className ? className : `date-time`}
			>
				{isRequired ? (
					localStateValue ? null : (
						<p className="date-time__feed-back">Обязательное поле</p>
					)
				) : null}
				{handleType(type)}
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
			import(`../../store/actions/${reducer}`).then(actionCreator =>
				dispatch(actionCreator.updateReduxSimpleValue(propertyName, value))
			);
		},
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(DateTime);

DateTime.propTypes = {
	state: PropTypes.object.isRequired,
	updateReduxValue: PropTypes.func.isRequired,
};
