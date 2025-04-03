import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { loginUser, registerUser } from "./userAPI";

const initialState = {
  isLoggedIn: false,
  activeAuthScreen: "register",
  isBusy: false,
  profile: {},
};

const SLICE_NAME = "user";

export const loginUserAsync = createAsyncThunk(
  `${SLICE_NAME}/loginUser`,
  async (amount) => {
    const response = await loginUser(amount);
    // The value we return becomes the `fulfilled` action payload
    return response.data;
  }
);

export const registerUserAsync = createAsyncThunk(
  `${SLICE_NAME}/registerUser`,
  async (amount) => {
    const response = await registerUser(amount);
    // The value we return becomes the `fulfilled` action payload
    return response.data;
  }
);

export const userSlice = createSlice({
  name: SLICE_NAME,
  initialState,
  reducers: {
    changeIsLoogedIn: (state, action) => {
      state.isLoggedIn = action.payload;
    },
    changeActiveAuthScreen: (state, action) => {
      state.activeAuthScreen = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loginUserAsync.pending, (state) => {
        state.isBusy = true;
      })
      .addCase(loginUserAsync.fulfilled, (state, action) => {
        state.isBusy = false;
        state.profile = action.payload;
      })
      .addCase(registerUserAsync.pending, (state) => {
        state.isBusy = true;
      })
      .addCase(registerUserAsync.fulfilled, (state, action) => {
        state.isBusy = false;
        state.profile = action.payload;
      });
  },
});

export const { changeIsLoogedIn, changeActiveAuthScreen } = userSlice.actions;

export const selectIsLoogedIn = (state) => state[SLICE_NAME].isLoggedIn;

export const selectProfile = (state) => state[SLICE_NAME].profile;

export const getIsBusy = (state) => state[SLICE_NAME].isBusy;

export const getActiveAuthScreen = (state) =>
  state[SLICE_NAME].activeAuthScreen;

export default userSlice.reducer;
