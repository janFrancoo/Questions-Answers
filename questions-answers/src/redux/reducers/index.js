import authReducer from "./authReducer"
import userReducer from "./userReducer"
import { combineReducers } from "redux"

const rootReducer = combineReducers({
    authReducer,
    userReducer
})

export default rootReducer
