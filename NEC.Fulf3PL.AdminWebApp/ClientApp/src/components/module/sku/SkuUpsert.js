import React from "react";
import { toast } from 'react-toastify';
import { useDispatch, useSelector } from "react-redux";
import { useState, useEffect } from "react";
import { submitSKU } from '../../../features/skuItemMaster/SKUItemMasterApi';
import { updateFilterFormData, selectFilterFormData } from '../../../features/skuItemMaster/SKUItemMasterSlice';
import AlertMessage from "../../common/alert/alertMessage"
import { SKU_CUSTOMER } from '../../../constents/constants';
const CreateSKU = () => {

    const dispatch = useDispatch();

    const filterFormData = useSelector(selectFilterFormData);

    const [showPayloadErrorMessage, setShowSKUErrorMessage] = useState(false);
    const [payloadErrorMessage, setSKUTriggerErrorMessage] = useState('');

    const [showPayloadSuccessMessage, setShowSKUSuccessMessage] = useState(false);
    const [payloadSuccessMessage, setSKUTriggerSuccessMessage] = useState('');

    const [showSkuProcessedMessage, setShowSkuProcessedMessage] = useState(false);
    const [paySkuProcessedMessage, setSkuProcessedMessage] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        dispatch(updateFilterFormData({ ...filterFormData, [name]: value, skip: 0 }));
    };

    const handleSave = (event) => {
        setShowSKUSuccessMessage(false);
        setShowSKUErrorMessage(false);
        setShowSkuProcessedMessage(false);
        const elementName = event?.target.getAttribute('name')
        if (elementName === 'btnSKUCreate' && (filterFormData.createOrderNumber == null || filterFormData.createOrderNumber === '')) {
            toast.warn("Please Enter SKU Id");
            return false;
        }
        else if  (elementName === 'btnSKUCreate' && (filterFormData.plant == null || filterFormData.plant === '')) {
            toast.warn("Please select customer");
            return false;
        }
        else {

            const updatedFilters = {
                ...filterFormData,
                documentId: filterFormData.createOrderNumber,
                customer:filterFormData.plant
            };

            submitSKU(updatedFilters).then((response) => {
                var data = response.data
                if (data.successCount === 0 && data.sentForProcess.length === 0 && data.skuProcessed.length === 0) {
                    setShowSKUErrorMessage(true);
                    setSKUTriggerErrorMessage("SKU not found. Please verify the SKU entered");
                }

                if (data.sentForProcess.length > 0) {
                    setShowSKUSuccessMessage(true);
                    setSKUTriggerSuccessMessage(`${response.data.sentForProcess.length} SKU sent successfully. Currently ${response.data.activeMessageCount} in queue to be processed`);
                }

                if (data.skuProcessed.length > 0) {
                    setShowSkuProcessedMessage(true);
                    setSkuProcessedMessage(`${response.data.skuProcessed.length} SKU already processed`);
                }

            }).catch((error) => {
                toast.error(error.message);
            });
        }
    };

    const handleClear = () => {
        const name = 'createOrderNumber'
        dispatch(updateFilterFormData({ ...filterFormData, [name]: "", plant: null, skip: 0 }));
    };

    return (
        <div className="flex-1 min-w-[150px]">
            <div className="p-2 flex items-center justify-left">
                <div className="container max-w-screen-lg ">
                    <h1 className="rounded text-1x1 font-bold">Send SKU Data to KTN</h1>
                    <div className="bg-white rounded p-4 ">
                        <div className="lg:col-span-2">
                            <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 ">
                                <div className="md:col-span-3">
                                    <label className="block text-gray-700 mb-1 font-thin">Enter SKU IDs</label>
                                    <input placeholder="Enter value..."
                                        type="text"
                                        name="createOrderNumber"
                                        value={filterFormData.createOrderNumber ?? ""}
                                        onChange={handleChange}
                                        className="w-full p-2 border border-gray-300 rounded"
                                    />
                                    <small className="text-gray-500 mb-1">Multiple IDs should be seperated by comma or space</small>
                                    <div className="flex-1">
                                        <label className="block text-gray-700 mb-1 font-thin">Customer</label>
                                        <select
                                            name="plant"
                                            value={filterFormData.plant ?? ""}
                                            onChange={handleChange}
                                            className="w-full p-2 border border-gray-300 rounded"
                                        >
                                            <option value="">Select</option>
                                            {Object.values(SKU_CUSTOMER).map(type => (
                                                <option key={type.id} value={type.key}>
                                                    {type.title}
                                                </option>
                                            ))}
                                        </select>
                                    </div>
                                    <div className="flex space-x-2 mt-1">
                                        <button name="btnSKUCreate"
                                            onClick={handleSave}
                                            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                                        >
                                            Create SKU
                                        </button>
                                        <button
                                            onClick={handleClear}
                                            className="bg-black text-white px-2 py-2 font-thin rounded hover:font-normal"
                                        >
                                            Clear
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    {showPayloadSuccessMessage && <AlertMessage message={payloadSuccessMessage} type="success" />}
                    {showSkuProcessedMessage && <AlertMessage message={paySkuProcessedMessage} type="warning" />}
                    {showPayloadErrorMessage && <AlertMessage message={payloadErrorMessage} type="error" />}
                </div>
            </div>
        </div>
    )
};

export default CreateSKU;
