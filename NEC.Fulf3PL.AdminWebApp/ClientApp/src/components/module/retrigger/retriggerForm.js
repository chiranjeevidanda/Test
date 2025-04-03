import { RETRIGGER_TRANSACTION_ORDER_TYPE, } from '../../../constents/constants';
import React, { useState, useEffect } from "react";
import { submitRetriggerDocument, fetchRetriggerPayload } from "../../../features/retrigger/retriggerApi";
import { toast } from 'react-toastify';
import DatePicker from "react-datepicker";
import AlertMessage from "../../common/alert/alertMessage"

const RetriggerForm = ({ formType }) => {

    const [filterFormData, setFilterFormData] = useState({
        documentType: null,
        singleDate: new Date(),
        dateFrom: new Date(Date.now() - 7 * 24 * 60 * 60 * 1000),
        dateTo: new Date(),
        payload: null,
        eventId: null,
        isSingleDate: null
    });

    const handleDateChange = (name, date) => {
        setFilterFormData({ ...filterFormData, [name]: date });
    };

    const handleRetriggerPayload = (name, payload) => {
        setFilterFormData({ ...filterFormData, [name]: payload });
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFilterFormData({ ...filterFormData, [name]: value });
    };

    const [showEventIdErrorMessage, setShowEventIdErrorMessage] = useState(false);
    const [eventIdErrorMessage, setEventIdTriggerErrorMessage] = useState('');

    const [showEventIdSuccessMessage, setShowEventIdSuccessMessage] = useState(false);
    const [eventIdSuccessMessage, setEventIdTriggerSuccessMessage] = useState('');

    const [showPayloadErrorMessage, setShowPayloadErrorMessage] = useState(false);
    const [payloadErrorMessage, setPayloadTriggerErrorMessage] = useState('');

    const [showPayloadSuccessMessage, setShowPayloadSuccessMessage] = useState(false);
    const [payloadSuccessMessage, setPayloadTriggerSuccessMessage] = useState('');

    const [showDateRngeSuccessMessage, setShowDateRangeSuccessMessage] = useState(false);
    const [showDateRngeErrorMessage, setShowDateRangeErrorMessage] = useState(false);

    const [dateRngetriggerSuccessMessage, setDateRangeTriggerSuccessMessage] = useState('');
    const [dateRngetriggerErrorMessage, setDateRangeTriggerErrorMessage] = useState('');


    const handleSave = (event) => {
        
        setShowEventIdSuccessMessage(false);
        setShowEventIdErrorMessage(false);


        setShowPayloadSuccessMessage(false);

        setShowDateRangeSuccessMessage(false);
        setShowDateRangeErrorMessage(false);

        if (validateTriggerForm(event)) {

            const elementName = event?.target.getAttribute('name');

            const updatedFilterFormData = {
                ...filterFormData,
                isSingleDate: 'btnDate' === elementName ? true : false,
            };

            submitRetriggerDocument(updatedFilterFormData).then((response) => {
                const elementName = event?.target.getAttribute('name')
                let successMessage = '';
                if (response.data.successCount <= 0) {
                    if (elementName === 'btnEventId') {
                        setEventIdTriggerErrorMessage("There are no failed records in the database for the provided inputs. Please review and attempt again")
                        setShowEventIdErrorMessage(true);
                    } else if (elementName === 'btnDate' || elementName === 'btnDate2') {
                        setDateRangeTriggerErrorMessage("There are no failed records in the database for the provided inputs. Please review and attempt again")
                        setShowDateRangeErrorMessage(true);
                    } else if (elementName === 'btnModify') {
                        setShowPayloadErrorMessage(true);
                        setPayloadTriggerErrorMessage("Failed to send modified payload for reprocessing")
                    }
                } else {

                    if (response.data.payloadType == "InboundRequest")
                    {
                        successMessage = `${response.data.successCount} failed payloads sent successfully. Currently ${response.data.activeMessageCount} in queue to be processed`
                     
                    }
                    else {
                        successMessage = `${response.data.successCount} failed payloads sent successfully.`
                    }
        if (elementName === 'btnEventId' && (filterFormData.eventId != null && filterFormData.eventId !== '')) {
            setEventIdTriggerSuccessMessage(successMessage);
            setShowEventIdSuccessMessage(true);

        } else if (elementName === 'btnDate' && (filterFormData.singleDate !== null)) {
            setDateRangeTriggerSuccessMessage(successMessage);
            setShowDateRangeSuccessMessage(true);
        }
        else if (elementName === 'btnDate2' && (filterFormData.dateFrom != null && filterFormData.dateTo !== '')) {
            setDateRangeTriggerSuccessMessage(successMessage);
            setShowDateRangeSuccessMessage(true);
        }
        else if (elementName === 'btnModify' && (filterFormData.payload != null && filterFormData.payload !== '')) {
            var responseMessage =`Payloads sent successfully. Currently ${response.data.activeMessageCount} in queue to be processed`
            setPayloadTriggerSuccessMessage(responseMessage);
            setShowPayloadSuccessMessage(true);
        }

        handleClear();
    }
}).catch((error) => {
    toast.error(error.message);
});
        }
    };

const validateTriggerForm = (event) => {
    const elementName = event?.target.getAttribute('name')
    if (elementName === 'btnEventId' && (filterFormData.eventId == null || filterFormData.eventId === '')) {
        toast.warn("Please Enter Event Id");
        return false;
    } else if (elementName === 'btnDate' && (filterFormData.documentType == null || filterFormData.singleDate === null)) {
        toast.warn("Please Select Order Type and Single Date");
        return false;

    }
    else if (elementName === 'btnDate2' && (filterFormData.documentType == null || filterFormData.dateFrom == null || filterFormData.dateTo === '')) {
        toast.warn("Please Select Order Type, Start and End Date");
        return false;
    }

    else if (elementName === 'btnModify' && (filterFormData.payload == null || filterFormData.payload === '')) {
        toast.warn("Please Provide Event Id and Payload");
        return false;
    }

    else if (elementName === 'btnModify' && (filterFormData.eventId != null && (filterFormData.payload != null && filterFormData.payload !== '') && !isValidPayloadFormat(filterFormData.payload))) {
        toast.warn("Please Provide valid payload format");
        return false;
    }
    return true;
}
function isValidPayloadFormat(jsonString: string): boolean {
    try {
        JSON.parse(jsonString);
        return true;
    } catch {
        return false;
    }
}

const loadRetriggerPayload = (event) => {
    setShowPayloadErrorMessage(false);
    const elementName = event?.target.getAttribute('name')
    if (elementName === 'btnLoadPayload' && (filterFormData.eventId == null || filterFormData.eventId === '')) {
        toast.warn("Please Enter Event Id");
        return;
    } else {
        setShowPayloadErrorMessage(false);
        fetchRetriggerPayload(filterFormData.eventId).then((response) => {
            handleClear();
            handleRetriggerPayload('payload', response.requestPayload);
        }).catch((error) => {
            handleRetriggerPayload('payload', '');
            setShowPayloadErrorMessage(true);
            setPayloadTriggerErrorMessage("Could not find the payload with Event Id " + filterFormData.eventId)
        });
    }
};

const handleClear = () => {
    setFilterFormData({
        documentType: null,
        singleDate: new Date(),
        dateFrom: new Date(Date.now() - 7 * 24 * 60 * 60 * 1000),
        dateTo: new Date(),
        payload: null,
        eventId: null,
        isSingleDate: null
    });
};

const maxDate = new Date();

return (
    <>
        {
            formType == 'event' ? (
                <div className="p-2 flex items-center justify-left">
                    <div className="container max-w-screen-lg ">
                        <div className="bg-white rounded p-4 ">
                            <div className="grid gap-2 gap-y-2 text-sm grid-cols-1 lg:grid-cols-2">
                                <div className="lg:col-span-2">
                                    <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 md:grid-cols-5">

                                        <div className="md:col-span-3">
                                            <label >Enter Event IDs</label>
                                            <input placeholder="Enter value..."
                                                type="text"
                                                name="eventId"
                                                value={filterFormData.eventId ?? ""}
                                                onChange={handleChange}
                                                className="w-full p-2 border border-gray-300 rounded"
                                            />
                                            <p className="text-gray-500 mb-1">Multiple IDs should be seperated by comma or space</p>

                                            <div className="flex space-x-2 mt-1">
                                                <button name='btnEventId'
                                                    onClick={handleSave}
                                                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                                                >
                                                    Trigger
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    {showEventIdSuccessMessage && <AlertMessage message={eventIdSuccessMessage} type="success" />}
                                    {showEventIdErrorMessage && <AlertMessage message={eventIdErrorMessage} type="error" />}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            ) :
                null
        }

        {
            formType == 'date' ? (
                <div className="p-2 flex items-center justify-left">
                    <div className="container max-w-screen-lg ">
                        <div className="bg-white rounded p-4 ">
                            <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 lg:grid-cols-3">
                                <div className="lg:col-span-1">
                                    <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 md:grid-cols-5">
                                        <div className="md:col-span-5">
                                            <label className="block text-gray-700 mb-1 font-thin" htmlFor="documentType">Select Type</label>
                                            <select
                                                name="documentType"
                                                value={filterFormData.documentType ?? ""}
                                                onChange={handleChange}
                                                className="w-full p-2 border border-gray-300 rounded"
                                            >
                                                <option value="">Select</option>
                                                {Object.values(RETRIGGER_TRANSACTION_ORDER_TYPE).map(type => (
                                                    <option key={type.id} value={type.key}>
                                                        {type.title}
                                                    </option>
                                                ))}
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 lg:grid-cols-3">
                                <div className="lg:col-span-1 mt-2">
                                    <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 md:grid-cols-5">

                                    </div>
                                </div>
                            </div>
                            <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 lg:grid-cols-3">
                                <div className="lg:col-span-4 mt-2">
                                    <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 md:grid-cols-6">
                                        <div className="md:col-span-2">
                                            <label>Single Date</label>
                                            <DatePicker className="w-full p-2 border border-gray-300 rounded" dateFormat="yyyy-MM-dd" selected={filterFormData.singleDate} showIcon selectsStart startDate={filterFormData.singleDate} maxDate={maxDate} onChange={(date) => handleDateChange('singleDate', date)} />
                                            <div className="flex space-x-2 mt-1">
                                                <button name='btnDate'
                                                    onClick={handleSave}
                                                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                                                >
                                                    Trigger Date
                                                </button>
                                            </div>
                                        </div>
                                        <div className="md:col-span-2">
                                            <label>Start Date</label>
                                            <DatePicker className="w-full p-2 border border-gray-300 rounded" dateFormat="yyyy-MM-dd" selected={filterFormData.dateFrom} showIcon selectsStart startDate={filterFormData.dateFrom} endDate={filterFormData.dateTo} maxDate={maxDate} onChange={(date) => handleDateChange('dateFrom', date)} />
                                            <div className="flex space-x-2 mt-1">
                                                <button name='btnDate2'
                                                    onClick={handleSave}
                                                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                                                >
                                                    Trigger Range
                                                </button>
                                            </div>
                                        </div>
                                        <div className="md:col-span-2">
                                            <label>End Date</label>
                                            <DatePicker className="min-w-[150px] flex w-full p-2 border border-gray-300 rounded min-w-[150px]" dateFormat="yyyy-MM-dd" showIcon selected={filterFormData.dateTo} selectsEnd startDate={filterFormData.dateFrom} minDate={filterFormData.dateFrom} maxDate={maxDate} endDate={filterFormData.dateTo} onChange={(date) => handleDateChange('dateTo', date)} />
                                        </div>
                                    </div>
                                    <div className="lg:col-span-4  mt-1">
                                        <div className="md:col-span-2 text-left">

                                        </div>
                                    </div>
                                </div>
                            </div>
                            {showDateRngeSuccessMessage && <AlertMessage message={dateRngetriggerSuccessMessage} type="success" />}
                            {showDateRngeErrorMessage && <AlertMessage message={dateRngetriggerErrorMessage} type="error" />}
                        </div>

                    </div>
                </div>
            ) :
                null
        }

        {
            formType == 'modify' ? (
                <div className="p-2 flex items-center justify-left">
                    <div className="container max-w-screen-lg ">
                        <div className="bg-white rounded p-4">
                            <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 lg:grid-cols-3">
                                <div className="lg:col-span-4 mt-2">
                                    <div className="grid gap-4 gap-y-2 text-sm grid-cols-1 md:grid-cols-5">
                                        <div className="md:col-span-2">
                                            <label htmlFor="eventId">Enter Event ID</label>
                                            <input required placeholder="Enter value..."
                                                type="text"
                                                name="eventId"
                                                value={filterFormData.eventId ?? ""}
                                                onChange={handleChange}
                                                className="w-full p-2 border border-gray-300 rounded"
                                            />

                                            <div className="flex space-x-2 mt-1">
                                                <button name="btnLoadPayload"
                                                    onClick={loadRetriggerPayload}
                                                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                                                >
                                                    Load Payload
                                                </button>
                                            </div>
                                        </div>
                                        <div className="md:col-span-3">
                                            <label htmlFor="payload">Payload</label>
                                            <textarea rows="5" placeholder="Enter value..."

                                                name="payload"
                                                value={filterFormData.payload ?? ""}
                                                onChange={handleChange}
                                                className="w-full p-2 border border-gray-300 rounded"
                                            />
                                            <div className="flex space-x-2 mt-1">
                                                <button name='btnModify'
                                                    onClick={handleSave}
                                                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                                                >
                                                    Save & Trigger Payload
                                                </button>
                                            </div>

                                        </div>
                                    </div>
                                    {showPayloadSuccessMessage && <AlertMessage message={payloadSuccessMessage} type="success" />}
                                    {showPayloadErrorMessage && <AlertMessage message={payloadErrorMessage} type="error" />}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            ) :
                null
        }
    </>

);
};

export default RetriggerForm;
