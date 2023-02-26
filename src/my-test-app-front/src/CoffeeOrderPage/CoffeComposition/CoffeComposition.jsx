import React, { useEffect, useState } from "react";
import { fetchCoffeeComposition } from "../../store/actions/coffeeOrderPage";
import { Spinner } from "reactstrap";
import { connect } from "react-redux";
import "./CoffeComposition.css";

const CoffeComposition = ({
	coffeeType,
	compositions,
	coffeeId,
	style,
	fetchComposition = f => f,
}) => {
	// состав кофе для coffeeId
	const [currentComposition, setCurrentComposition] = useState([]);

	/**
	 * проверяет есть ли уже в redux состав для данного кофе
	 * true - если есть
	 */
	const checkIsCompositionInState = coffeeId => {
		if (compositions && typeof coffeeId === "number") {
			const coffeeIds = Object.keys(compositions);

			return coffeeIds.some(key => key == coffeeId);
		}

		return false;
	};

	useEffect(() => {
		if (!checkIsCompositionInState(coffeeId)) {
			fetchComposition(coffeeId);
		}
	}, [coffeeId]);

	useEffect(() => {
		if (checkIsCompositionInState(coffeeId)) {
			setCurrentComposition(compositions[coffeeId]);
		}
	}, [compositions, coffeeId]);

	const parseToRender = currentIngredient => {
		// название и описание меры весов для ингридиента
		const unitName = currentIngredient.ingredient.ingredientUnit.name;
		const unitDescription = currentIngredient.ingredient.ingredientUnit.description;

		return (
			<li
				key={currentIngredient.ingredient.id}
				title={`${
					currentIngredient.ingredient.name
				} в стандартной порции ${coffeeType.name.toLowerCase()}, выраженное в ${
					unitDescription ? `${unitDescription}ах` : unitName
				}`}
			>
				{`${currentIngredient.ingredient.name} - ${currentIngredient.value} ${unitName}`}
			</li>
		);
	};

	return (
		<div
			style={style}
			className={`coffee-composition ${
				currentComposition.length > 0 ? "" : "coffee-composition_loading"
			}`}
		>
			{currentComposition.length > 0 ? (
				currentComposition.map(ingr => parseToRender(ingr))
			) : (
				<Spinner
					width="40px"
					heigth="40px"
				/>
			)}
		</div>
	);
};

const mapStateToProps = state => {
	return {
		coffeeType: state.coffeeOrderPage.instance.coffeeType,
		compositions: state.coffeeOrderPage.selections?.compositions,
	};
};

const mapDispatchToProps = dispatch => {
	return {
		fetchComposition: coffeeId => dispatch(fetchCoffeeComposition(coffeeId)),
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(CoffeComposition);
