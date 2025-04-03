import axios from 'axios';
const API_URI = process.env.REACT_APP_API_URI || 'api';

export const fetchInbounds = async () => {
  try {
      const response = await axios.get(`${API_URI}/dashboard/inboundTransaction`);
    if (response.status === 200) {
      return response.data;
    } else {
      throw new Error('Failed to fetch dashboard statistics');
    }
  } catch (error) {
    console.error('Error fetching dashboard statistics:', error);
    throw error;
  }
};