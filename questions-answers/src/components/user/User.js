import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import * as userActions from "../../redux/actions/userActions"
import alertifyjs from "alertifyjs"
import Cookies from "universal-cookie"
import { Row, Col } from "reactstrap"
import Avatar from './Avatar';

class User extends Component {
    componentDidMount() {
        let userId = this.props.match.params.id

        const cookies = new Cookies()
        const token = cookies.get("access_token")

        this.props.actions.getUser(userId, token)
    }

    componentDidUpdate() {
        if (!this.props.userResponse.success)
            alertifyjs.error(this.props.userResponse.message)
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
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
      userResponse: state.userReducer
    }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            getUser: bindActionCreators(userActions.getUser, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(User)
