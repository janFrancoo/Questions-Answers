import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import * as questionActions from "../../redux/actions/questionActions"
import alertifyjs from "alertifyjs"
import Answers from "./Answers"
import AddAnswer from "./AddAnswer"
import Cookies from "universal-cookie"

class QuestionDetail extends Component {
    componentDidMount() {
        this.props.actions.getQuestion(this.props.match.params.id)
    }

    componentDidUpdate() {
        if (!this.props.questionResponse.success)
            alertifyjs.error(this.props.questionResponse)
    }

    render() {
        return (
            <div>
                <h3>{ this.props.questionResponse.data.question.title }</h3>
                <hr />
                <p>{ this.props.questionResponse.data.question.questionText }</p>
                <p>{ this.props.questionResponse.data.question.date }</p>
                <hr />
                <p>
                    <img src={ this.props.questionResponse.data.avatar ? 
                                "https://localhost:44309/Media/Avatars/" + this.props.questionResponse.data.avatar
                            :   "http://localhost:3000/images/default_avatar.png" } 
                                width="50" height="50" alt="avatar" />
                    <a href={"/user/" + this.props.questionResponse.data.userId}>{ this.props.questionResponse.data.username }</a>
                </p>
                <h4>Answers</h4>
                <hr />
                <Answers getData={{
                    type: "question",
                    id: this.props.match.params.id
                }} />
                {
                    new Cookies().get("access_token") && 
                        <div>
                            <hr />
                            <AddAnswer questionId={this.props.match.params.id} />
                        </div>
                }
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
      questionResponse: state.questionReducer
    }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            getQuestion: bindActionCreators(questionActions.getQuestion, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(QuestionDetail)
