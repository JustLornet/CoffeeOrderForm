import { combineReducers } from "redux";
import coffeeOrderPage from "../reducers/coffeeOrderPage";

const rootReducer = combineReducers({
    coffeeOrderPage,
});

export default rootReducer;