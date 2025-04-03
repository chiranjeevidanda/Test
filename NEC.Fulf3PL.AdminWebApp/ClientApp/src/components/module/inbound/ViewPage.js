import React from 'react';

const ViewPage = ({ data }) => {
    const { createdDate, orderType, webhookPayload, sapBapiInput, sapBapiResponse, orderNumber } = data;

    const formatDate = (dateString) => {
        const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
        return new Date(dateString).toLocaleDateString(undefined, options);
    };

    return (
        <div className="mx-auto p-6 bg-white shadow-md rounded-lg">
            <h2 className="text-2xl font-bold mb-4">Document Details</h2>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Received Timestamp:</label>
                <p className="text-gray-600">{formatDate(createdDate)}</p>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Order Number:</label>
                <p className="text-gray-600">{orderNumber}</p>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Order Type:</label>
                <p className="text-gray-600">{orderType}</p>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Webhook Payload:</label>
                <p className="text-gray-600">{webhookPayload}</p>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Sap Bapi Input:</label>
                <p className="text-gray-600">{sapBapiInput}</p>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Sap Bapi Response:</label>
                <p className="text-gray-600">{sapBapiResponse}</p>
            </div>
        </div>
    );
};

export default ViewPage;
