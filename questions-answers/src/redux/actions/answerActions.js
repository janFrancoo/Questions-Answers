import * as actionTypes from "./actionTypes"

export const getAnswersByQuestion = (questionId) => (function(dispatch) {    
    return fetch("https://localhost:44309/api/answers/get-answers-by-question?questionId=" + questionId, {
        method: "get",
        headers: {
            "Accept": "application/json, text/plain, */*",
            "Content-Type": "application/json"
        }})
        .then(res => res.json())
        .then(res => {
            if (res.success !== false)
                return dispatch(getAnswersSuccess(res))
            else
                return dispatch(getAnswersFail(res))
        })
})

export const getAnswersSuccess = (answersResponse) => ({
    type: actionTypes.GET_ANSWERS_SUCCESS,
    payload: answersResponse
})

export const getAnswersFail = (answersResponse) => ({
    type: actionTypes.GET_ANSWERS_FAIL,
    payload: answersResponse.message
})

export const getAnswersByUser = (userId, token) => (function(dispatch) {    
    return fetch("https://localhost:44309/api/answers/get-answers-by-user?userId=" + userId, {
        method: "get",
        headers: {
            "Accept": "application/json, text/plain, */*",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        }})
        .then(res => res.json())
        .then(res => {
            if (res.success !== false)
                return dispatch(getAnswersSuccess(res))
            else
                return dispatch(getAnswersFail(res))
        })
})
