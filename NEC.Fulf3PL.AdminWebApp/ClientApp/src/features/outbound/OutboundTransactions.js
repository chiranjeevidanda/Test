import React, { useEffect, useState } from "react";
import { CONTENT_TYPES, OUTBOUND_TRANSACTION_ORDER_TYPE } from "../../constents/constants";
import DataTable from "../../components/block/Table";
import { useDispatch, useSelector } from "react-redux";
import { updateFilterFormData, fetchItems, selectoutboundTransactionsStatus, selectoutboundTransactions, selectFilterFormData, selectPagination, } from "./OutboundTransactionsSlice";
import LoadingCircle from '../../components/atom/LoadingCircle';
import OutboundTransactionsSearchBar from "../../components/module/outbound/OutboundTransactionSearchBar";
import NoDataTableTr from "../../components/sckeletons/NoDataTableTr";

export function OutboundTransactions() {
    const dispatch = useDispatch();

    const outboundTransactionsItems = useSelector(selectoutboundTransactions);
    const outboundItems = {
        ...outboundTransactionsItems,
        results: outboundTransactionsItems.results ?? []
    };

    const outboundTransactionsStatus = useSelector(selectoutboundTransactionsStatus);
    const filterFormData = useSelector(selectFilterFormData);
    const pagination = useSelector(selectPagination);
    const [viewData, setViewData] = useState(null);
    const [sortData, setSortData] = useState({ column: filterFormData.SortBy, direction: filterFormData.SortDir });
    const [showMessage, setShowMessage] = useState(false);
    var tableColumns = [
        { index: "documentType", title: "Order Type", type: CONTENT_TYPES["text"], default: "" },
        { index: "modifiedDate", title: "Time Stamp", type: CONTENT_TYPES["date"], default: "" },
        { index: "eventId", title: "Event ID", type: CONTENT_TYPES["text"], default: "" },
        { index: "customer", title: "Customer", type: CONTENT_TYPES["text"], default: "" },
        { index: "documentId", title: "PO #", type: CONTENT_TYPES["text"], default: "" },
        { index: "status", title: "KTN Response", type: CONTENT_TYPES["text"], default: "" },
        { index: "systemId", title: "System Id", type: CONTENT_TYPES["text"], default: "" },
        { index: "requestData", title: "KTN Payload", type: CONTENT_TYPES["scrollableTextCell"], default: "" },
        { index: "containerNumber", title: "Container Number", type: CONTENT_TYPES["text"], default: "" },
        { index: "errorMessage", title: "Error Message", type: CONTENT_TYPES["scrollableTextCell"], default: "" },
    ];

    const selectedOrderType = OUTBOUND_TRANSACTION_ORDER_TYPE;

    if (filterFormData.documentType === selectedOrderType.SKUCreate.key) {
        tableColumns[4].title = "Product Code"
    } else if (filterFormData.documentType === selectedOrderType.POCreate.key) {
        tableColumns[4].title = "PO #"
    }
    else if (filterFormData.documentType === selectedOrderType.OrderCreate.key || selectedOrderType.ReturnOrder.key) {
        tableColumns[4].title = "Delivery Number"
            }
    if (filterFormData.documentType !== selectedOrderType.POCreate.key) {
        tableColumns = tableColumns.filter((column, index) => index !== 8);
    }

    const viewIcons = false;
    const tableFilterColumns = [];
    const tableSortColumns = ['documentType', 'modifiedDate', 'eventId', 'customer', 'documentId', 'status'];
    const tableHighlightedColumns = 'documentId';
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
    useEffect(() => {
        setSortData({ column: filterFormData.SortBy, direction: filterFormData.SortDir });
    }, [filterFormData.SortBy, filterFormData.SortDir]);

    const viewHandle = (data) => {
        setViewData(data);
    }

    return (
        <div>
            <div className="p-2 bg-gray-100 flex-1">
                <h1 className="bg-white rounded p-4 text-2xl font-bold mb-4 pl-5">View Outbound Payloads</h1>
            </div>
            <div className="p-6 bg-gray-100 flex-1  outbound-view">
                <div className="row">
                    <OutboundTransactionsSearchBar showMessage={showMessage}  setShowMessage={setShowMessage} />
                </div>
                {
                    outboundItems.results.length > 0 && outboundTransactionsStatus != 'loading' &&
                    <DataTable
                        columns={tableColumns}
                        filters={tableFilterColumns}
                        getRecord={viewHandle}
                        pagination={pagination}
                        selectPageNo={handleSearch}
                        sortCall={handleSort}
                        sortedData={sortData}
                        sorts={tableSortColumns}
                        data={outboundTransactionsItems?.results}
                        icons={viewIcons}
                        highlightedText={filterFormData.orderNumber}
                        highlightColumn={tableHighlightedColumns}
                    />

                }
                {outboundTransactionsStatus == 'loading' && <LoadingCircle />}
            </div >
            {!showMessage && outboundTransactionsItems?.results && outboundItems.results.length === 0 && <NoDataTableTr />}
        </div>
    );
}
