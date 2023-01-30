import { createSlice } from '@reduxjs/toolkit'

const initialState = {
    isUserSignedIn: false
}

const authSlice = createSlice({
    name: 'auth',
    initialState: initialState,
    reducers: {
        signIn(state) {
            state.isUserSignedIn = true
        },
        signOut(state) {
            state.isUserSignedIn = false
        }
    }
})

export const { signIn, signOut } = authSlice.actions
export default authSlice.reducer