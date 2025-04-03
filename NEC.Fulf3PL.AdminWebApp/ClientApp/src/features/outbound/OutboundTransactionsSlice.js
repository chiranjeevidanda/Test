import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { fetchOutboundTransactions, fetchOutboundTransactionsExport } from './OutboundTransactionsApi';

const initialState = {
    status: 'idle',
    outboundTransactions: {},
    filterFormData: {
        documentType: "",
        dateFrom: new Date(Date.now() - 7 * 24 * 60 * 60 * 1000).toJSON().slice(0, 10),
        dateTo: new Date().toJSON().slice(0, 10),
        orderNumber: "",
        status: "",
        take: 15,
        skip: 0,
        page: 1,
    },
    pagination: {
        pageNo: 0,
        takeCount: 0,
        totalItems: 0
    },

};

// Async thunk to fetch outbound transaction details
export const fetchItems = createAsyncThunk(
    'outbound/fetchItems',
    async (filters = {}) => {
        const response = await fetchOutboundTransactions(filters);
        return response;
    }
);

export const fetchExports = createAsyncThunk(
    'outbound/fetchExports',
    async (filters = {}) => {
        const response = await fetchOutboundTransactionsExport(filters);
        return response;
    }
);


export const outboundTransactionsSlice = createSlice({
    name: 'outboundTransactions',
    initialState,
    reducers: {
        increment: (state) => {
            state.value += 1;
        },
        decrement: (state) => {
            state.value -= 1;
        },
        updateFilterFormData: (state, action) => {
            state.filterFormData = action.payload;
        },
        resetFilterFormData: (state, action) => {
            state.filterFormData = {
                documentType: "",
                dateFrom: new Date(Date.now() - 7 * 24 * 60 * 60 * 1000).toJSON().slice(0, 10),
                dateTo: new Date().toJSON().slice(0, 10),
                orderNumber: "",
                status: "",
                take: 15,
                skip: 0,
                page: 1,
            };
        },

    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchItems.pending, (state) => {
                state.status = 'loading';
                state.filterFormData.skip = state.pagination?.pageNo * state.filterFormData.take;
            })
            .addCase(fetchItems.fulfilled, (state, action) => {
                state.status = 'idle';
                state.outboundTransactions = action.payload;
                state.filterFormData.take = action.payload.take;
                state.filterFormData.skip = action.payload.skip;
                state.pagination = {
                    pageNo: (action.payload.skip / action.payload.take),
                    takeCount: action.payload.take,
                    totalItems: action.payload.total
                };
            })
            .addCase(fetchItems.rejected, (state, action) => {
                state.status = 'failed';
                console.error('Error fetching statistics:', action.error);
            }).addCase(fetchExports.pending, (state) => {
                state.exportStatus = 'loading';
            })
            .addCase(fetchExports.fulfilled, (state, action) => {
                state.exportStatus = 'idle';
                state.exportData = action.payload;
            })
            .addCase(fetchExports.rejected, (state, action) => {
                state.exportStatus = 'failed';
            });
    },
});

export const { increment, decrement, updateFilterFormData, resetFilterFormData } = outboundTransactionsSlice.actions;

// Selectors
export const selectoutboundTransactions = (state) => state.outboundTransaction.outboundTransactions;
export const selectoutboundTransactionsStatus = (state) => state.outboundTransaction.status;
export const selectFilterFormData = (state) => state.outboundTransaction.filterFormData;
export const selectPagination = (state) => state.outboundTransaction.pagination;


export default outboundTransactionsSlice.reducer;
