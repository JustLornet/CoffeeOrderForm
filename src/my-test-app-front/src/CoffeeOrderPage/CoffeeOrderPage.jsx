import React, { useEffect, useState } from "react";
import "./CoffeeOrderPage.css";
import {
	fetchCoffeeMainSelections,
	sendDataToBack,
	updateReduxCustomCompositionValue,
	updateReduxSimpleValue,
	clearReduxCoffeePage,
} from "../store/actions/coffeeOrderPage";
import { connect } from "react-redux";
import SelectField from "../Common/SelectField/SelectField";
import { Spinner, Label } from "reactstrap";
import { checkIfSelectionsIsIncomplete } from "../Common/Service/ServiceFunctions";
import RadioButton from "../Common/RadioButton/RadioButton";
import CheckBox from "../Common/CheckBox/CheckBox";
import TextField from "../Common/TextField/TextField";
import CoffeeImage from "./CoffeeImage/CoffeeImage";
import CoffeComposition from "./CoffeComposition/CoffeComposition";
import CoffeePageFooter from "./CoffeePageFooter/CoffeePageFooter";
import { BsBack, BsFillXSquareFill } from "react-icons/bs";
import OrderHistory from "./OrderHistory/OrderHistory";

const CoffeeOrderPage = ({
	coffeeOrderPage,
	selections,
	sendToBack = f => f,
	fetchCoffeeSelections = f => f,
	setCompositionInRedux = f => f,
	updateReduxValue = f => f,
	clearForm = f => f,
}) => {
	// reducer для текущей страницы, необходимо для переиспользования общих компонентов
	const currentReducer = "coffeeOrderPage";

	// получение элементов с бэка при первом рендере компонента
	useEffect(() => {
		if (checkIfSelectionsIsIncomplete(selections)) {
			fetchCoffeeSelections();
		}
	}, []);

	// модальное окно для истории заказов
	const [modalIsOpen, setModalIsOpen] = useState(false);

	/**
	 * обарботка события нажатия на кнопку показа истории
	 */
	const showOrderHistory = valueBool => {
		setModalIsOpen(valueBool);
	};

	return (
		<React.Fragment>
			{coffeeOrderPage.isFecthing ? (
				<Spinner
					width="40px"
					height="40px"
					color="info"
				/>
			) : (
				<div
					id="mainCoffeeOrderPage"
					className="page"
				>
					<div className="page__main-container">
						<OrderHistory
							isOpen={modalIsOpen}
							closeWindow={() => showOrderHistory(false)}
						/>
						<div
							className="page__coffee-header"
							style={{ margin: "0 0 1.4rem 0" }}
						>
							<span>Choose your coffee!</span>
						</div>
						<div className="main-container__body">
							<div className="page__main-columns page__main-columns_left">
								<div>
									<div
										style={{
											display: "flex",
											justifyContent: "space-between",
											margin: "0 0 1rem 0",
										}}
									>
										<div style={{ display: "flex", flexDirection: "column" }}>
											<div
												style={{
													display: "flex",
													flexDirection: "row",
													margin: "0 0 1.5rem 0",
												}}
											>
												<span
													style={{
														margin: "0 1.5rem 0 0",
														fontWeight: "550",
														color: "#4E342E",
													}}
												>
													Меня зовут
												</span>
												<TextField
													reducer={currentReducer}
													propertyName="customerName"
													isRequired
													placeholder="имя"
													useReduxValues
												/>
											</div>
											<div
												style={{
													display: "flex",
													flexDirection: "row",
												}}
											>
												<span
													style={{
														margin: "0 1.5rem 0 0",
														fontWeight: "550",
														color: "#4E342E",
													}}
												>
													Я хочу заказать
												</span>
												<SelectField
													reducer={currentReducer}
													propertyName="coffeeType"
													selectionName="coffeeTypes"
													useReduxValue
													style={{
														width: "7rem",
														fontWeight: "600",
														padding: "0.2rem",
														color: "#3E2723",
														backgroundColor: "white",
													}}
												/>
											</div>
										</div>
										<div className="page__main-icons-container">
											<BsBack
												title="История заказов"
												className="page__main-icon"
												onClick={() => showOrderHistory(true)}
												// display={
												// 	coffeeOrderPage.instance.customerName
												// 		? ""
												// 		: "none"
												// }
											/>
											<BsFillXSquareFill
												title="Очистить форму, кроме имени"
												className="page__main-icon"
												onClick={clearForm}
												display={
													coffeeOrderPage.instance.coffeeType
														? ""
														: "none"
												}
											/>
										</div>
									</div>

									<div style={{ margin: "0 0 0 0.5rem" }}>
										{coffeeOrderPage.instance.coffeeType ? (
											<React.Fragment>
												<Label className="page__standart-header">
													Состав стандартного{" "}
													{coffeeOrderPage.instance.coffeeType.name.toLowerCase()}
													:
												</Label>
												<CoffeComposition
													style={{
														height: "5.1rem",
														margin: "0 0 0.5rem 0",
													}}
													coffeeId={
														coffeeOrderPage.instance.coffeeType.id
													}
												/>
												<Label className="page__standart-header">
													Описание:
												</Label>
												<p
													className="page__description-text"
													style={{ height: "4.8rem" }}
												>
													{
														coffeeOrderPage.instance.coffeeType
															.description
													}
												</p>
											</React.Fragment>
										) : null}
									</div>
								</div>
								<div style={{ margin: "0 0 0 0.5rem" }}>
									{coffeeOrderPage.instance.coffeeType ? (
										<React.Fragment>
											<Label className="page__standart-header">
												Соберите свой кофе:
											</Label>
											{coffeeOrderPage.selections.ingredients.map(
												ingredient => (
													<CheckBox
														key={`Ingredients-${ingredient.id}`}
														style={{
															margin: "0 0 0.2rem 0",
															display: "flex",
															flexDirection: "row",
															justifyContent: "space-between",
															width: "100%",
														}}
														useReduxValue
														reducer={currentReducer}
														customOnChange="none"
														propertyName={[
															"customCompositions",
															coffeeOrderPage.instance.customCompositions.findIndex(
																item =>
																	item.ingredientId ==
																	ingredient.id
															),
															"value",
														]}
														label={ingredient.name}
														featureType="textField"
														featureOnChange={newValue =>
															setCompositionInRedux(
																ingredient.id,
																newValue
															)
														}
														featurePropertyName={[
															"customCompositions",
															coffeeOrderPage.instance.customCompositions.findIndex(
																item =>
																	item.ingredientId ==
																	ingredient.id
															),
															"value",
														]}
														featureStyle={{
															width: "8rem",
														}}
														featurePlaceholder={`... ${ingredient.ingredientUnit.name}`}
														featureTypeOfValue="positiveInteger"
													/>
												)
											)}
										</React.Fragment>
									) : null}
								</div>
							</div>
							<div className="page__main-columns">
								{coffeeOrderPage.instance.coffeeType ? (
									<CoffeeImage
										className="page__image page__image_main"
										imageStyle={{
											filter: "hue-rotate(300deg)",
										}}
										imagePath={`CoffeeTypes/${coffeeOrderPage.instance.coffeeType.name}/Composition.jpg`}
									/>
								) : null}
								<div style={{ margin: "1rem 0 0.2rem 0", display: "block" }}>
									{coffeeOrderPage.instance.coffeeType ? (
										<React.Fragment>
											<Label className="page__standart-header">
												Комментарий:
											</Label>
											<TextField
												wrapped
												style={{
													width: "100%",
													height: "7rem",
													margin: "0 0 1rem 0",
												}}
												validCheck={false}
												reducer={currentReducer}
												propertyName="comments"
												placeholder="Комментарии к заказу"
											/>
											<Label className="page__standart-header">
												Сироп для вашего кофе:
											</Label>
											<div
												style={{
													display: "flex",
													justifyContent: "space-between",
												}}
											>
												<RadioButton
													itemStyle={{
														margin: "0.2rem 0",
													}}
													reducer={currentReducer}
													selectionName="syrups"
													propertyName="syrup"
												/>
												{coffeeOrderPage.instance.syrup ? (
													<CoffeeImage
														style={{ margin: "0 2rem", height: "auto" }}
														className="page__image page__image_small"
														imagePath={
															coffeeOrderPage.instance.syrup
																? `Syrups/${coffeeOrderPage.instance.syrup.name}/Bottle.png`
																: null
														}
														imageStyle={{ height: "auto" }}
													/>
												) : null}
											</div>
											<p
												className={`page__description-text ${
													!coffeeOrderPage.instance.syrup
														? `page__description-text_empty`
														: null
												}`}
												style={{
													margin: "0 0 0 0",
													height: "6rem",
												}}
											>
												{coffeeOrderPage.instance.syrup?.description}
											</p>
										</React.Fragment>
									) : null}
								</div>
							</div>
						</div>
					</div>
					<div className="page__footer-container">
						{coffeeOrderPage.instance.coffeeType ? (
							<CoffeePageFooter
								isConfirmEnabled={
									coffeeOrderPage.instance.orderExecutionDateTime &&
									coffeeOrderPage.instance.customerName &&
									coffeeOrderPage.instance.coffeeType
								}
								orderDateTime={coffeeOrderPage.instance.orderExecutionDateTime}
								confirmOrder={() => sendToBack(coffeeOrderPage.instance)}
								updateValue={newValue =>
									updateReduxValue("orderExecutionDateTime", newValue)
								}
							/>
						) : null}
					</div>
				</div>
			)}
		</React.Fragment>
	);
};

const mapStateToProps = state => {
	// console.log(state);
	return {
		coffeeOrderPage: state.coffeeOrderPage,
		selections: state.coffeeOrderPage.selections,
	};
};

const mapDispatchToProps = dispatch => {
	return {
		fetchCoffeeSelections: () => dispatch(fetchCoffeeMainSelections()),
		sendToBack: order => dispatch(sendDataToBack(order)),
		setCompositionInRedux: (itemId, newValue) =>
			dispatch(updateReduxCustomCompositionValue(itemId, newValue)),
		updateReduxValue: (propertyName, value) =>
			dispatch(updateReduxSimpleValue(propertyName, value)),
		clearForm: () => dispatch(clearReduxCoffeePage()),
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(CoffeeOrderPage);
