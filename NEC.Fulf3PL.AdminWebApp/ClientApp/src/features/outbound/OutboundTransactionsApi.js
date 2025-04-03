import axios from 'axios';
const API_URI = process.env.REACT_APP_API_URI || 'api';

export const fetchOutboundTransactions = async (filters = {}) => {
    try {
        const response = await axios.get(`${API_URI}/OutboundTransactions`, { params: filters });
        if (response.status === 200) {
            return response.data;
        } else {
            throw new Error('Failed to fetch outbound transactions');
        }
    } catch (error) {
        console.error('Error fetching outbound transactions:', error);
        throw error;
    }
};

export const fetchOutboundTransactionsExport = async (filters = {}) => {
    try {
        const response = await axios.get(`${API_URI}/OutboundTransactions/export`, { params: filters, responseType: 'blob' });
        if (response.status === 200) {
            return response.data;
        } else {
            throw new Error('Failed to Export the Outbound Transactions');
        }
    } catch (error) {
        console.error('Error Exporting the Outbound Transactions:', error);
        throw error;
    }
};
