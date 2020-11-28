import * as actionTypes from "../actions/actionTypes"
import initialState from "./initialState"

export default function questionReducer(state=initialState.question, action) {    
    switch (action.type) {
        case actionTypes.GET_QUESTION_SUCCESS:
            return action.payload
        case actionTypes.GET_QUESTION_FAIL:
            return {
                ...action.payload,
                ...state
            }
        case actionTypes.ADD_QUESTION_SUCCESS:
            return action.payload
        case actionTypes.ADD_QUESTION_FAIL:
            return action.payload
        default: 
            return state
    }
}