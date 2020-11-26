import * as actionTypes from "../actions/actionTypes"

export default function authReducer(state, action) {
    switch (action.type) {
        case actionTypes.LOGIN_SUCCESS:
            return action.payload
        case actionTypes.LOGIN_FAIL:
            return action.payload
        case actionTypes.REGISTER_SUCCESS:
            return action.payload
        case actionTypes.REGISTER_FAIL:
            return action.payload
        default: 
            return { }
    }
}