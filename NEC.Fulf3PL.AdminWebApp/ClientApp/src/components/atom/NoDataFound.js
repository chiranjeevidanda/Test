// NoDataFound.js
import React from 'react';

const NoDataFound = () => {
  return (
    <div className="flex flex-col items-center justify-center h-full p-4">
      <svg
        className="w-16 h-16 text-gray-500"
        fill="none"
        stroke="currentColor"
        viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth="2"
          d="M20 13V5a2 2 0 00-2-2H6a2 2 0 00-2 2v8M9 21h6m-3-3v3M5 21h14a2 2 0 002-2v-5a2 2 0 00-2-2H5a2 2 0 00-2 2v5a2 2 0 002 2z"
        ></path>
      </svg>
      <h2 className="mt-4 text-2xl font-semibold text-gray-700">No Data Found</h2>
      <p className="mt-2 text-gray-500">
        Sorry, we couldn't find any data to display. Please try again later.
      </p>
      <button
        className="mt-6 px-4 py-2 bg-blue-600 text-white font-semibold rounded-md hover:bg-blue-700 focus:outline-none"
      >
        Refresh
      </button>
    </div>
  );
};

export default NoDataFound;
