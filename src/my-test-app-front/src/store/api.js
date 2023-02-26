import axios from "axios";

export const BASE_BACK_PATH = "https://localhost:7133";

export const get = path => {
	return axios.get(`${BASE_BACK_PATH}${path}`);
};

export const post = (path, json) => {
	return axios.post(`${BASE_BACK_PATH}${path}`, json);
};
