import { createSlice } from '@reduxjs/toolkit'
import axios from 'axios'

const initialState = {
    user: null
};

const authSlice = createSlice({
    name: 'auth',
    initialState: initialState,
    reducers: {
        storeUser(state, action) {
            state.user = action.payload;
        },
        storeUserError(state) {
            console.log("error")
            state.user = null;
        },
        userExpired(state) {
            state.user = null;
        },
        userSignedOut(state) {
            state.user = null;
        }
    }
})

export function setAuthHeader(token) {
    axios.defaults.headers.common['Authorization'] = token ? 'Bearer ' + token : ''
}

export const { storeUser, storeUserError, userExpired, userSignedOut } = authSlice.actions

export default authSlice.reducer