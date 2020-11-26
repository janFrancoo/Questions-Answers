import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import { Button, Form, FormGroup, Label, Input } from 'reactstrap'
import * as authActions from "../../redux/actions/authActions"
import alertifyjs from "alertifyjs"
import Cookies from "universal-cookie"
import { Redirect } from "react-router-dom"

class Register extends Component {
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
        const username = data.get('username')
        const pwd = data.get('password')

        if (!(email && pwd && username))
            alertifyjs.error("Missing fields");
        else
            this.props.actions.register(email, username, pwd);
    }

    componentDidUpdate() {
        if (!this.props.registerResponse.success)
            alertifyjs.error(this.props.registerResponse)
        else {
            const loginData = this.props.registerResponse.data

            const cookies = new Cookies()
            cookies.set("access_token", loginData.token, { path: "/", expires: new Date(loginData.expiration) })

            alertifyjs.success("Welcome!")
            this.setState({ redirect: true })
        }
    }

    render() {
        return (
            <div>
                <Form onSubmit={(event) => this.handleSubmit(event)}>
                    { this.state.redirect && <Redirect to="/" /> }
                    <FormGroup>
                        <Label for="email">E-mail</Label>
                        <Input type="email" name="email" id="email" placeholder="Your e-mail" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="username">Username</Label>
                        <Input type="text" name="username" id="username" placeholder="Your username" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="password">Password</Label>
                        <Input type="password" name="password" id="password" placeholder="Your password" />
                    </FormGroup>
                    <Button color="primary">Register</Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
      registerResponse: state.authReducer
    }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            register: bindActionCreators(authActions.register, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Register)
