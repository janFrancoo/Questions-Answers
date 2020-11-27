import * as actionTypes from "../actions/actionTypes"
import initialState from "./initialState"

export default function userReducer(state=initialState.user, action) {
    switch (action.type) {
        case actionTypes.GET_USER_SUCCESS:
            return action.payload
        case actionTypes.GET_USER_FAIL:
            if (action.payload.status === 401) {
                return {
                    ...action.payload,
                    ...state,
                    message: "You must login to access here"
                }
            }
            return {
                ...action.payload,
                ...state
            }
        case actionTypes.UPLOAD_AVATAR_SUCCESS:
            state.data.avatar = action.payload.data.avatar
            return Object.assign({}, state)
        default: 
            return state
    }
}
