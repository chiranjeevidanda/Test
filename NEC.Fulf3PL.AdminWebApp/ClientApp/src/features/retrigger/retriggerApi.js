import axios from 'axios';
import { API_HEADER_APPLICATION_JSON } from "../../constents/constants";
const API_URI = process.env.REACT_APP_API_URI || 'api';

export const submitRetriggerDocument = async (body) => {
    try {
        const response = await axios.post(`${API_URI}/Retrigger`, body, {
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

export const fetchRetriggerPayload = async (eventId) => {
    try {
        const response = await axios.get(`${API_URI}/Retrigger/${eventId}`);
        if (response.status === 200) {
            return response.data;
        } else {
            console.error('Error fetching Payload:', response);
            throw response;
        }
    } catch (error) {
        console.error('Error fetching Payload:', error);
        throw error;
    }
};