import { ExclamationCircleIcon } from "@heroicons/react/24/outline";
import React, { useEffect, useState } from "react";

const DeleteConformaionModel = ({
  message = "",
  onClose,
  onDelete,
}) => {
  const [deleteLoading, setDeleteLoading] = useState(false);

  useEffect(() => {
    setDeleteLoading(false);
  }, []);
  return (
    <>
      {message ? (
        <div className="fixed inset-0 z-50 flex items-center justify-center">
          <div className="modal absolute top-0 left-0 bottom-0 right-0 bg-opacity-50 bg-gray-900 flex flex-col justify-center items-center">
            <div className="modal-content bg-white p-10 rounded-lg shadow-lg flex flex-col justify-center items-center md:h-3/6 md:w-1/3  h-1/4  w-2/3">
              <div>
                <ExclamationCircleIcon
                  className="mx-auto h-14 w-14 text-red-400 mb-4"
                  aria-hidden="true"
                />
                <p className="text-center">{message}</p>
                <div className="my-4 flex justify-center space-x-4">
                  <button
                    onClick={() => {
                      onDelete();
                      setDeleteLoading(true);
                    }}
                    className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
                  >
                    {deleteLoading ? (
                      <span v-if="isLoading" className="flex">
                        <svg
                          className="animate-spin h-5 w-5 mr-2"
                          viewBox="0 0 24 24"
                        >
                          <circle
                            className="opacity-25"
                            cx="12"
                            cy="12"
                            r="10"
                            stroke="currentColor"
                            strokeWidth="4"
                          ></circle>
                          <path
                            className="opacity-75"
                            fill="currentColor"
                            d="M4 12a8 8 0 018-8V2.83A10 10 0 002 12h2zm16 0a8 8 0 01-8 8V21.17A10 10 0 0022 12h-2z"
                          ></path>
                        </svg>
                        Loading...
                      </span>
                    ) : (
                      "Delete"
                    )}
                  </button>
                  <button
                    onClick={onClose}
                    className="px-4 py-2 bg-gray-300 text-gray-700 rounded hover:bg-gray-400"
                  >
                    Cancel
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      ) : (
        ""
      )}
    </>
  );
};

export default DeleteConformaionModel;
