import React from "react";
import PropTypes from "prop-types";
import { BASE_BACK_PATH } from "../../store/api";
import "./CoffeeImage.css";

const REQUEST_COFFEE_IMG = `${BASE_BACK_PATH}/Coffee/GetImage?path=Images/`;

// TODO: далее переделать компонент под общий - сделать универсальным
const CoffeeImage = ({ imagePath, style, imageStyle, className }) => {
	return (
		<div
			className={className ? className : `coffee-image`}
			style={style}
		>
			<img
				className="coffee-image__img"
				style={imageStyle}
				src={`${REQUEST_COFFEE_IMG}${imagePath}`}
			/>
		</div>
	);
};

export default CoffeeImage;

CoffeeImage.propTypes = {
	imagePath: PropTypes.string.isRequired,
};
