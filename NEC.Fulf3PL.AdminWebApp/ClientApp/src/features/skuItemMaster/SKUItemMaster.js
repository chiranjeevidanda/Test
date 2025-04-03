import React, { useEffect, useState } from "react";
import { CONTENT_TYPES } from "../../constents/constants";
import DataTable from "../../components/block/Table";
import { useDispatch, useSelector } from "react-redux";
import { updateFilterFormData, fetchItems, selectskuItemMaster, selectFilterFormData, selectPagination, selectskuItemMasterStatus } from "./SKUItemMasterSlice";
import CreateSKU from "../../components/module/sku/SkuUpsert";
import SKUSearchBar from "../../components/module/sku/SkuSearch";
import LoadingCircle from '../../components/atom/LoadingCircle';

export function SKU() {

    const dispatch = useDispatch();

    const sKUsItems = useSelector(selectskuItemMaster);
    const skuItemMasterStatus = useSelector(selectskuItemMasterStatus);
    const processedSKUsItems = {
        ...sKUsItems,
        results: sKUsItems.results ?? []
    };
    
    const filterFormData = useSelector(selectFilterFormData);
    const pagination = useSelector(selectPagination);

    const [viewData, setViewData] = useState(null);
    const [sortData, setSortData] = useState({ column: filterFormData.SortBy, direction: filterFormData.SortDir });

    const tableColumns = [
        { "index": "modifiedDate", "title": "Timestamp", "type": CONTENT_TYPES["date"], "default": "" },
        { "index": "status", "title": "Status", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.customer", "title": "Customer", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.code", "title": "SKU Id", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.description", "title": "Description", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.stockKeepingUnit", "title": "Stock Keeping Unit", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.itemGroup", "title": "Item Group", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.codeOfGoods", "title": "Code of Goods", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.reference1", "title": "Plant", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.reference3", "title": "Season Description", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.model", "title": "Model", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.design", "title": "Design", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.size", "title": "Size", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.color", "title": "Color", "type": CONTENT_TYPES["text"], "default": "" },
        { "index": "productDetails.season", "title": "Season", "type": CONTENT_TYPES["text"], "default": "" }
    ]

    const tableSortColumns = [];
    const tableFilterColumns = [];

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

    return (
        <div>
            <div className="p-2 bg-gray-100 flex-1">
                <h1 className="bg-white rounded p-4 text-2xl font-bold mb-4 pl-5">SKU</h1>
            </div>
            <div className="bg-white">
                <div className="p-2 bg-gray-100 flex-1 sku-items-master">
                    <div className="bg-white p-4 rounded-lg shadow-md mx-5 flex items-center space-x-4">
                        <CreateSKU />
                        <SKUSearchBar />
                    </div>

                </div>
                <div className="p-2 bg-gray-100 flex-1 sku-items-master  sku-view" style={{ 'overflow': 'hidden' }}>
                    {
                        processedSKUsItems.results.length > 0 && skuItemMasterStatus != 'loading' &&
                        <DataTable
                            noDataMessage=""
                            columns={tableColumns}
                            sorts={tableSortColumns}
                            filters={tableFilterColumns}
                            getRecord={viewHandle}
                            pagination={pagination}
                            selectPageNo={handleSearch}
                            sortCall={handleSort}
                            sortedData={sortData}
                            data={sKUsItems?.results}
                            icons={false}
                        />
                    }
                     {skuItemMasterStatus == 'loading' && <LoadingCircle />}
                </div >
            </div>
        </div>
    );
}
