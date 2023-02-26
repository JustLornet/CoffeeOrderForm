import React, { useEffect, useState } from "react";
import Modal from "react-modal";
import { connect } from "react-redux";
import { Button, Label, Spinner, Input } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { fetchOrderHistory, setReduxOrderData } from "../../store/actions/coffeeOrderPage";
import "./OrderHistory.css";
import moment from "moment";

const OrderHistory = ({
	isOpen,
	allOrders,
	isFetching,
	getOrderHistory = f => f,
	closeWindow = f => f,
	chooseOrder = f => f,
}) => {
	useEffect(() => {
		// запрос истории заказов при каждом открытии окна
		if (isOpen) {
			getOrderHistory();
		}
	}, [isOpen]);

	const handleOrderClick = ev => {
		const orderId = ev.currentTarget.id;
		chooseOrder(allOrders.find(item => item.id == orderId));
		closeWindow();
	};

	// фильтрация - простая строка поиска по имени и дате совершения заказа
	const [searchText, setSearchText] = useState("");

	/**Поиск по имени заказчика и дате заказа */
	const handleSearchInput = ev => {
		ev.preventDefault();
		const text = ev.target.value;
		setSearchText(text);
	};

	/**Проверка заказа на то, подходит ли он по условиям фильтрации */
	const checkOrderForSearch = order => {
		// проверка заказа по имени покупателя
		const nameCheck = order.customerName.toLowerCase().includes(searchText.toLowerCase());
		if (nameCheck) {
			return true;
		}

		//проверка заказа по дате соверешения заказа
		const orderMomentParsed = moment(order.orderCreationDateTime).format("DDMMYYYYHHmm");
		const symbolsToReplace = [" ", "-", "/", "-", ",", ".", "\\"];

		let parsedSearchString = searchText;
		symbolsToReplace.forEach(symbol => {
			parsedSearchString = parsedSearchString.replace(symbol, "");
		});

		return orderMomentParsed.includes(parsedSearchString);
	};

	const parseOrderToComponent = order => {
		return (
			<div
				id={order.id}
				key={order.id}
				className="order-history__order-position"
				onClick={handleOrderClick}
				style={{ padding: "0.5rem 0" }}
			>
				<li style={{ margin: "0 0 0 1rem" }}>
					<span>{`заказ на имя ${order.customerName}`}</span>
					<span>{` совершен: ${moment(order.orderCreationDateTime).format(
						"DD-MM-YYYY в HH:MM"
					)}`}</span>
				</li>
			</div>
		);
	};

	const handleOrder = order => {
		if (checkOrderForSearch(order)) {
			return parseOrderToComponent(order);
		}

		return null;
	};

	return (
		<Modal
			isOpen={isOpen}
			className="order-history__container"
		>
			<Label
				className="order-history__main-header"
				style={{ margin: "0 0 1rem 0" }}
			>
				История заказов
			</Label>
			<Input
				type="text"
				placeholder="Имя покупателя / дата заказа"
				onChange={handleSearchInput}
				value={searchText}
				style={{ margin: "0 0 0.5rem 0" }}
				className="order-history__search"
			/>
			<div className="order-history__body">
				{isFetching ? (
					<Spinner
						color="info"
						style={{ width: "4rem", height: "4rem" }}
					></Spinner>
				) : (
					<React.Fragment>
						{allOrders.length > 0
							? allOrders.map(order => handleOrder(order))
							: `Данные отсутствуют`}{" "}
					</React.Fragment>
				)}
			</div>
			<Button
				className="order-history__close-button"
				style={{ margin: "2rem 0 0 0" }}
				onClick={closeWindow}
			>
				Закрыть
			</Button>
		</Modal>
	);
};

const mapStateToProps = state => {
	return {
		isFetching: state.coffeeOrderPage.isFetching,
		allOrders: state.coffeeOrderPage.selections.orderHistory ?? [],
	};
};

const mapDispatchToProps = dispatch => {
	return {
		getOrderHistory: () => dispatch(fetchOrderHistory()),
		chooseOrder: order => dispatch(setReduxOrderData(order)),
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(OrderHistory);

Modal.setAppElement("#root");
