import React from 'react';

const ViewPage = ({ data }) => {

    if (!data || typeof data !== 'object') return <div>Invalid data</div>;

    const {
        modifiedDate = data.modifiedDate,
        status = data.status,
        customer = data.productDetails.customer,
        code = data.productDetails.code,
        description = data.productDetails.description,
        stockKeepingUnit = data.productDetails.stockKeepingUnit,
        itemGroup = data.productDetails.itemGroup,
        codeOfGoods = data.productDetails.codeOfGoods,
        reference1 = data.productDetails.reference1,
        reference3 = data.productDetails.reference3,
        model = data.productDetails.model,
        design = data.productDetails.design,
        size = data.productDetails.size,
        season = data.productDetails.season,
    } = data;

    return (
        <div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Timestamp :</label>
                <p className="text-gray-600 ml-2" ml-2>{modifiedDate}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">status :</label>
                <p className="text-gray-600 ml-2" ml-2>{status}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">SKU Id :</label>
                <p className="text-gray-600 ml-2" ml-2>{code}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">description :</label>
                <p className="text-gray-600 ml-2">{description}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Customer :</label>
                <p className="text-gray-600 ml-2">{customer}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Stock Keeping Unit :</label>
                <p className="text-gray-600 ml-2">{stockKeepingUnit}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Item Group :</label>
                <p className="text-gray-600 ml-2">{itemGroup}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Code Of Goods: </label>
                <p className="text-gray-600 ml-2">{codeOfGoods}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Reference 1 :</label>
                <p className="text-gray-600 ml-2">{reference1}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Reference 3 :</label>
                <p className="text-gray-600 ml-2">{reference3}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Model :</label>
                <p className="text-gray-600 ml-2">{model}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Desig :n</label>
                <p className="text-gray-600 ml-2">Code: {design}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Size :</label>
                <p className="text-gray-600 ml-2">{size}</p>
            </div>
            <div className="mb-1 flex">
                <label className="block text-gray-700 font-bold">Season :</label>
                <p className="text-gray-600 ml-2">{season}</p>
            </div>
        </div>
    );
};

export default ViewPage;
