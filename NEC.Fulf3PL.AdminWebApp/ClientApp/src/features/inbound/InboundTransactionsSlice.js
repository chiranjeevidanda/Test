import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { fetchInboundTransactions, fetchInboundTransactionsExport } from './InboundTransactionsApi';

const initialState = {
    status: 'idle',
    inboundTransactions: {},
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

// Async thunk to fetch inbound transaction details
export const fetchItems = createAsyncThunk(
    'inbound/fetchItems',
    async (filters = {}) => {
        const response = await fetchInboundTransactions(filters);
        return response;
    }
);

export const clearFetchFilter = createAsyncThunk(
    'inbound/fetchItems',
    async () => {
        const response = await fetchInboundTransactions(initialState.filterFormData);
        return response;
    }
);

export const fetchExports = createAsyncThunk(
    'inbound/fetchExports',
    async (filters = {}) => {
        const response = await fetchInboundTransactionsExport(filters);
        return response;
    }
);


export const inboundTransactionsSlice = createSlice({
    name: 'inboundTransactions',
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
                state.inboundTransactions = action.payload;
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
            });
    },
});

export const { increment, decrement, updateFilterFormData, resetFilterFormData } = inboundTransactionsSlice.actions;

// Selectors
export const selectinboundTransactions = (state) => state.inboundTransaction.inboundTransactions;
export const selectinboundTransactionsStatus = (state) => state.inboundTransaction.status;
export const selectFilterFormData = (state) => state.inboundTransaction.filterFormData;
export const selectPagination = (state) => state.inboundTransaction.pagination;


export default inboundTransactionsSlice.reducer;
