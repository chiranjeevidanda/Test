import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { fetchSKUs, fetchSkuExport } from './SKUItemMasterApi';

const initialState = {
    status: 'idle',
    skuItems: {},
    filterFormData: {
        documentId: "",
        createDocumentId: "",
        customer: "",
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
export const fetchExports = createAsyncThunk(
    'skuItemMaster/fetchExports',
    async (filters = {}) => {
        const response = await fetchSkuExport(filters);
        return response;
    }
);
export const fetchItems = createAsyncThunk(
    'skuItemMaster/fetchItems',
    async (filters = {}) => {
        const response = await fetchSKUs(filters);
        return response;
    }
);

export const skuItemMasterSlice = createSlice({
    name: 'skuItemMaster',
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
                orderNumber: "",
                createOrderNumber: "",
                customer: "",
                take: 15,
                skip: 0,
                page: 1,
            };
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchItems.pending, (state) => {
                state.status = 'loading';
                state.filterFormData.skip = state.pagination?.pageNo * state.filterFormData.take;
            })
            .addCase(fetchItems.fulfilled, (state, action) => {
                state.status = 'idle';
                state.skuItems = action.payload;
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

export const { increment, decrement, updateFilterFormData, resetFilterFormData } = skuItemMasterSlice.actions;

export const selectskuItemMaster = (state) => state.skuItemMaster.skuItems;
export const selectskuItemMasterStatus = (state) => state.skuItemMaster.status;
export const selectFilterFormData = (state) => state.skuItemMaster.filterFormData;
export const selectPagination = (state) => state.skuItemMaster.pagination;

export default skuItemMasterSlice.reducer;
