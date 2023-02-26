import * as api from "../api";

export const REQUEST_DATA = "REQUEST_DATA";
export const requestData = () => {
	return { type: REQUEST_DATA };
};

export const SET_REDUX_COFFEE_SELECTIONS = "SET_REDUX_COFFEE_SELECTIONS";
/**
 * Если передается только items, чистит весь selections, приравнивая к items
 * Если передается ещё propertyName, добавляет новый selection
 */
const setReduxCoffeeSelections = (items, propertyName) => {
	return {
		type: SET_REDUX_COFFEE_SELECTIONS,
		items: items,
		propertyName: propertyName,
	};
};

export const SET_REDUX_COFFEE_COMPOSITION = "SET_REDUX_COFFEE_COMPOSITION";
const setReduxCoffeeComposition = (items, coffeeId) => {
	return {
		type: SET_REDUX_COFFEE_COMPOSITION,
		items: items,
		coffeeId: coffeeId,
	};
};

export const SET_REDUX_ORDER_DATA = "SET_REDUX_ORDER_DATA";
export const setReduxOrderData = order => {
	return {
		type: SET_REDUX_ORDER_DATA,
		order: order,
	};
};

export const UPDATE_REDUX_SIMPLE_VALUE = "UPDATE_REDUX_SIMPLE_VALUE";
export const updateReduxSimpleValue = (propertyName, value) => {
	return {
		type: UPDATE_REDUX_SIMPLE_VALUE,
		propertyName: propertyName,
		value: value,
	};
};

export const UPDATE_REDUX_CUSTOM_COMPOSITION_VALUE = "UPDATE_REDUX_CUSTOM_COMPOSITION_VALUE";
/**
 * Обновление поля доп. ингридиентов в соответствии с форматом, который принимает бэк
 */
export const updateReduxCustomCompositionValue = (itemId, value) => {
	return {
		type: UPDATE_REDUX_CUSTOM_COMPOSITION_VALUE,
		itemId: itemId,
		value: value,
	};
};

export const CLEAR_REDUX_COFFEE_PAGE = "CLEAR_REDUX_COFFEE_PAGE";
/**
 * Очистка полей в redux для сброса формы
 */
export const clearReduxCoffeePage = () => {
	return {
		type: CLEAR_REDUX_COFFEE_PAGE,
	};
};

export const fetchCoffeeMainSelections = () => {
	return dispatch => {
		dispatch(requestData());
		api.get(`/Coffee/GetSelections`)
			.then(response => {
				if (response.status != 200) {
					console.log("Error", response);
					return [];
				}

				return response.data;
			})
			.then(json => dispatch(setReduxCoffeeSelections(json)));
	};
};

export const fetchCoffeeComposition = coffeeId => {
	return dispatch => {
		dispatch(requestData());
		api.get(`/Coffee/GetComposition?coffeeId=${coffeeId}`)
			.then(response => {
				if (response.status != 200) {
					console.log("Error", response);
					return null;
				}

				return response.data;
			})
			.then(json => dispatch(setReduxCoffeeComposition(json, coffeeId)));
	};
};

export const fetchOrderHistory = () => {
	return dispatch => {
		dispatch(requestData());
		api.get(`/Coffee/GetOrderHistory`)
			.then(response => {
				if (response.status != 200) {
					console.log("Error", response);
					return null;
				}

				return response.data;
			})
			.then(json => dispatch(setReduxCoffeeSelections(json, "orderHistory")));
	};
};

export const sendDataToBack = order => {
	return dispatch => {
		dispatch(requestData());
		api.post(`/Coffee/SaveOrder`, order)
			.then(response => {
				if (response.status != 200) {
					console.log("Error", response);
					return null;
				}

				return response.data;
			})
			.then(json => dispatch(setReduxOrderData(null)));
	};
};

// export const fetchImage = (path) => {
// 	return (dispatch) => {
// 		axios
// 			.get(`https://localhost:7138/base/getimage?path=${path}`)
// 			.then((response) => response.data['Result']);
// 	};
// };

// export const fetchItems = () => {
// 	return (dispatch) => {
// 		axios
// 			.get("https://localhost:7138/base/getallitemswithimages")
// 			.then((response) => dispatch(fetchItemsSuccess(response.data)))
// 			.catch((error) => console.log("error", error));
// 	};
// };
