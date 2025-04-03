import { useState } from "react";

export default function Pagination({
  totalItems = 0,
  takeCount = 0,
  pageNo = 0,
  paginate = (pageNumber) => { },
}) {
  const [pgItems, setPgItems] = useState(null);

  const renderPageNumbers = () => {
    const totalPages = Math.ceil(totalItems / takeCount);
    const pageNumbersToShow = 5; // Number of page numbers to display (current page + 2 previous + 2 next)

    if (totalPages <= pageNumbersToShow) {
      // If total pages is less than or equal to the desired number of page numbers to show
      return Array.from(Array(totalPages).keys()).map((pageNumber) =>
        renderPageButton(pageNumber)
      );
    }

    let pageNumbers = [];

    if (pageNo < pageNumbersToShow - 1) {
      // If current page is within the first page numbers to show
      for (let i = 0; i < pageNumbersToShow; i++) {
        pageNumbers = [...pageNumbers, i];
      }
      pageNumbers.push("ellipsis", totalPages - 1); // Add ellipsis and last page number
    } else if (pageNo >= totalPages - (pageNumbersToShow - 1)) {
      // If current page is within the last page numbers to show
      pageNumbers.push(0, "ellipsis"); // Add first page number and ellipsis
      for (let i = totalPages - pageNumbersToShow; i < totalPages; i++) {
        pageNumbers.push(i);
      }
    } else {
      // If current page is in the middle
      const startIndex = pageNo - 2;
      const endIndex = pageNo + 2;

      pageNumbers.push(0, "ellipsis", startIndex); // Add first page number, ellipsis, and start index

      for (let i = startIndex + 1; i <= endIndex; i++) {
        pageNumbers.push(i);
      }

      pageNumbers.push("ellipsis", totalPages - 1); // Add ellipsis and last page number
    }

    return pageNumbers.map((pageNo, index) => {
      if (pageNo === "ellipsis") {
        return <li key={`ellipsis-${index}`}>...</li>;
      }
      return renderPageButton(pageNo);
    });
  };

  const renderPageButton = (pageNumber) => {
    const isActive = pageNumber === pageNo;

    return (
      <li key={pageNumber}>
        <button
          disabled={isActive}
          className={`px-3 py-1 mx-1 rounded-md ${isActive ? "bg-blue-500 text-white" : "bg-gray-300"
            }`}
          onClick={() => paginate(pageNumber)}
        >
          {pageNumber + 1}
        </button>
      </li>
    );
  };

  return (
    <div className="flex justify-end mt-4 mr-4">
      {totalItems > takeCount && (
        <ul className="flex">
          <> {renderPageNumbers()}</>
        </ul>
      )}
    </div>
  );
}
