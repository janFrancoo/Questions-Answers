import * as actionTypes from "./actionTypes"

export const getQuestions = () => (function(dispatch) {   
    return fetch("https://localhost:44309/api/question/get-questions", {
        method: "get",
        headers: {
            "Accept": "application/json, text/plain, */*",
            "Content-Type": "application/json"
        }})
        .then(res => res.json())
        .then(res => {
            if (res.success !== false)
                return dispatch(getQuestionsSuccess(res))
            else
                return dispatch(getQuestionsFail(res))
        })
})

export const getQuestionsSuccess = (questionsResponse) => ({
    type: actionTypes.GET_QUESTIONS_SUCCESS,
    payload: questionsResponse
})

export const getQuestionsFail = (questionsResponse) => ({
    type: actionTypes.GET_QUESTIONS_FAIL,
    payload: questionsResponse.message
})

export const getQuestion = (id) => (function(dispatch) {   
    return fetch("https://localhost:44309/api/question/get-question-by-id?id=" + id, {
        method: "get",
        headers: {
            "Accept": "application/json, text/plain, */*",
            "Content-Type": "application/json"
        }})
        .then(res => res.json())
        .then(res => {
            if (res.success !== false)
                return dispatch(getQuestionSuccess(res))
            else
                return dispatch(getQuestionFail(res))
        })
})

export const getQuestionSuccess = (questionResponse) => ({
    type: actionTypes.GET_QUESTION_SUCCESS,
    payload: questionResponse
})

export const getQuestionFail = (questionResponse) => ({
    type: actionTypes.GET_QUESTION_FAIL,
    payload: questionResponse.message
})

export const getQuestionsByUser = (userId, token) => (function(dispatch) {   
    return fetch("https://localhost:44309/api/question/get-question-by-user?userId=" + (userId || ""), {
        method: "get",
        headers: {
            "Accept": "application/json, text/plain, */*",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token 
        }})
        .then(res => res.json())
        .then(res => {
            if (res.success !== false)
                return dispatch(getQuestionsSuccess(res))
            else
                return dispatch(getQuestionsFail(res))
        })
})
