import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import { Button, Form, FormGroup, Label, Input } from 'reactstrap'
import * as answerActions from "../../redux/actions/answerActions"
import Cookies from "universal-cookie"
import { Redirect } from "react-router-dom"
import alertifyjs from "alertifyjs"

class AddAnswer extends Component {
    constructor(props) {
        super(props)

        this.state = {
            redirect: false
        }
    }

    handleSubmit(event) {
        event.preventDefault()

        const data = new FormData(event.target)
        const answerText = data.get("answerText")
        const body = {
            QuestionId: this.props.questionId,
            AnswerText: answerText
        }

        const cookies = new Cookies()
        const token = cookies.get("access_token")

        this.props.actions.addAnswer(body, token)
    }

    componentDidUpdate() {
        if (this.props.answerResponse.success)
            this.setState({ redirect: true })
        else
            alertifyjs.error(this.props.answerResponse)
    }

    render() {
        return (
            <div>
                <h3>Add Answer</h3>
                { this.state.redirect && <Redirect to="/" /> }
                <Form onSubmit={(event) => this.handleSubmit(event)}>
                    <FormGroup>
                        <Input type="textarea" name="answerText" id="answerText" placeholder="Answer..." />
                    </FormGroup>
                    <Button>Submit</Button>
                </Form>
            </div>
        );
    }
}


function mapStateToProps(state) {
    return {
        answerResponse: state.answerReducer
    }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            addAnswer: bindActionCreators(answerActions.addAnswer, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(AddAnswer)
