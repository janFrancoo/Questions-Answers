import * as actionTypes from "./actionTypes"

export const getUser = (id, token) => (function(dispatch) {        
    let statusCode
    let url = "https://localhost:44309/api/users/get-user-by-id"
    if (id)
        url += "?id=" + id

    return fetch(url, {
        method: "get",
        headers: {
            "Accept": "application/json, text/plain, */*",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        }})
        .then(res => {
            statusCode = res.status
            if (res.status === 401)
                return { }
            return res.json()
        })
        .then(res => {
            if (res.success === true)
                return dispatch(getUserSuccess(res))
            else {
                res.status = statusCode
                return dispatch(getUserFail(res))
            }
        })
})

export const getUserSuccess = (userResponse) => ({
    type: actionTypes.GET_USER_SUCCESS,
    payload: userResponse
})

export const getUserFail = (userResponse) => ({
    type: actionTypes.GET_USER_FAIL,
    payload: userResponse
})
