import * as actionTypes from "../actions/actionTypes"

export default function answerReducer(state, action) {
    switch (action.type) {
        case actionTypes.ADD_ANSWER_SUCCESS:
            return action.payload
        case actionTypes.ADD_ANSWER_FAIL:
            return action.payload
        default: 
            return { }
    }
}
