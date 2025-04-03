import React from 'react';

const ViewPage = ({ data }) => {
    const { createdDate, eventId, documentType, modifiedDate, status } = data;

    const formatDate = (dateString) => {
        const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
        return new Date(dateString).toLocaleDateString(undefined, options);
    };

    return (
        <div className="max-w-2xl mx-auto p-6 bg-white shadow-md rounded-lg">
            <h2 className="text-2xl font-bold mb-4">Document Details</h2>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Created Date:</label>
                <p className="text-gray-600">{formatDate(createdDate)}</p>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Event ID:</label>
                <p className="text-gray-600">{eventId}</p>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Document Type:</label>
                <p className="text-gray-600">{documentType}</p>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Modified Date:</label>
                <p className="text-gray-600">{formatDate(modifiedDate)}</p>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Status:</label>
                <p className="text-gray-600">{status}</p>
            </div>
        </div>
    );
};

export default ViewPage;
