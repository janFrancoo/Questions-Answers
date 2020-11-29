import authReducer from "./authReducer"
import userReducer from "./userReducer"
import questionsReducer from "./questionsReducer"
import questionReducer from "./questionReducer"
import answersReducer from "./answersReducer"
import answerReducer from "./answerReducer"
import { combineReducers } from "redux"

const rootReducer = combineReducers({
    authReducer,
    userReducer,
    questionsReducer,
    questionReducer,
    answersReducer,
    answerReducer
})

export default rootReducer
