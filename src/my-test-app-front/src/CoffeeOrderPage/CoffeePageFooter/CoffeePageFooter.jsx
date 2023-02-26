import React, { useState } from "react";
import { Button, Label } from "reactstrap";
import DateTime from "../../Common/DateTime/DateTime";
import "./CoffeePageFooter.css";
import moment from "moment";

const CoffeePageFooter = ({
	isConfirmEnabled,
	orderDateTime,
	confirmOrder = f => f,
	updateValue = f => f,
}) => {
	// дата и время, объединенные вместе в формате moment
	const [localDateTime, setLocalDateTime] = useState(
		moment(orderDateTime ?? "01010001 000000", "DDMMYYYY HHmmss")
	);
	const handleInnerChange = (currentType, newValue) => {
		let newDateTime = moment(localDateTime);
		switch (currentType) {
			case "date":
				newDateTime = moment(
					`${localDateTime.format("HHmmss")} ${newValue.format("DDMMYYYY")}`,
					"HHmmss DDMMYYYY"
				);
				break;
			case "time":
				newDateTime = moment(
					`${newValue.format("HHmmss")} ${localDateTime.format("DDMMYYYY")}`,
					"HHmmss DDMMYYYY"
				);
				break;
		}

		handleChange(newDateTime);
	};

	const handleChange = newMoment => {
		setLocalDateTime(newMoment);
		updateValue(newMoment.format());
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
				value={orderDateTime}
				customOnChange={value => handleInnerChange("date", value)}
			/>
			<DateTime
				className="page-footer__order-time"
				style={{ margin: "0 1rem" }}
				type="time"
				isRequired
				value={orderDateTime}
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
