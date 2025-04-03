import { ACTIVATE, API_URL } from "../constents/constants";
import axios from "axios";
import { toast } from "react-toastify";

export const getTableDataFromApi = async (data) => {
  const TABLE_API = API_URL + data.apiurl;

  delete data.apiurl;
  try {
    const response = await axios.get(TABLE_API, { params: data });
    if (response.status === 200) {
      return response.data;
    } else {
      console.error("Error retrieving products:", response.status);
    }
  } catch (error) {
    console.error("Error retrieving products:", error);
  }
};

export const deleteTableRowApi = (data) => {
  const TABLE_API = API_URL + data?.apiurl + "/" + data?.id;

  delete data.apiurl;
  return new Promise(async function (myResolve, myReject) {
    try {
      const response = await axios.delete(TABLE_API);
      if (response.status === 200) {
        myResolve(response.data);
      } else {
        myReject("Error deleting :");
        console.error("Error deleting:", response.status);
      }
    } catch (error) {
      myReject("Error deleting:");
      console.error("Error deleting:", error);
      toast.error(error.response.data.errors.id[0]);
    }
  });
};

export const activateTableRowApi = (data) => {
  const TABLE_API =
    API_URL + "/" + data?.apiurl + "/" + data?.data.id + "/" + ACTIVATE;
  const postHeader = data?.headers;
  delete data.apiurl;
  return new Promise(async function (myResolve, myReject) {
    try {
      const response = await axios.put(TABLE_API, {
        ...data?.data,
        active: true,
        headers: postHeader,
      });
      if (response.status === 200) {
        myResolve(response.data);
      } else {
        myReject("Error activating:");
        console.error("Error activating:", response.status);
      }
    } catch (error) {
      myReject("Error activating:");
      console.error("Error activating:", error);
    }
  });
};
