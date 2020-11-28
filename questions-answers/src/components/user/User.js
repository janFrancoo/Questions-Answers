import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import * as userActions from "../../redux/actions/userActions"
import * as questionActions from "../../redux/actions/questionActions"
import alertifyjs from "alertifyjs"
import Cookies from "universal-cookie"
import { Row, Col, ListGroup, ListGroupItem } from "reactstrap"
import Avatar from './Avatar'
import { Link } from "react-router-dom"

class User extends Component {
    componentDidMount() {
        let userId = this.props.match.params.id

        const cookies = new Cookies()
        const token = cookies.get("access_token")

        this.props.actions.getUser(userId, token)
        this.props.actions.getQuestions(userId, token)
    }

    componentDidUpdate() {
        if (!this.props.userResponse.success)
            alertifyjs.error(this.props.userResponse.message)

        if (!this.props.questionsResponse.success)
            alertifyjs.error(this.props.questionsResponse.message)

        console.log(this.props.questionsResponse)
    }

    render() {
        return (
            <div>
                <Row>
                    <Col xs="4">
                        { this.props.userResponse.success && 
                            <img src={ this.props.userResponse.data.avatar ? 
                                "https://localhost:44309/Media/Avatars/" + this.props.userResponse.data.avatar
                            :   "http://localhost:3000/images/default_avatar.png" } 
                                width="300" height="300" alt="avatar" />
                        }
                    </Col>
                    <Col xs="8">
                        <h1>{ this.props.userResponse.data.username }</h1>
                        {
                            !this.props.match.params.id && this.props.userResponse.success && <Avatar />
                        }
                    </Col>
                </Row>
                <hr />
                <h3>Questions</h3>
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
        userResponse: state.userReducer,
        questionsResponse: state.questionsReducer
    }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            getUser: bindActionCreators(userActions.getUser, dispatch),
            getQuestions: bindActionCreators(questionActions.getQuestionsByUser, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(User)
