import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { fetchInbounds } from './dashboardAPI';

const initialState = {
    value: 0,
    status: 'idle',
    dashbordItems: {},
    filterFormData: {
        documentType: "",
        documentId: "",
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

// Async thunk to fetch chart data
export const fetchInboundTransaction = createAsyncThunk(
    'dashboard/fetchInboundTransaction',
    async (filters = {}) => {
        const response = await fetchInbounds(filters);
        return response;
    }
);


export const dashboardSlice = createSlice({
    name: 'dashboard',
    initialState,
    reducers: {
        increment: (state) => {
            state.value += 1;
        },
        updateFilterFormData: (state, action) => {
            state.filterFormData = action.payload;
        },
        resetFilterFormData: (state, action) => {
            state.filterFormData = {
                documentType: "",
                documentId: "",
                status: "",
                take: 15,
                skip: 0,
                page: 1,
            };
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchInboundTransaction.pending, (state) => {
                state.status = 'loading';
            })
            .addCase(fetchInboundTransaction.fulfilled, (state, action) => {
                state.status = 'idle';
                state.inboundDashbordItems = action.payload;
                state.filterFormData.take = action.payload.take;
                state.filterFormData.skip = action.payload.skip;
                state.pagination = {
                    pageNo: (action.payload.skip / action.payload.take),
                    takeCount: action.payload.take,
                    totalItems: action.payload.total
                };
            })
            .addCase(fetchInboundTransaction.rejected, (state, action) => {
                state.status = 'failed';
                console.error('Error fetching statistics:', action.error);
            })
    }
});

export const { increment, updateFilterFormData, resetFilterFormData } = dashboardSlice.actions;

// Selectors
export const selectInboundDashbordItems = (state) => state.dashboard.inboundDashbordItems;
export const selectDashbordStatus = (state) => state.dashboard.status;
export const selectFilterFormData = (state) => state.dashboard.filterFormData;
export const selectPagination = (state) => state.dashboard.pagination;

export default dashboardSlice.reducer;
