import * as actionTypes from "./actionTypes"

export const login = (email, pwd) => (function(dispatch) {    
    return fetch("https://localhost:44309/api/auth/login", {
        method: "post",
        headers: {
            "Accept": "application/json, text/plain, */*",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Email: email,
            Password: pwd
        })})
        .then(res => res.json())
        .then(res => {
            if (res.success !== false)
                return dispatch(loginSuccess(res))
            else
                return dispatch(loginFail(res))
        })
})

export const loginSuccess = (loginResponse) => ({
    type: actionTypes.LOGIN_SUCCESS,
    payload: loginResponse
})

export const loginFail = (loginResponse) => ({
    type: actionTypes.LOGIN_FAIL,
    payload: loginResponse.message
})

export const register = (email, username, pwd) => (function(dispatch) {    
    return fetch("https://localhost:44309/api/auth/register", {
        method: "post",
        headers: {
            "Accept": "application/json, text/plain, */*",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Email: email,
            Username: username,
            Password: pwd
        })})
        .then(res => res.json())
        .then(res => {
            if (res.success !== false)
                return dispatch(registerSuccess(res))
            else
                return dispatch(registerFail(res))
        })
})

export const registerSuccess = (registerResponse) => ({
    type: actionTypes.REGISTER_SUCCESS,
    payload: registerResponse
})

export const registerFail = (registerResponse) => ({
    type: actionTypes.REGISTER_FAIL,
    payload: registerResponse.message
})
