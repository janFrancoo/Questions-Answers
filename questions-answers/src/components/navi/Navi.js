import React, { Component } from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavLink,
  Button
} from 'reactstrap';
import Cookies from "universal-cookie"
import { connect } from "react-redux"
import { Link } from "react-router-dom"

class Navi extends Component {
  constructor(props) {
    super(props)

    this.state = {
        isOpen: false,
        isLoggedIn: false
    }
  }

  toggle() { 
    this.setState({ isOpen: !this.state.isOpen })
  }

  logout() {
    const cookies = new Cookies()
    cookies.remove("access_token", { path: '/' })
    this.setState({ isLoggedIn: false })
  }

  componentDidMount() {
    // check token exists
    const cookies = new Cookies()
    const token = cookies.get("access_token")

    if (token) {
      this.setState({ isLoggedIn: true })
    }
  }

  componentDidUpdate(previousProps, previousState) {
    // check if user logged in
    if (this.props.loginResponse.success && previousState.isLoggedIn !== true) {
      this.setState({ isLoggedIn: true })
    }
  }

  render() {
    return (
      <div>
        <Navbar color="light" light expand="md">
          <NavbarBrand href="/">Questions & Answers</NavbarBrand>
          <NavbarToggler onClick={() => this.toggle()} />
          <Collapse isOpen={this.state.isOpen} navbar>
            <Nav className="mr-auto" navbar></Nav>
            { !this.state.isLoggedIn && <NavLink href="/auth/login">Login</NavLink> }
            { !this.state.isLoggedIn && <NavLink href="/auth/register">Register</NavLink> }
            { this.state.isLoggedIn && <Link to="/question/add" className="mr-5">Add Question</Link> }
            { this.state.isLoggedIn && <Link to="/user" className="mr-5">Profile</Link> }
            { this.state.isLoggedIn && <Button color="primary" onClick={() => this.logout()}>Logout</Button> }
          </Collapse>
        </Navbar>
      </div>
      )
  };
}

function mapStateToProps(state) {
  return {
    loginResponse: state.authReducer
  }
}

export default connect(mapStateToProps)(Navi)
