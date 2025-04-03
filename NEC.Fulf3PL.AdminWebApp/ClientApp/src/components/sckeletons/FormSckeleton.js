import React from "react";

const FormSkeleton = () => {
  return (
    <div
      className="page-container relative isolate rounded-lg border border-gray-200 shadow-md m-5 md:p-10 sm:px-5
       bg-white "
      aria-hidden="true"
    >
      <form>
        <div className="grid grid-cols-3 gap-4">
          {Array.from(Array(12).keys()).map((index) => (
            <div key={index}>
              <div className="w-6/12 h-3 mb-3 bg-gray-200 rounded"></div>
              <div className="w-full h-6 bg-gray-200 rounded"></div>
            </div>
          ))}
        </div>
      </form>
    </div>
  );
};

export default FormSkeleton;
