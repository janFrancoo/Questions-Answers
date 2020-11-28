import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import * as questionActions from "../../redux/actions/questionActions"
import alertifyjs from "alertifyjs"
import { ListGroup, ListGroupItem } from 'reactstrap'
import { Link } from "react-router-dom"

class QuestionList extends Component {
    componentDidMount() {
        this.props.actions.getQuestions()
    }

    componentDidUpdate() {
        if (!this.props.questionsResponse.success)
            alertifyjs.error(this.props.questionsResponse)
    }

    render() {
        return (
            <div>
                <ListGroup>
                    {
                        this.props.questionsResponse.data.map(question => (
                            <ListGroupItem key={question.id} >
                                <Link to={"/question/" + question.id}>{question.title}</Link>
                            </ListGroupItem>
                        ))
                    }
                </ListGroup>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
      questionsResponse: state.questionsReducer
    }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            getQuestions: bindActionCreators(questionActions.getQuestions, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(QuestionList)
