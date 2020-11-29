import * as actionTypes from "../actions/actionTypes"
import initialState from "./initialState"

export default function answersReducer(state=initialState.answers, action) {
    switch (action.type) {
        case actionTypes.GET_ANSWERS_SUCCESS:
            return action.payload
        case actionTypes.GET_ANSWERS_FAIL:
            return {
                ...action.payload,
                ...state
            }
        default: 
            return state
    }
}