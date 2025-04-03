import { configureStore } from "@reduxjs/toolkit";
import userSliceReducer from "../features/user/userSlice";
import dashboardSliceReducer from "../features/dashboard/dashboardSlice";
import outboundTransactionsReducer from "../features/outbound/OutboundTransactionsSlice";
import inboundTransactionsReducer from "../features/inbound/InboundTransactionsSlice";
import sKUsReducer from "../features/skuItemMaster/SKUItemMasterSlice";

export const store = configureStore({
  reducer: {
    user: userSliceReducer,
    dashboard: dashboardSliceReducer,
    outboundTransaction: outboundTransactionsReducer,
    inboundTransaction: inboundTransactionsReducer,
    skuItemMaster: sKUsReducer
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false,
    }),
});

store.subscribe(() => console.log(store.getState()));
