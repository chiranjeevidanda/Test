import axios from 'axios';
const API_URI = process.env.REACT_APP_API_URI || 'api';

export const fetchInboundTransactions = async (filters = {}) => {
    try {
        const response = await axios.get(`${API_URI}/InboundTransactions`, { params: filters });
        if (response.status === 200) {
            return response.data;
        } else {
            throw new Error('Failed to fetch inbound transactions');
        }
    } catch (error) {
        console.error('Error fetching inbound transactions:', error);
        throw error;
    }
};

export const fetchInboundTransactionsExport = async (filters = {}) => {
    try {
        const response = await axios.get(`${API_URI}/InboundTransactions/export`, { params: filters, responseType: 'blob' });
        if (response.status === 200) {
            return response.data;
        } else {
            throw new Error('Failed to Export the Inbound Transactions');
        }
    } catch (error) {
        console.error('Error Exporting the Inbound Transactions:', error);
        throw error;
    }
};