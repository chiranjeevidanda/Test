import React from "react";


const TableTrSckeleton = ({ colSpan = 7 }) => {
  return (
    <>
      <tr>
        <td colSpan={colSpan + 1} className="px-6 py-4">
          <div role="status" className="max-w-full w-full animate-pulse">
            <div className="h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-48 mb-4"></div>
            <div className="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[360px] mb-2.5"></div>
            <div className="h-2 bg-gray-200 rounded-full dark:bg-gray-700 mb-2.5"></div>
          </div>
        </td>
      </tr>
      <tr>
        <td colSpan={colSpan + 1} className="px-6 py-4">
          <div role="status" className="max-w-full w-full animate-pulse">
            <div className="h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-48 mb-4"></div>
            <div className="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[360px] mb-2.5"></div>
            <div className="h-2 bg-gray-200 rounded-full dark:bg-gray-700 mb-2.5"></div>
          </div>
        </td>
      </tr>
      <tr>
        <td colSpan={colSpan + 1} className="px-6 py-4">
          <div role="status" className="max-w-full w-full animate-pulse">
            <div className="h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-48 mb-4"></div>
            <div className="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[360px] mb-2.5"></div>
            <div className="h-2 bg-gray-200 rounded-full dark:bg-gray-700 mb-2.5"></div>
          </div>
        </td>
      </tr>
    </>
  );
};

export default TableTrSckeleton;
