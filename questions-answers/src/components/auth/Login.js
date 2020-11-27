import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import { Button, Form, FormGroup, Label, Input } from 'reactstrap'
import * as authActions from "../../redux/actions/authActions"
import alertifyjs from "alertifyjs"
import Cookies from "universal-cookie"
import { Redirect } from "react-router-dom"

class Login extends Component {
    constructor(props) {
        super(props)

        this.state = {
            redirect: false
        }
      }

    handleSubmit(event) {
        event.preventDefault()

        const data = new FormData(event.target)
        const email = data.get('email')
        const pwd = data.get('password')

        if (!(email && pwd))
            alertifyjs.error("Missing fields");
        else
            this.props.actions.login(email, pwd);
    }

    componentDidUpdate() {
        if (!this.props.loginResponse.success)
            alertifyjs.error(this.props.loginResponse)
        else {
            const loginData = this.props.loginResponse.data

            const cookies = new Cookies()
            cookies.set("access_token", loginData.token, { path: "/", expires: new Date(loginData.expiration) })

            alertifyjs.success("Welcome!")
            this.setState({ redirect: true })
        }
    }

    render() {
        return (
            <Form onSubmit={(event) => this.handleSubmit(event)}>
                { this.state.redirect && <Redirect to="/" /> }
                <FormGroup>
                    <Label for="exampleEmail">E-mail</Label>
                    <Input type="email" name="email" id="email" placeholder="Your e-mail" />
                </FormGroup>
                <FormGroup>
                    <Label for="examplePassword">Password</Label>
                    <Input type="password" name="password" id="password" placeholder="Your password" />
                </FormGroup>
                <Button color="primary">Login</Button>
            </Form>
        );
    }
}

function mapStateToProps(state) {
    return {
      loginResponse: state.authReducer
    }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            login: bindActionCreators(authActions.login, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Login)
