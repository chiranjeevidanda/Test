export const URL_BASE = "/page";
// export const API_URL = process.env.NEXT_PUBLIC_API_URI;
export const NEXT_PUBLIC_CAPBUILDER_URI =
    process.env.NEXT_PUBLIC_CAPBUILDER_URI;
export const MAX_RESULTS = 10;

export const SORT_DIRECTION_ASC = "ASC";
export const SORT_DIRECTION_DESC = "DESC";
export const SORT_DIRECTION_RESET = "RESET";

export const STATUS_COLUMN_INDEX = "active";

export const PAGINATION_PAGE = 0;
export const PAGINATION_TOTAL = 0;
export const PAGINATION_SKIP = 0;
export const PAGINATION_TAKE = 5; //PER PAGE RECORDS

export const API_HEADER_MULTIPART_FORMDATA = {
    "Content-Type": "multipart/form-data",
};
export const API_HEADER_APPLICATION_JSON = {
    "Content-Type": "application/json",
};

export const DEFAULT_PRODUCT_OPTION_ID = 404;
export const DEFAULT_TEXTURE_ID = 55;
export const DEFAULT_ROALTYPE_ID = 1;

export const CONTENT_TYPES = {
    text: "TEXT",
    file: "FILE",
    image: "IMAGE",
    color: "COLOR",
    bool: "BOOLEAN",
    scrollableTextCell: "SCROLLABLETEXTCELL"
};

export const FORM_METHODS = {
    post: "POST",
    put: "PUT",
    get: "GET",
    delete: "DELETE",
};

export const ACTIONS_AGAINS_FORM_METHODS = {
    POST: "Created",
    PUT: "Updated",
    GET: "Got",
    DELETE: "Deleted",
};


export const DOCUMENT_TYPES = {
    expense: { id: "1", key: 'Expense', title: 'Expense' },
    invoice: { id: "2", key: 'Invoice', title: 'Invoice' },
    Employee: { id: "3", key: 'Employee', title: 'Employee' },
    Vendor: { id: "4", key: 'Vendor', title: ' Vendor' },
    CostCenter: {
        id: "5", key: 'CostCenter', title: ' Cost Center'
    },
    InternalOrder: { id: "6", key: 'InternalOrder', title: ' Internal Order' },

};

export const FINANCIAL_DOCUMENT_TYPES = {
    expense: { id: "1", key: 'Expense', title: 'Expense' },
    invoice: { id: "2", key: 'Invoice', title: 'Invoice' }
};

export const DOCUMENT_STATUS = {
    completed: { id: "1", key: 'Completed', title: 'Completed' },
    failed: { id: "2", key: 'Failed', title: 'Failed' },
    pending: { id: "3", key: 'Pending', title: ' Pending' },
};

export const RETRIGGER_DOCUMENT_STATUS = {
    failed: { id: "2", key: 'Failed', title: 'Failed' },
    pending: { id: "3", key: 'Pending', title: ' Pending' },
};

export const INOUND_TRANSACTION_ORDER_TYPE = {
    GoodsReceived: { id: "1", key: 'GoodsReceived', title: 'PO Receipt' },
    GoodsIssued: { id: "2", key: 'GoodsIssued', title: 'Shipment' },
    ReturnReceived: { id: "3", key: 'ReturnReceived', title: 'Return Receipt' },
    Inventory: { id: "4", key: 'Inventory', title: 'Inventory' },
};

export const SKU_CUSTOMER = {
    NEWERAB2B: { id: "1", key: 'NEWERAB2B', title: 'NEWERAB2B' },
    NEWERAB2C: { id: "2", key: 'NEWERAB2C', title: 'NEWERAB2C' },
};
export const OUTBOUND_TRANSACTION_ORDER_TYPE = {
    POCreate: { id: "1", key: 'PurchaseOrder', title: 'PO Create' },
    OrderCreate: { id: "2", key: 'CreateOrder', title: 'Order Create' },
    ReturnOrder: { id: "3", key: 'ReturnOrder', title: 'Return Order' },
    SKUCreate: { id: "4", key: 'ProductMaster', title: 'SKU Create' },
};


export const RETRIGGER_TRANSACTION_ORDER_TYPE = {
    SKUCreate: { id: "1", key: 'ProductMaster', title: 'SKU Create to KTN' },
    GoodsReceived: { id: "2", key: 'GoodsReceived', title: 'PO Receipt' },
    GoodsIssued: { id: "3", key: 'GoodsIssued', title: 'Shipment' },
    ReturnReceived: { id: "4", key: 'ReturnReceived', title: 'Return Receipt' },
    Inventory: { id: "5", key: 'Inventory', title: 'Inventory' },
    POCreate: { id: "6", key: 'PurchaseOrder', title: 'PO Create' },
    OrderCreate: { id: "7", key: 'CreateOrder', title: 'Order Create' },
    ReturnOrder: { id: "8", key: 'ReturnOrder', title: 'Return Order' },
};

export const FORM_PURPOSE_VIEW = "view";
export const FORM_PURPOSE_EDIT = "edit";

export const ALERT_SUCCESS_ACTIVATE = "Done!, Successfully Activated.";
export const ALERT_SUCCESS_DEACTIVATE = "Done!, Successfully Deactivated.";
export const ALERT_TRY_AGAIN = "Sorry!, Try Againg Later.";
export const ALERT_ERROR_FORM_VALIDATION = "Form validation errors.";

export const DEFAULT_DELETE_CONFIRM_MESSAGE =
    "Are you sure you want to continue?";

export const ACTIVATE = "activate";

export const API_URL = process.env.REACT || 'api';