import * as actionTypes from "../actions/actionTypes"
import initialState from "./initialState"

export default function questionsReducer(state=initialState.questions, action) {    
    switch (action.type) {
        case actionTypes.GET_QUESTIONS_SUCCESS:
            return action.payload
        case actionTypes.GET_QUESTIONS_FAIL:
            return {
                ...action.payload,
                ...state
            }
        default: 
            return state
    }
}