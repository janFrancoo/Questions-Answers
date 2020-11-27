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

export const uploadAvatar = (formData, token) => (function(dispatch) {        
    return fetch("https://localhost:44309/api/users/update-avatar", {
        method: "post",
        headers: {
            "Accept": "application/json, text/plain, */*",
            "Authorization": "Bearer " + token
        },
        body: formData })
        .then(res => {
            return res.json()
        })
        .then(res => {
            if (res.success === true)
                return dispatch(uploadAvatarSuccess(res))
            else {
                return dispatch(uploadAvatarFail(res))
            }
        })
})

export const uploadAvatarSuccess = (res) => ({
    type: actionTypes.UPLOAD_AVATAR_SUCCESS,
    payload: res
})

export const uploadAvatarFail = (res) => ({
    type: actionTypes.UPLOAD_AVATAR_FAIL,
    payload: res
})
