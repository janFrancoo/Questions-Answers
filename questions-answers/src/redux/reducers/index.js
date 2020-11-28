import authReducer from "./authReducer"
import userReducer from "./userReducer"
import questionsReducer from "./questionsReducer"
import questionReducer from "./questionReducer"
import { combineReducers } from "redux"

const rootReducer = combineReducers({
    authReducer,
    userReducer,
    questionsReducer,
    questionReducer
})

export default rootReducer
