import React, { useState } from 'react';

const AlertMessage = ({ message, type }) => {
    const [showAlert, setShowAlert] = useState(true);

    // Function to close the alert
    const closeAlert = () => {
        setShowAlert(false);
    };

    // Tailwind classes for success or error alerts
    let alertTypeClasses = type === 'success'
        ? 'bg-green-100 border-green-400 text-green-700'
        : 'bg-red-100 border-red-400 text-red-700';

    if (type === 'warning') {
        alertTypeClasses ="bg-orange-100 border-l-4 border-orange-400 text-orange-700"
    }

    return (
        showAlert && (
            <div
                className={`${alertTypeClasses} border px-2 mt-2 py-2 rounded relative flex justify-between items-center`}
                role="alert" style={{ maxWidth: '42rem'}}
            >
                <span className="text-sm">{message}</span>
                <button
                    type="button"
                    className={`${type === 'success' ? 'text-green-700 hover:text-green-900' : 'text-red-700 hover:text-red-900'}`}
                    aria-label="Close"
                    onClick={closeAlert}
                >
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 fill-current" viewBox="0 0 20 20" fill="currentColor">
                        <path
                            fillRule="evenodd"
                            d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                            clipRule="evenodd"
                        />
                    </svg>
                </button>
            </div>
        )
    );
};

export default AlertMessage;
