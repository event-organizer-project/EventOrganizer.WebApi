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
            setAuthHeader(action.payload.access_token);
            state.user = action.payload;
        },
        storeUserError(state) {
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

export function getUserSlice(payload) {
    return {
        name: payload.profile.name,
        scopes: payload.scope.split(' '),
        access_token: payload.access_token
    }
}

export const { storeUser, storeUserError, userExpired, userSignedOut } = authSlice.actions

export default authSlice.reducer