import React from "react";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import "./App.css";
import CoffeeOrderPage from './CoffeeOrderPage/CoffeeOrderPage'

export const App = () => {
	return (
		<div className="app__container">
			<div className="app">
				<main>
					<Router>
						<Routes>
							<Route path="/" element={<CoffeeOrderPage />} />
							<Route
								path="*"
								element={<p>Неопознанный маршрут</p>}
							/>
						</Routes>
					</Router>
				</main>
			</div>
		</div>
	);
};

export default App;