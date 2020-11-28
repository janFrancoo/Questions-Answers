import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import * as questionActions from "../../redux/actions/questionActions"
import alertifyjs from "alertifyjs"

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
                { this.props.questionResponse.data.title }
                <hr />
                <p>{ this.props.questionResponse.data.questionText }</p>
                <p>{ this.props.questionResponse.data.date }</p>
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
