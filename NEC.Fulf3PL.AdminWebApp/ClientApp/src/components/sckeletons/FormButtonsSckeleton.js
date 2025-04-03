import React from "react";
import LoadingCircle from "../attom/LoadingCircle";

const FormButtonsSckeleton = () => {
  return (
    <div
      role="status"
      className="grid grid-cols-3 gap-3 max-w-max md:justify-end sm:justify-center my-2 relative"
    >
      <div className="h-10 bg-gray-200 rounded-md dark:bg-gray-700 w-16 "></div>
      <div className="h-10 bg-gray-200 rounded-md dark:bg-gray-700 max-w-[360px] "></div>
      <div className="h-10 bg-gray-200 rounded-md dark:bg-gray-700 "></div>
      <div
        role="status"
        className="absolute -translate-x-1/2 -translate-y-1/2 top-2/4 left-1/2"
      >
        <LoadingCircle />
        <span className="sr-only">Loading...</span>
      </div>
    </div>
  );
};

export default FormButtonsSckeleton;
