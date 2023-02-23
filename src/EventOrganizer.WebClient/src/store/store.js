import { configureStore } from '@reduxjs/toolkit'
import generalReducer from './generalSlice'
import authReducer from './authSlice'

export default configureStore({
    reducer: {
        general: generalReducer,
        auth: authReducer
    }
})
