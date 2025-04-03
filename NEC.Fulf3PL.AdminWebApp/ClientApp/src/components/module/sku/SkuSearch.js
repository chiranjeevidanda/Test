import React from "react";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { toast } from 'react-toastify';
import { downloadExcel } from '../../../genaric/fns';
import DownloadIcon from '../../atom/DownloadIcon';
import { fetchItems, fetchExports, selectFilterFormData, updateFilterFormData } from '../../../features/skuItemMaster/SKUItemMasterSlice';
import AlertMessage from "../../common/alert/alertMessage"

const SKUSearchBar = () => {

    const dispatch = useDispatch();
    const filterFormData = useSelector(selectFilterFormData);

    const [showSKUSearchErrorMessage, setShowSKUSearchErrorMessage] = useState(false);
    const [sKUSearchErrorMessage, setSKUSearchTriggerErrorMessage] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        dispatch(updateFilterFormData({ ...filterFormData, [name]: value, skip: 0 }));
    };

    const handleSearch = async (event) => {
        dispatch(updateFilterFormData(filterFormData));
        const response = await dispatch(fetchItems(filterFormData));
        if (response.payload?.results?.length <= 0) {
            setShowSKUSearchErrorMessage(true);
            setSKUSearchTriggerErrorMessage("There are no records in the database. Please attempt again");
        }
    };

    const handleExport = async (event) => {
        toast.info("Download in progress");
        const updatedFilterFormData = {
            ...filterFormData,
            skip: 0
        };
        const response = await dispatch(fetchExports(updatedFilterFormData));
        if (response.meta.requestStatus === 'fulfilled') {
            downloadExcel({ payload: response.payload, filename: 'Outbound_transaction_export.xlsx' });
            toast.success("Download Completed");
        } else {
            toast.error("Download Failed");
        }
    };

    const handleClear = () => {
        const name = 'orderNumber'
        dispatch(updateFilterFormData({ ...filterFormData, [name]: "", skip: 0 }));
        dispatch(fetchItems({}));
    };

    return (
        <div className="flex-1 min-w-[150px]">
            <div className="p-2 flex items-center justify-left">
                <div className="container max-w-screen-lg ">
                    <h1 className="rounded text-1x1 font-bold">Search for SKU in Azure Database</h1>
                    <div className="bg-white rounded p-4 ">
                        <div className="lg:col-span-2">
                            <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 ">
                                <div className="md:col-span-3">
                                    <label className="block text-gray-700 mb-1 font-thin">SKU Id</label>
                                    <input placeholder="Enter value..."
                                        type="text"
                                        name="orderNumber"
                                        value={filterFormData.orderNumber ?? ""}
                                        onChange={handleChange}
                                        className="w-full p-2 border border-gray-300 rounded"
                                    />
                                    <small className="text-gray-500 mb-1">Multiple IDs should be seperated by comma or space</small>
                                    <div className="flex space-x-2 mt-1">
                                        <button name="btnSKUSearch"
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
                                        <button name="btnExport"
                                            onClick={handleExport}
                                            className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 flex items-center"
                                        >
                                            <DownloadIcon />

                                        </button>
                                    </div>
                                </div>
                            </div>
                            {showSKUSearchErrorMessage && <AlertMessage message={sKUSearchErrorMessage} type="error" />}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default SKUSearchBar;
