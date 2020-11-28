import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import { Button, Form, FormGroup, Label, Input } from 'reactstrap'
import * as questionActions from "../../redux/actions/questionActions"
import Cookies from "universal-cookie"
import { Redirect } from "react-router-dom"
import alertifyjs from "alertifyjs"

class AddQuestion extends Component {
    constructor(props) {
        super(props)

        this.state = {
            redirect: false
        }
    }

    handleSubmit(event) {
        event.preventDefault()

        const data = new FormData(event.target)
        const title = data.get("title")
        const questionText = data.get("questionText")
        const body = {
            Title: title,
            QuestionText: questionText
        }

        const cookies = new Cookies()
        const token = cookies.get("access_token")

        this.props.actions.addQuestion(body, token)
    }

    componentDidUpdate() {
        if (this.props.postResponse.success)
            this.setState({ redirect: true })
        else
            alertifyjs.error(this.props.postResponse)
    }

    render() {
        return (
            <div>
                <h2>Add Question</h2>
                <hr />
                { this.state.redirect && <Redirect to="/" /> }
                <Form onSubmit={(event) => this.handleSubmit(event)}>
                    <FormGroup>
                        <Label for="title">Title</Label>
                        <Input type="text" name="title" id="title" placeholder="Title" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="questionText">Question</Label>
                        <Input type="textarea" name="questionText" id="questionText" placeholder="Question" />
                    </FormGroup>
                    <Button>Submit</Button>
                </Form>
            </div>
        );
    }
}


function mapStateToProps(state) {
    return {
        postResponse: state.questionReducer
    }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            addQuestion: bindActionCreators(questionActions.addQuestion, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(AddQuestion)
