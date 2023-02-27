import React, { useState, useEffect } from "react";
import { Button, Label } from "reactstrap";
import DateTime from "../../Common/DateTime/DateTime";
import "./CoffeePageFooter.css";
import moment from "moment";

const CoffeePageFooter = ({
	isConfirmEnabled,
	// в формате ISO без time zone
	orderDateTime,
	confirmOrder = f => f,
	updateValue = f => f,
}) => {
	// пришедше значение в формает ISO
	const orderTimeISO = moment(orderDateTime).format();

	// дата и время, объединенные вместе в формате ISO
	const [localDateTime, setLocalDateTime] = useState(
		orderDateTime ? orderTimeISO : moment("01010001 000000", "DDMMYYYY HHmmss").format()
	);

	useEffect(() => {
		if (orderTimeISO != localDateTime) {
			console.log(orderDateTime, localDateTime);
			setLocalDateTime(orderTimeISO);
		}
	}, [orderDateTime]);

	/**
	 * Отдельно считываем дату и время и изменяем в текущей дате-времени, хранящейся локально в state
	 * @param {*} currentType
	 * @param {*в формате moment} newValue
	 */
	const handleInnerChange = (currentType, newValue) => {
		console.log(localDateTime, newValue);
		let newDateTime = null;
		switch (currentType) {
			case "time":
				newDateTime = moment(
					`${newValue.format("HHmmss")} ${moment(localDateTime).format("DDMMYYYY")}`,
					"HHmmss DDMMYYYY"
				);
				console.log(newDateTime);
				break;
			case "date":
				newDateTime = moment(
					`${moment(localDateTime).format("HHmmss")} ${newValue.format("DDMMYYYY")}`,
					"HHmmss DDMMYYYY"
				);
				console.log(newDateTime);
				break;
		}

		handleChange(newDateTime, newValue.format());
	};

	const handleChange = (newMoment, initNewValueISO) => {
		console.log(orderTimeISO, initNewValueISO);
		setLocalDateTime(newMoment.format());
		if (orderTimeISO != initNewValueISO) {
			updateValue(newMoment.format());
		}
	};

	return (
		<div className="page-footer">
			<Label style={{ margin: "0 0 0 1.5rem", fontWeight: "500" }}>
				Выберите дату и время заказа:
			</Label>
			<DateTime
				className="page-footer__order-date"
				style={{ margin: "0 1rem" }}
				type="date"
				isRequired
				value={orderDateTime ? orderTimeISO : null}
				customOnChange={value => handleInnerChange("date", value)}
			/>
			<DateTime
				className="page-footer__order-time"
				style={{ margin: "0 1rem" }}
				type="time"
				isRequired
				value={orderDateTime ? orderTimeISO : null}
				customOnChange={value => handleInnerChange("time", value)}
			/>
			<Button
				className="page-footer__save-button"
				onClick={confirmOrder}
				disabled={!isConfirmEnabled}
				title={
					isConfirmEnabled
						? "Отправка заказа на подготовку"
						: "Пожалуйста, заполните обязательные поля"
				}
			>
				Подтвердить заказ
			</Button>
		</div>
	);
};

export default CoffeePageFooter;
