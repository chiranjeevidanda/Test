import React, { useEffect, useState } from "react";
import { CONTENT_TYPES } from "../../constents/constants";
import DataTable from "../../components/block/Table";
import { useDispatch, useSelector } from "react-redux";
import { updateFilterFormData, fetchItems, selectinboundTransactionsStatus, selectinboundTransactions, selectFilterFormData, selectPagination, } from "./InboundTransactionsSlice";
import LoadingCircle from '../../components/atom/LoadingCircle';
import ViewPage from "../../components/module/inbound/ViewPage";
import InboundTransactionsSearchBar from "../../components/module/inbound/InboundTransactionSearchBar";
import NoDataTableTr from "../../components/sckeletons/NoDataTableTr";
export function InboundTransactions() {
    const dispatch = useDispatch();

    const inboundTransactionsItems = useSelector(selectinboundTransactions);
    const inboundItems = {
        ...inboundTransactionsItems,
        results: inboundTransactionsItems.results ?? []
    };
    const inboundTransactionsStatus = useSelector(selectinboundTransactionsStatus);
    const filterFormData = useSelector(selectFilterFormData);
    const pagination = useSelector(selectPagination);
    const [viewData, setViewData] = useState(null);
    const [sortData, setSortData] = useState({ column: filterFormData.SortBy, direction: filterFormData.SortDir });
    const [showMessage, setShowMessage] = useState(false);

    const tableColumns = [        
        { index: "orderType", title: "Order Type", type: CONTENT_TYPES["text"], default: "" },
        { index: "id", title: "Event Id", type: CONTENT_TYPES["text"], default: "" },
        { index: "createdDate", title: "Received Timestamp", type: CONTENT_TYPES["date"], default: "" },
        { index: "orderNumber", title: "Order Number", type: CONTENT_TYPES["text"], default: "" },
        { index: "transactionStatus", title: "SAP Response", type: CONTENT_TYPES["text"], default: "" },
        { index: "webhookPayload", title: "Webhook Payload", type: CONTENT_TYPES["scrollableTextCell"], default: "" },
        { index: "sapConverterPayload", title: "Sap Converter Payload", type: CONTENT_TYPES["scrollableTextCell"], default: "" },
        { index: "sapBapiInput", title: "Sap Bapi Input", type: CONTENT_TYPES["scrollableTextCell"], default: "" },
        { index: "sapBapiResponse", title: "Sap Bapi Response", type: CONTENT_TYPES["scrollableTextCell"], default: "" },
        { index: "errorMessage", title: "Error Message", type: CONTENT_TYPES["scrollableTextCell"], default: "" },
    ];

    const tableFilterColumns = [];
    const tableSortColumns = ['orderType', 'id', 'createdDate', 'transactionStatus'];
    const tableHighlightedColumns = 'orderNumber';
    const handleSearch = (pageNo) => {
        const skip = pageNo * filterFormData.take;
        dispatch(updateFilterFormData({ ...filterFormData, page: pageNo, skip }));
        dispatch(fetchItems({ ...filterFormData, page: pageNo, skip }));
    };
    const handleSort = (sortData) => {
        const SortBy = sortData.column;
        const SortDir = sortData.direction;
        dispatch(updateFilterFormData({ ...filterFormData, SortBy: SortBy, SortDir: SortDir }));
        dispatch(fetchItems({ ...filterFormData, SortBy: SortBy, SortDir: SortDir }));
    };

    const viewHandle = (data) => {
        setViewData(data);
    }

    const closeView = () => {
        setViewData(null);
    }
    useEffect(() => {
        setSortData({ column: filterFormData.SortBy, direction: filterFormData.SortDir });
    }, [filterFormData.SortBy, filterFormData.SortDir]);

    useEffect(() => {
        dispatch(fetchItems({ ...filterFormData }));
    }, []);

    return (
        <div> <div className="p-2 bg-gray-100 flex-1">
            <h1 className="bg-white rounded p-4 text-2xl font-bold mb-4 pl-5">View Inbound Payloads</h1>
        </div>
            <div className="p-6 bg-gray-100 flex-1 inbound-view">
                <div className="row">
                    <InboundTransactionsSearchBar showMessage={showMessage} setShowMessage={setShowMessage} />
                </div>
                {
                    inboundItems.results.length > 0 && inboundTransactionsStatus !== 'loading'  &&
                        <DataTable
                            columns={tableColumns}                          
                            filters={tableFilterColumns}
                            getRecord={viewHandle}
                            pagination={pagination}
                            selectPageNo={handleSearch}
                            sortCall={handleSort}
                            sortedData={sortData}
                            sorts={tableSortColumns}
                            data={inboundTransactionsItems?.results}
                            highlightedText={filterFormData.orderNumber}
                            highlightColumn={tableHighlightedColumns}
                            //icons={true}
                            //isGetRecord={false}
                        />
                }
                {inboundTransactionsStatus == 'loading' && <LoadingCircle />}
                {viewData ? (
                    <div className="fixed z-10 inset-0 overflow-y-auto">
                        <div className="flex items-center justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">
                            <div className="fixed inset-0 transition-opacity" aria-hidden="true">
                                <div className="absolute inset-0 bg-gray-900 opacity-75"></div>
                            </div>
                            <span className="hidden sm:inline-block sm:align-middle sm:h-screen">&#8203;</span>
                            <div className="inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all" style={{ width: 'min-content' }}>
                                <div className="bg-white p-6">
                                    <ViewPage data={viewData} />
                                </div>
                                <div className="px-4 py-3 sm:px-6 sm:flex sm:flex-row-reverse">
                                    <button
                                        type="button"
                                        className="w-full inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-blue-600 text-base font-medium text-white hover:bg-blue-700 sm:ml-3 sm:w-auto sm:text-sm"
                                        onClick={closeView}
                                    >
                                        Close
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                ) : ''}

            </div >
            {!showMessage && inboundTransactionsItems?.results && inboundItems.results.length === 0 && <NoDataTableTr />}
        </div>
    );
}
