"use client";

import {
    ArrowDownIcon,
    ArrowUpIcon,
    ArrowsUpDownIcon,
    XCircleIcon,
    FunnelIcon,
    EyeIcon,
    PencilIcon,
    TrashIcon,
    CheckCircleIcon,
    XMarkIcon,
    CheckIcon,
    PlusCircleIcon,
} from "@heroicons/react/24/outline";
import { Link, NavLink } from "react-router-dom";
import React, { useEffect, useRef, useState } from "react";
import TableTrSckeleton from "../sckeletons/TableTrSckeleton";
import {
    activateTableRowApi,
    deleteTableRowApi,
    getTableDataFromApi,
} from "../../apis";
import {
    ALERT_SUCCESS_ACTIVATE,
    ALERT_SUCCESS_DEACTIVATE,
    ALERT_TRY_AGAIN,
    CONTENT_TYPES,
    DEFAULT_DELETE_CONFIRM_MESSAGE,
    PAGINATION_PAGE,
    PAGINATION_SKIP,
    PAGINATION_TAKE,
    PAGINATION_TOTAL,
    SORT_DIRECTION_ASC,
    SORT_DIRECTION_DESC,
    SORT_DIRECTION_RESET,
    STATUS_COLUMN_INDEX,
    URL_BASE,
} from "../../constents/constants";
import Pagination from "./pagination";
import NoDataTableTr from "../sckeletons/NoDataTableTr";
import DeleteConformaionModel from "./DeleteConformaionModel";
import { toast } from "react-toastify";
import LoadingCircle from "../atom/LoadingCircle";
import ScrollableTextCell from "./ScrollableTextCell";

import { useLocation } from 'react-router-dom';

export default function DataTable({
    noDataMessage = "",
    columns = [""],
    data = [""],
    sorts = [""],
    filters = [""],
    apiUrl = "",
    activeOnly = false,
    pagination = {
    },
    sortedData = {},
    deleteCallCondition = (recordId) =>
        Promise.resolve({ status: true, message: "" }),
    sortCall = (SortData) => { },
    // filterCall = (SortData) => {}, 
    isGetRecord = false,
    getRecord = (data) => { },
    selectPageNo = (pageNo) => { },
    conditionResponce = { status: true, message: "" },
    icons = true,
    highlightedText = "",
    highlightColumn = ""
    }) {
    // Get the current URL path

    const location = useLocation();
    const pathname = location.pathname;

    // Split the current URL path into segments
    const urlSegments = pathname
        ?.split(URL_BASE)[1]
        ?.split("/")
        .filter((segment) => segment !== "");
    const currentPageUrl = urlSegments && urlSegments.length ? urlSegments[0] : "-";

    const VIEW_URL = `${URL_BASE}/${currentPageUrl}/view`;
    const EDIT_URL = `${URL_BASE}/${currentPageUrl}/edit`;
    // const DELETE_URL = `${URL_BASE}/${currentPageUrl}/#delete`;

    function classNames(classes = []) {
        return classes.filter(Boolean).join(" ");
    }

    //   const [loading, setLoading] = useState(true);
    const [sortOrder, setSortOrder] = useState({});
    const [sortColumn, setSortColumn] = useState("");
    const [customDeleteMessage, setCustomDeleteMessage] = useState("");
    const [deleteRecordId, setDeleteRecordId] = useState();
    const [activateRecordId, setActivateRecordId] = useState();
    const [filterColumns, setFilterColumns] = useState(
        {}
    );
    const [filterColumnsData, setFilterColumnsData] = useState({});
    const [tableData, setTableData] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [pageNo, setPageNo] = useState(PAGINATION_PAGE);
    const [totalItems, setTotalItems] = useState(PAGINATION_TOTAL);
    const [skipCount, setSkipCount] = useState(PAGINATION_SKIP);
    const [takeCount, setTakeCount] = useState(PAGINATION_TAKE);
    const [isOpen, setIsOpen] = useState({});
    const [showModal, setShowModal] = useState(false);

    const prevSkipRef = useRef(skipCount);

    // GET TABLE DATA FROM API

    const getTableRecordsFromApi = (sentData) => {
        debugger;
        getTableDataFromApi(sentData)
            .then((res) => {
               let filteredData = res?.results;
                if (filteredData.length >= 1) {
                    setTableData(filteredData);
                    setTotalItems(res?.total);
                    if (skipCount) {
                        setSkipCount(res?.skip);
                    }
                } else {
                    setTableData(data); debugger;
                    setTotalItems(res?.total);
                }
                setIsLoading(false);
            })
            .catch((error) => {
                setIsLoading(false);
                setTableData(data);
                console.error("Error:", error);
            });
    }

    // GET REQUEST DATA FUNCTION
    const getRequestData = () => {
        let resultData;
        if (activeOnly) {
            resultData = { active: true };
        }
        else {
            resultData = {};
        }
        Object.values(filterColumnsData).map((filters) => {
            if (filters.value) {
                resultData[filters.name] = filters.value;
            }
        });
        return resultData;
    };

    const genarateSortReqData = () => {
        const resultData = {};
        if (sortColumn && sortOrder[sortColumn] != SORT_DIRECTION_RESET) {
            resultData["sortBy"] = sortColumn;
            resultData["sortDir"] = sortOrder[sortColumn];
        }
        return resultData;
    };

    // GET TABLE DATA | SEARCH FUNCTION
    const getTableData = (reqData) => {
        // setIsLoading(true);
        if (apiUrl) {
            reqData.apiurl = apiUrl;
            let requestData = getRequestData();
            let requestSortData = genarateSortReqData();
            const sentData = {
                ...reqData,
                ...requestData,
                ...requestSortData,
                skip: skipCount,
                take: takeCount,
            };
            if (activeOnly) {
                getTableRecordsFromApi(sentData);
            } else {
                getTableRecordsFromApi(sentData);
            }

        }
    };

    useEffect(() => {
        getTableData({});
    }, [apiUrl]);
     
    const toggleDropdown = (
        e
    ) => {
        const index = e.currentTarget.dataset.columnindex ?? "";
        let openValue = isOpen && isOpen[index] ? !isOpen[index] : true;
        setIsOpen({ ...isOpen, [index]: openValue });
    };

    const getNextSortDirection = (sortBy, sortDir) => {
        if (sortBy && sortDir) {
            if (sortDir && sortDir === SORT_DIRECTION_ASC) {
                return SORT_DIRECTION_DESC;
            } else if (sortDir && sortDir === SORT_DIRECTION_DESC) {
                return SORT_DIRECTION_RESET;
            } else if (sortDir && sortDir === SORT_DIRECTION_RESET) {
                return SORT_DIRECTION_ASC;
            }
        }
    }

    const updateSortDirection = (sortBy, sortDir) => {

        if (sortBy && sortDir) {
            if (sortDir && sortDir === SORT_DIRECTION_ASC) {
                setSortOrder({ [sortBy]: SORT_DIRECTION_ASC });
            } else if (sortDir && sortDir === SORT_DIRECTION_DESC) {
                setSortOrder({ [sortBy]: SORT_DIRECTION_DESC });
            } else if (sortDir && sortDir === SORT_DIRECTION_RESET) {
                setSortOrder({ [sortBy]: SORT_DIRECTION_RESET });
            }
        }
    }

    const handleSortClick = async (
        event
    ) => {
        const target = event.target;
        const index = target.dataset.columnindex;
        const sortDir = target.dataset?.sortdir ?? SORT_DIRECTION_ASC;
        let selectedSortOrder = "";
        const sortData = {
            column: sortedData?.column ?? '',
            direction: sortedData?.direction ?? sortDir
        }

        // const sortedData = [...data];

        // console.log("index", index);
        if (index) {
            setSortColumn(index.toString());
            sortData.column = index.toString()

            // console.log("sortDir:", sortDir);
            if (sortDir && sortDir === SORT_DIRECTION_ASC) {
                sortData.direction = SORT_DIRECTION_ASC;
                setSortOrder({ [index]: SORT_DIRECTION_DESC });
                selectedSortOrder = SORT_DIRECTION_DESC;
            } else if (sortDir && sortDir === SORT_DIRECTION_DESC) {
                sortData.direction = SORT_DIRECTION_DESC;
                setSortOrder({ [index]: SORT_DIRECTION_RESET });
                selectedSortOrder = SORT_DIRECTION_RESET;
            } else if (sortDir && sortDir === SORT_DIRECTION_RESET) {
                sortData.direction = SORT_DIRECTION_RESET;
                setSortOrder({ [index]: SORT_DIRECTION_ASC });
                selectedSortOrder = SORT_DIRECTION_ASC;
            }
        }
        sortCall(sortData);

    };

    const sortElement = (index) => {
        return (
            <button
                type="button"
                className="btn-sort"
                data-columnindex={index}
                data-sortdir={getNextSortDirection(index, sortOrder[index])}
                onClick={handleSortClick}
            >
                {sortOrder && sortOrder[index] === SORT_DIRECTION_ASC ? (
                    <ArrowUpIcon className="relative -z-10 w-10 h-4" />
                ) : sortOrder && sortOrder[index] === SORT_DIRECTION_DESC ? (
                        <ArrowDownIcon className="relative -z-10 w-10 h-4" />
                ) : (sortOrder && sortOrder[index] === SORT_DIRECTION_RESET) ||
                    !sortOrder[index] ? (
                    <ArrowsUpDownIcon className="relative -z-10 w-10 h-4" />
                ) : (
                    ""
                )}
            </button>
        );
    };

    const filterHandle = (e) => {
        setFilterColumns({ ...filterColumns, [e.target?.name]: e.target.value });

        const filterData = {
            name: e.target?.name.toString(),
            value: e.target.value,
        };

        setFilterColumnsData({
            ...filterColumnsData,
            [e.target?.name.toString()]: filterData,
        });
    };

    const removeFilterHandle = (
        e
    ) => {
        const index = e.currentTarget.dataset.columnindex ?? "";
        const updatedFilterColumns = filterColumns;
        delete updatedFilterColumns[index];
        setFilterColumns(updatedFilterColumns);

        const updatedFilterColumnsData = filterColumnsData;
        delete updatedFilterColumnsData[index];
        setFilterColumnsData(filterColumnsData);

        getTableData({});
        setIsOpen({ ...isOpen, [index]: false });
    };

    const applyFilter = (event) => {
        const target = event.target;
        const index = target.dataset.columnindex;

        if (index) {
            setIsOpen({ ...isOpen, [index]: false });
            setSkipCount(PAGINATION_SKIP); // WHEN SKIP COUNT Change call getTableData({});
            setPageNo(PAGINATION_PAGE);
            // setSortColumn({});
            // setSortOrder({});

            if (skipCount == PAGINATION_SKIP) {
                getTableData({});
            }
        }
    };

    const filterDom = (index, domOptions) => {
        const popupPosition = domOptions?.position ?? "left";
        return (
            <>
                <div className="relative inline-block">
                    <button
                        type="button"
                        className="relative z-10 block w-full justify-center -gap-x-1.5 rounded-md bg-gray-50 p-1 text-xs font-semibold text-gray-900  hover:text-gray-500"
                        data-columnindex={index}
                        onClick={toggleDropdown}
                    >
                        {!isOpen[index] ? (
                            <FunnelIcon
                                className={`relative -z-10 -mr-1 h-4 w-4 text-gray-400 ${filterColumns[index]
                                    ? "text-gray-500 fill-black"
                                    : "text-gray-400"
                                    }`}
                                aria-hidden="true"
                            />
                        ) : (
                            <XCircleIcon className="relative -z-10 -mr-1 h-4 w-4 text-red-500 text-sm"></XCircleIcon>
                        )}
                    </button>

                    {isOpen && isOpen[index] && (
                        <ul
                            className={`absolute z-10 top-100 ${popupPosition}-0 w-40 py-1 bg-white border border-gray-300 rounded-md shadow`}
                        >
                            <li className="px-2 py-2">
                                <div className="flex items-center">
                                    <input
                                        id={`filter-${index}`}
                                        type="text"
                                        name={index}
                                        onChange={filterHandle}
                                        defaultValue={
                                            filterColumns && filterColumns[index]
                                                ? filterColumns[index]
                                                : ""
                                        }
                                        autoComplete="username"
                                        className="block border border-1 border-spacing-1 w-full rounded-md bg-transparent py-1.5 pl-1 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6"
                                        placeholder="Filter"
                                    />
                                </div>
                                <div className="flex flex-row items-end justify-end">
                                    {filterColumns && filterColumns[index] && (
                                        <button
                                            type="button"
                                            className="border-0 focus:ring-1 font-medium rounded-lg text-red-500 text-xs px-2 mr-2 hover:bg-gray-200 "
                                            data-columnindex={index}
                                            onClick={removeFilterHandle}
                                        >
                                            {" "}
                                            Clear
                                            {/* <XMarkIcon className="text-red-500 h-4 w-4"></XMarkIcon> */}
                                        </button>
                                    )}
                                    <button
                                        type="button"
                                        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-1 focus:ring-blue-100 font-medium rounded-lg text-xs px-2 py-1.5 mr-2 mt-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-200"
                                        data-columnindex={index}
                                        onClick={applyFilter}
                                    >
                                        Apply
                                    </button>
                                </div>
                            </li>
                        </ul>
                    )}
                </div>
            </>
        );
    };

    const getValuefromMultiDymentionObj = (data, index) => {
        // Initialize a reference to the data object
        let result = data;
        if (index) {
            // Split the index string into separate keys 
            const keys = index ? index.split(".") : null;
            // Iterate through the keys and access the nested property

            for (const key of keys) {
                if (result && result.hasOwnProperty(key)) {
                    result = result[key];
                } else {
                    result = undefined;
                    break;
                }
            }
        }
        return typeof result == "object" ? 'undefined' : result;
    };

    const makeTdByContentType = (column, rowDetails) => {
     const columnIndex = column?.index ? column?.index : "";
        const columnTitle = column?.title ? column?.title : "";
        const dd = CONTENT_TYPES;
        const columnType = column?.type
         ? column?.type
            : CONTENT_TYPES["text"];
        if (columnType === CONTENT_TYPES["text"]) {
            // Wildcard search highlighting functionality
            if (columnIndex === highlightColumn && highlightedText.endsWith('*')) {
                const cellValue = getValuefromMultiDymentionObj(rowDetails, column.index); // Get the cell value
                const highlightTerm = highlightedText.replace(/\*$/, ""); // Extract text before '*'

                const lowerCellValue = cellValue.toLowerCase();
                const lowerHighlightTerm = highlightTerm.toLowerCase();

                if (lowerCellValue.startsWith(lowerHighlightTerm)) {
                    return (
                        <td key={`td-text-${columnIndex}-${rowDetails.id}`} className="px-6 py-4">
                            <span>
                                <span className="highlight-color">{cellValue.substring(0, highlightTerm.length)}</span>
                                {cellValue.substring(highlightTerm.length)}
                            </span>
                        </td>
                    );
                } else {
                    return (
                        <td key={`td-text-${columnIndex}-${rowDetails.id}`} className="px-6 py-4">
                            {cellValue}
                        </td>
                    );
                }
            }  //Ends Wildcardsearch highlighting functionality
            else {
                return (
                    <td
                        key={`td-text-${columnIndex}-${rowDetails.id}`}
                        className="px-6 py-4"
                    >
                        {getValuefromMultiDymentionObj(rowDetails, column.index)}
                    </td>
                );
            }
        }
        else
            if (columnType === CONTENT_TYPES["scrollableTextCell"]) {
                return (
                    <td
                        key={`td-text-${columnIndex}-${rowDetails.id}`}
                        className="px-6 py-4 scrollable-cell"
                    >
                        <ScrollableTextCell text={getValuefromMultiDymentionObj(rowDetails, column.index)} />

                    </td>
                );
            } else if (columnType === CONTENT_TYPES["file"]) {
                return (
                    <td
                        key={`td-text-${columnIndex}-${rowDetails.id}`}
                        className="px-6 py-4"
                    >
                        {rowDetails[columnIndex].fileName}
                    </td>
                );
            } else if (columnType === CONTENT_TYPES["image"]) {
                var imageContent = "";

                let imageSrc = getValuefromMultiDymentionObj(
                    rowDetails,
                    column.index
                );

                if (imageSrc) {
                    imageContent = (
                        <img src={`${imageSrc}`} alt={columnTitle} width="50"></img>
                    );
                } else {
                    imageContent = <img src={imageSrc} alt={columnTitle} width="50"></img>;
                }
                return (
                    <td
                        scope="col"
                        className={`flext flex-col gap-1 px-6 py-4 font-medium text-gray-900 `}
                        key={`td-img-${columnIndex}-${rowDetails.id}`}
                    >
                        {imageContent}
                    </td>
                );
            } else if (columnType === CONTENT_TYPES["color"]) {
                const cellData = rowDetails[columnIndex];
                const formattedCellData =
                    cellData && cellData.startsWith("#") ? cellData : `#${cellData}`;

                return (
                    <td
                        key={`td-text-${columnIndex}-${rowDetails.id}`}
                        className="px-6 py-4"
                    >
                        <div
                            className={`w-8 h-8 mx-auto align-middle`}
                            style={{ backgroundColor: formattedCellData }}
                        />
                    </td>
                );
            } else if (columnType === CONTENT_TYPES["bool"]) {
                return (
                    <td
                        key={`td-text-${columnIndex}-${rowDetails.id}`}
                        className="px-6 py-4"
                    >
                        {rowDetails[columnIndex] ? (
                            <CheckIcon className="text-gray-500 w-5 h-5" />
                        ) : (
                            <XMarkIcon className="text-red-400 w-5 h-5" />
                        )}
                    </td>
                );
            }
    };

    const tableRow = (rowData) => {
        if (rowData) {
              return Object.values(columns).map((column) => {
                return makeTdByContentType(column, rowData);
            });
        }
    };

    const paginate = (pageNumber) => {
        const skipPage = pageNumber * takeCount;
        // console.log("skipPage", skipPage, pageNumber);

        setSkipCount(skipPage);
        setPageNo(pageNumber);
    };

    const activeRow = async (record) => {
        console.log("active?");
        setIsLoading(true);
        // Perform the actual delete operation here
        setActivateRecordId(record);
        console.log(activateRecordId);
        activateTableRowApi({ apiurl: apiUrl, data: record })
            .then((responce) => {
                // setActivateRecordId("");
                getTableData({});

                // Once deleted, close the modal
                toast.success(ALERT_SUCCESS_ACTIVATE + " ");
            })
            .catch((error) => {
                setActivateRecordId("");
                toast.error(ALERT_TRY_AGAIN);
            })
            .finally(() => {
                // setIsLoading(false);
            });
    };

    const deleteRow = async (recordId) => {
        setDeleteRecordId(recordId);
        setShowModal(true);
        setCustomDeleteMessage("");
        try {
            const conditionResponce = await deleteCallCondition(recordId);
            if (conditionResponce && conditionResponce["status"] == false) {
                //Modal Has been used
                setCustomDeleteMessage(conditionResponce["message"]);
            } else {
                setCustomDeleteMessage(DEFAULT_DELETE_CONFIRM_MESSAGE);
            }

            handleDeleteClick();
        } catch (error) {
            setDeleteRecordId("");
            console.error("Error in deleteCallCondition:", error);
            // Handle the error here
        }
    };

    const handleDeleteClick = () => {
        setShowModal(true);
    };

    const handleConfirmDelete = () => {
        // Perform the actual delete operation here
        deleteTableRowApi({ apiurl: apiUrl, id: deleteRecordId }).then(
            (responce) => {
                toast.success(ALERT_SUCCESS_DEACTIVATE + " ");
                getTableData({});

                // Once deleted, close the modal
                setDeleteRecordId(0);
                setCustomDeleteMessage("Deactivated.");
                setTimeout(() => {
                    setShowModal(false);
                }, 1000);
            }
        );
    };

    const handleCloseModal = () => {
        setShowModal(false);
    };

    useEffect(() => {
        if (sortedData || Object.keys(sortedData).length >= 1) {
            console.log(sortedData); //debugger;
            updateSortDirection(sortedData?.column, sortedData?.direction);
        }
    }, [sortedData]);

    useEffect(() => {
        setTableData({});
        getTableData({});
    }, [skipCount]);

    useEffect(() => {
        if (conditionResponce?.message) {
            // setCustomDeleteMessage(conditionResponce?.message);
        }
    }, [conditionResponce]);

    useEffect(() => {
        // if (data && tableData && data.toString() !== tableData.toString()) {
        if (data) {
            setTableData(data);
        }
    }, [data]);

    return (
        <div className="#max-h-[600px] #sm:max-h-[72vh] #md:max-h-[72vh] h-full overflow-y-auto rounded-md border shadow-sm m-5 md:pb-5 border-collapse bg-white text-left text-sm text-gray-500">
            <table className="min-w-full divide-y divide-gray-200">
                <thead className="sticky top-0 bg-gray-50">
                    <tr>
                        <th
                            key={"th1"}
                            scope="col"
                            className="w-3 min-w-[6vw] px-3 py-3 font-medium text-gray-900 whitespace-nowrap"
                        >
                            &nbsp;
                        </th>
                        {columns &&
                            columns.map((column, key) => {
                                const columnIndex = column?.index ? column?.index : "";
                                const columnTitle = column?.title ? column?.title : "";
                                const domOptions = {
                                    position:
                                        key == 0
                                            ? "left"
                                            : key >= Object.keys(columns).length - 1
                                                ? "right"
                                                : "",
                                };
                                return (
                                    <th
                                        scope="col"
                                        className={`flext flex-row flex-nowrap gap-1 px-3 py-3 font-medium text-gray-900 whitespace-nowrap`}
                                        key={`th-${columnIndex}`}
                                    >
                                        {columnTitle}
                                        {sorts &&
                                            sorts.includes(columnIndex) &&
                                            sortElement(columnIndex)}
                                        {filters &&
                                            filters.includes(columnIndex) &&
                                            filterDom(columnIndex, domOptions)}
                                    </th>
                                );
                            })}
                    </tr>
                </thead>
                <tbody className="divide-y divide-gray-100 border-t border-gray-100">
                    {tableData && Object.keys(tableData)?.length >= 1 ? (
                        Object.values(tableData).map((item, index) => {
                            const newLocal = "currentColor";
                            return (
                                <tr
                                    className="hover:bg-gray-50"
                                    key={`tr-${item.id}${item?.name || index}`}
                                >
                                    <td className="px-3 py-3 pl-6">
                                        <div className="flex justify-start gap-3">
                                            {isGetRecord && typeof getRecord === "function" ? (
                                                <>
                                                    {item.active ? (
                                                        <PlusCircleIcon
                                                            className="w-5 h-5 text-xs uppercase leading-normal cursor-pointer border-green-400 text-green-400"
                                                            data-te-toggle="tooltip"
                                                            data-te-placement="top"
                                                            data-te-ripple-init
                                                            data-te-ripple-color="light"
                                                            title="Select Row"
                                                            onClick={() => getRecord(item)}
                                                        />
                                                    ) : (
                                                        ""
                                                    )}
                                                </>
                                            ) : icons ? (
                                                <>
                                                    <button
                                                        x-data="{ tooltip: 'VIEW' }"
                                                    // href={`${VIEW_URL}/${item.id}`}
                                                    >
                                                        <EyeIcon
                                                            className="w-4 h-4 text-xs uppercase leading-normal"
                                                            data-te-toggle="tooltip"
                                                            data-te-placement="top"
                                                            data-te-ripple-init
                                                            data-te-ripple-color="light"
                                                            title="View"
                                                            onClick={() => getRecord(item)}
                                                        />
                                                    </button>
                                                    <NavLink
                                                        x-data="{ tooltip: 'Edit' }"
                                                        href={`${EDIT_URL}/${item.id}`}
                                                        className="hidden"
                                                    >
                                                        <PencilIcon
                                                            className="w-4 h-4 text-xs uppercase leading-normal cursor-pointer"
                                                            data-te-toggle="tooltip"
                                                            data-te-placement="top"
                                                            data-te-ripple-init
                                                            data-te-ripple-color="light"
                                                            title="Edit"
                                                        />
                                                    </NavLink>

                                                    {item[STATUS_COLUMN_INDEX] === true ? (
                                                        <button
                                                            x-data="{ tooltip: 'Delete' }"
                                                            // href={`${DELETE_URL}/${item.id}`}
                                                            onClick={() => deleteRow(item.id)}
                                                            className="hidden"
                                                        >
                                                            {showModal && deleteRecordId == item.id ? (
                                                                <div className="w-4 h-4 overflow-hidden">
                                                                    <LoadingCircle />
                                                                </div>
                                                            ) : (
                                                                <TrashIcon
                                                                    className="w-4 h-4 text-xs text-red-500 uppercase leading-normal"
                                                                    data-te-toggle="tooltip"
                                                                    data-te-placement="top"
                                                                    data-te-ripple-init
                                                                    data-te-ripple-color="light"
                                                                    title="Delete"
                                                                />
                                                            )}
                                                        </button>
                                                    ) : (
                                                        <button
                                                            x-data="{ tooltip: 'Delete' }"
                                                            // href={`${DELETE_URL}/${item.id}`}
                                                            onClick={() => activeRow(item)}
                                                            className="hidden"
                                                        >
                                                            {isLoading && activateRecordId == item?.id ? (
                                                                <div className="w-4 h-4 overflow-hidden">
                                                                    <LoadingCircle />
                                                                </div>
                                                            ) : (
                                                                <CheckCircleIcon
                                                                    className="w-4 h-4 text-xs text-green-500 uppercase leading-normal"
                                                                    data-te-toggle="tooltip"
                                                                    data-te-placement="top"
                                                                    data-te-ripple-init
                                                                    data-te-ripple-color="light"
                                                                    title="Activate"
                                                                />
                                                            )}
                                                        </button>
                                                    )}
                                                </>
                                            ) : ""
                                            }
                                        </div>
                                    </td>
                                    {tableRow(item)}
                                </tr>
                            );
                        })
                    ) : (
                        <>
                               {
                                    isLoading ? (
                                <TableTrSckeleton colSpan={Object.keys(columns).length} />
                            ) : (
                  
                            <NoDataTableTr
                                         
                                             noDataMessage= {noDataMessage}
                ></NoDataTableTr>
              )}
                        </>
                    )}
                </tbody>
            </table>

            {/* Render pagination */}

            <Pagination
                pageNo={pagination.pageNo}
                takeCount={pagination.takeCount}
                totalItems={pagination.totalItems}
                paginate={selectPageNo}
            ></Pagination>

            {/* Show the DeleteConfirmationModal if showModal is true */}
            {showModal && (
                <DeleteConformaionModel
                    message={customDeleteMessage}
                    onClose={handleCloseModal}
                    onDelete={handleConfirmDelete}
                ></DeleteConformaionModel>
            )}
        </div>
    );
}
