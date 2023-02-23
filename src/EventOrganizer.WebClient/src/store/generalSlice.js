import { createSlice } from '@reduxjs/toolkit'

const initialState = {
    loading: false,
    error: null
}

const generalSlice = createSlice({
    name: 'general',
    initialState: initialState,
    reducers: {
        startLoading(state) {
            state.loading = true
        },
        finishLoading(state) {
            state.loading = false
        },
        setError(state, action) {
            state.error = action.payload;
        },
        removeError(state) {
            state.error = null;
        }
    }
})

export const { startLoading, finishLoading, setError, removeError } = generalSlice.actions
export default generalSlice.reducer