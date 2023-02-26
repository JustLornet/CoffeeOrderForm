import {
	REQUEST_DATA,
	SET_REDUX_COFFEE_SELECTIONS,
	UPDATE_REDUX_SIMPLE_VALUE,
	SET_REDUX_COFFEE_COMPOSITION,
	SET_REDUX_ORDER_DATA,
	UPDATE_REDUX_CUSTOM_COMPOSITION_VALUE,
	CLEAR_REDUX_COFFEE_PAGE,
} from "../actions/coffeeOrderPage";
import cloneDeep from "lodash/cloneDeep";

const initInstance = {
	coffeeType: null,
	customCompositions: [],
};

const initState = {
	instance: initInstance,
	selections: {
		coffeeTypes: [],
		syrups: [],
		ingredients: [],
		orderHistory: [],
	},
	isFetching: false,
};

// если свойство лежит не в instance, а глубже
const walkThroughNames = (initInstance, names, newValue) => {
	// на этом моменте каемся, что испольуем deepcopy и идем дальше
	let newState = cloneDeep(initInstance);
	let stateElement = newState;
	names.forEach((namePart, index) => {
		if (index === names.length - 1) {
			// если элемент - предпоследний
			console.log(names, newValue, stateElement[namePart])
			stateElement[namePart] = newValue;
		} else if (index < names.length - 1) {
			// всё, кроме последнего элемента
			stateElement = stateElement[namePart];
		} else if (index === names.length - 1) {
			// последний элемент
			stateElement[namePart] = newValue;
		} else {
			console.log("Error");
		}
	});

	return newState;
};

/**
 * Проверяет, есть ли переданный состав в redux, если нет или отличается,
 * дает актуальное значение всего текущего объекта
 */
const insertCustomComposition = (itemId, newValue, currentObject) => {
	if (newValue) {
		const itemIndex = currentObject.findIndex(item => item.ingredientId == itemId);
		if (itemIndex >= 0) {
			if (currentObject[itemIndex].value != newValue) {
				return currentObject.map(item =>
					item.ingredientId == itemId ? { ingredientId: itemId, value: newValue } : item
				);
			}
		} else {
			return [...currentObject, { ingredientId: itemId, value: newValue }];
		}
	}

	return currentObject;
};

const coffeeOrderPage = (state = initState, action) => {
	switch (action.type) {
		case REQUEST_DATA:
			return { ...state, isFetching: true };
		case SET_REDUX_COFFEE_SELECTIONS:
			if (action.propertyName) {
				return {
					...state,
					isFetching: false,
					selections: {
						...state.selections,
						[action.propertyName]: action.items,
					},
				};
			} else {
				return {
					...state,
					isFetching: false,
					selections: action.items,
				};
			}
		case SET_REDUX_COFFEE_COMPOSITION:
			return {
				...state,
				selections: {
					...state.selections,
					compositions: {
						...state.selections.compositions,
						[action.coffeeId]: action.items,
					},
				},
			};
		case UPDATE_REDUX_SIMPLE_VALUE:
			if (typeof action.propertyName === "string") {
				return {
					...state,
					instance: {
						...state.instance,
						[action.propertyName]: action.value,
					},
				};
			} else if (Array.isArray(action.propertyName)) {
				return {
					...state,
					instance: walkThroughNames(state.instance, action.propertyName, action.value),
				};
			} else {
				console.log(`Error with:`, action);

				return state;
			}
		case SET_REDUX_ORDER_DATA:
			// при подтверждении заказа - отправке на бэк
			return {
				...state,
				instance: action.order ?? initInstance,
			};
		case UPDATE_REDUX_CUSTOM_COMPOSITION_VALUE:
			return {
				...state,
				instance: {
					...state.instance,
					customCompositions: insertCustomComposition(
						action.itemId,
						action.value,
						state.instance.customCompositions
					),
				},
			};
		case CLEAR_REDUX_COFFEE_PAGE:
			return {
				...state,
				instance: initInstance,
			};
		default:
			return state;
	}
};

export default coffeeOrderPage;
