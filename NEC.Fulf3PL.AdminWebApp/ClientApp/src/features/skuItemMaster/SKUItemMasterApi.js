import axios from 'axios';
import { API_HEADER_APPLICATION_JSON } from "../../constents/constants";
const API_URI = process.env.REACT_APP_API_URI || 'api';

export const submitSKU = async (body) => {
    try {
        const response = await axios.post(`${API_URI}/ItemMaster`, body, {
            headers: API_HEADER_APPLICATION_JSON
        });
        if (response.status === 200) {
            return response;
        } else {
            return {
                message: "An error occured while sending the request"
            };
        }
    } catch (error) {
        throw error;
    }
};

export const fetchSkuExport = async (filters = {}) => {
    try {
        const response = await axios.get(`${API_URI}/ItemMaster/export`, { params: filters, responseType: 'blob' });
        if (response.status === 200) {
            return response.data;
        } else {
            throw new Error('Download Failed' + response.status);
        }
    } catch (error) {
        console.error('Download Failed', error);
        throw error;
    }
};

export const fetchSKUs = async (filters) => {
    try {
        const response = await axios.get(`${API_URI}/ItemMaster`, { params: filters });
        if (response.status === 200) {
            return response.data;
        } else {
            throw new Error('Failed to fetch Documents');
        }
    } catch (error) {
        console.error('Error fetching Documents:', error);
        throw error;
    }
};