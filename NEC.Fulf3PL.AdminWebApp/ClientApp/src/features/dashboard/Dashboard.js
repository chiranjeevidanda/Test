import React, { useEffect, useState } from "react";
import DataTable from "../../components/block/Table";
import { useDispatch, useSelector } from "react-redux";
import LoadingCircle from '../../components/atom/LoadingCircle';
import { CONTENT_TYPES } from "../../constents/constants";
import { selectInboundDashbordItems, selectDashbordStatus, selectPagination, fetchInboundTransaction } from '../../features/dashboard/dashboardSlice'
import ProcessingStatusTable from "../../components/module/Dashboard/ProcessingStatusTable";
export function Dashboard() {

    const dispatch = useDispatch();
    const inboundTransaction = useSelector(selectInboundDashbordItems);
    const dashbordStatus = useSelector(selectDashbordStatus);
    const pagination = useSelector(selectPagination);

    const [setViewData] = useState(null);

    const viewIcons = false;
    const tableSortColumns = [];
    const tableFilterColumns = [];

    var inboundTableColumns = [
        { index: 0, title: "Current Processing Status", type: CONTENT_TYPES["date"], default: "" }
    ];

    const viewHandle = (data) => {
        setViewData(data);
    }

    useEffect(() => {
        dispatch(fetchInboundTransaction({}));

        const interval = setInterval(() => {
            dispatch(fetchInboundTransaction({}));
        }, 300000);

        return () => clearInterval(interval);
    }, [dispatch]);

    return (
        <div>
            <div className="p-2 bg-gray-100 flex-1">
                <h1 className="bg-white rounded p-4 text-2xl font-bold mb-4 pl-5">Dashboard</h1>
            </div>
            <div className="p-2 bg-gray-100 flex-1">
                <h3 className="text-2xl font-bold mb-4 pl-5">Inbound Transactions into SAP</h3>
                <div className="row">
                    <div className="p-2 flexitems-center justify-left">
                        <div className="container max-w-screen-lg ">
                            <div className="rounded">
                                <div className="grid gap-3 gap-y-1 text-sm grid-cols-1 lg:grid-cols-1">
                        {
                            dashbordStatus !== 'loading' ? (
                                <ProcessingStatusTable statuses={inboundTransaction?.results} />
                            ) : <LoadingCircle />
                        }
                    </div>
                    </div>
                    </div>
                    </div>                
                </div>
            </div >
        </div >
    );
}
