import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import * as userActions from "../../redux/actions/userActions"
import alertifyjs from "alertifyjs"
import Cookies from "universal-cookie"
import { Row, Col } from "reactstrap"
import { Link } from 'react-router-dom';

class User extends Component {
    componentDidMount() {
        let userId = this.props.match.params.id

        const cookies = new Cookies()
        const token = cookies.get("access_token")

        this.props.actions.getUser(userId, token)
    }

    componentDidUpdate() {
        if (!this.props.userResponse.success)
            alertifyjs.error(this.props.userResponse)
    }

    render() {
        return (
            <div>
                <Row>
                    <Col xs="4">
                        <img src="https://localhost:44309/Media/Avatars/1005.jpeg" width="300" height="300" alt="avatar" />
                    </Col>
                    <Col xs="8">
                        <h1>{ this.props.userResponse.data.username }</h1>
                        {
                            !this.props.match.params.id && <Link to="/user/settings">Settings</Link>
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
