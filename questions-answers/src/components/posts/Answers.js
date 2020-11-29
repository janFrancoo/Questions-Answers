import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import { ListGroup, ListGroupItem } from 'reactstrap'
import * as answerActions from "../../redux/actions/answerActions"
import alertifyjs from "alertifyjs"
import Cookies from "universal-cookie"

class Answers extends Component {
    componentDidMount() {
        if (this.props.getData.type === "question")
            this.props.actions.getAnswersByQuestion(this.props.getData.id)
        else if (this.props.getData.type === "user") {
            const cookies = new Cookies()
            const token = cookies.get("access_token")
            this.props.actions.getAnswersByUser(this.props.getData.id, token)
        }
    }

    componentDidUpdate() {
        if (!this.props.answersResponse.success)
            alertifyjs.error(this.props.answersResponse)

        console.log(this.props.answersResponse)
    }

    render() {
        return (
            <div>
                {
                    this.props.answersResponse.data.length === 0 ? 
                        <p>No answers for now</p> : 
                        <ListGroup>
                            {
                                this.props.answersResponse.data.map(answer => (
                                    <ListGroupItem key={answer.id} >
                                        <p>{answer.answerText}</p>
                                        <p>{answer.date}</p>
                                        <p>Likes: {answer.likeCount}</p>
                                    </ListGroupItem>
                                ))
                            }
                        </ListGroup>
                }
            </div>
        );
    }
}


function mapStateToProps(state) {
    return {
        answersResponse: state.answersReducer
    }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            getAnswersByQuestion: bindActionCreators(answerActions.getAnswersByQuestion, dispatch),
            getAnswersByUser: bindActionCreators(answerActions.getAnswersByUser, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Answers)
