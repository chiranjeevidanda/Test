import { React, useState } from "react";
import { toast } from 'react-toastify';
import DatePicker from "react-datepicker";
import { useDispatch, useSelector } from "react-redux";
import { downloadExcel } from '../../../genaric/fns';
import DownloadIcon from '../../atom/DownloadIcon';
import { INOUND_TRANSACTION_ORDER_TYPE } from '../../../constents/constants';
import { fetchItems, clearFetchFilter, resetFilterFormData, selectFilterFormData, updateFilterFormData, fetchExports } from '../../../features/inbound/InboundTransactionsSlice';

const InboundTransactionsSearchBar = ({ showMessage, setShowMessage }) => {

    const dispatch = useDispatch();
    const filterFormData = useSelector(selectFilterFormData);
    const [messageText, setMessageText] = useState("");
    const handleDateChange = (name, date) => {
        dispatch(updateFilterFormData({ ...filterFormData, [name]: date }));
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        const filteredFormData = { ...filterFormData, [name]: value, skip: 0 }
        dispatch(updateFilterFormData(filteredFormData));
        dispatch(fetchItems(filteredFormData));
    };

    const handleOrderNumberChange = (e) => {
        const { name, value } = e.target;
        const filteredFormData = { ...filterFormData, [name]: value, skip: 0 }
        dispatch(updateFilterFormData(filteredFormData));
    };

    const handleSearch = () => {
        setShowMessage(false);
        dispatch(updateFilterFormData(filterFormData));
        dispatch(fetchItems(filterFormData));
    };

    const handleClear = () => {
        dispatch(resetFilterFormData());
        dispatch(clearFetchFilter());
        setShowMessage(false);
    };

    const handleExport = async (event) => {
        const elementName = event.currentTarget?.name
        if (elementName === 'btnExport' && (filterFormData.documentType == null || filterFormData.documentType === '')) {
            toast.warn("Select the order type to download the file.");
            return false;
        }
        else {
            const updatedFilterFormData = {
                ...filterFormData,
                skip: 0
            };
            const response = await dispatch(fetchExports(updatedFilterFormData));
            if (response.meta.requestStatus === 'fulfilled') {
                toast.info("Download in progress");
                setShowMessage(false);
                downloadExcel({ payload: response.payload, filename: 'Inbound_transaction_export.xlsx' });
                toast.success("Download Completed");
            } else {
                setMessageText("There are no records in the database for the provided inputs. Please review and attempt again.");
                setShowMessage(true);
            }
        }
    };

    const maxDate = new Date();

    return (
        <div>
        <div className="bg-white p-4 rounded-lg shadow-md mb-6 mx-5 flex items-center space-x-4">
            <div className="flex-1">
                <label className="block text-gray-700 mb-1 font-thin">Order Type</label>
                <select
                    name="documentType"
                    value={filterFormData.documentType ?? ""}
                    onChange={handleChange}
                    className="w-full p-2 border border-gray-300 rounded"
                >
                    <option value="">Select</option>
                    {Object.values(INOUND_TRANSACTION_ORDER_TYPE).map(type => (
                        <option key={type.id} value={type.key}>
                            {type.title}
                        </option>
                    ))}
                </select>
            </div>

            <div className="flex-1-">
                <label className="block text-gray-700 mb-1 font-thin">Order Number</label>
                <input
                    type="text"
                    name="orderNumber"
                    value={filterFormData.orderNumber ?? ""}
                    onChange={handleOrderNumberChange}
                    className="w-full p-2 border border-gray-300 rounded"
                />
            </div>

            <div className="flex-1 min-w-[150px]">
                <label className="block text-gray-700 mb-1 font-thin">Received After</label>
                <DatePicker className="w-full p-2 border border-gray-300 rounded" dateFormat="yyyy-MM-dd" selected={filterFormData.dateFrom} showIcon selectsStart startDate={filterFormData.dateFrom} endDate={filterFormData.dateTo} maxDate={maxDate} onChange={(date) => handleDateChange('dateFrom', date)} />
            </div>
            <div className="flex-1 min-w-[150px]">
                <label className="block text-gray-700 mb-1 font-thin">Received Before</label>
                <DatePicker className="min-w-[150px] flex w-full p-2 border border-gray-300 rounded min-w-[150px]" dateFormat="yyyy-MM-dd" showIcon selected={filterFormData.dateTo} selectsEnd startDate={filterFormData.dateFrom} minDate={filterFormData.dateFrom} maxDate={maxDate} endDate={filterFormData.dateTo} onChange={(date) => handleDateChange('dateTo', date)} />
            </div>

            <div className="flex space-x-2 mt-6">
                <button
                    onClick={handleSearch}
                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                >
                    Search
                </button>
                <button
                    onClick={handleClear}
                    className="bg-black text-white px-2 py-2 font-thin rounded hover:font-normal"
                >
                    Clear
                </button>
            </div>
            <div className="flex -1 mt-6">
                <button name="btnExport"
                    onClick={handleExport}
                    className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 flex items-center"
                >
                    <DownloadIcon />

                </button>
            </div>
        </div>
            {showMessage && (
                <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">
                    <span className="block sm:inline">{messageText}</span>
                    <button
                        onClick={() => setShowMessage(false)}
                        className="absolute top-0 bottom-0 right-0 px-4 py-3">
                        <span aria-hidden="true" className="text-red-500">&times;</span>
                    </button>
                </div>
            )
            }
        </div>
    );
};

export default InboundTransactionsSearchBar;
