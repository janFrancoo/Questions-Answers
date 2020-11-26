import React, { Component } from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  Button
} from 'reactstrap';
import Cookies from "universal-cookie"
import { connect } from "react-redux"

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
          <NavbarBrand href="/">reactstrap</NavbarBrand>
          <NavbarToggler onClick={() => this.toggle()} />
          <Collapse isOpen={this.state.isOpen} navbar>
            <Nav className="mr-auto" navbar>
              <NavItem>
                <NavLink href="/components/">Components</NavLink>
              </NavItem>
              <NavItem>
                <NavLink href="https://github.com/reactstrap/reactstrap">GitHub</NavLink>
              </NavItem>
              <UncontrolledDropdown nav inNavbar>
                <DropdownToggle nav caret>
                  Options
                </DropdownToggle>
                <DropdownMenu right>
                  <DropdownItem>
                    Option 1
                  </DropdownItem>
                  <DropdownItem>
                    Option 2
                  </DropdownItem>
                  <DropdownItem divider />
                  <DropdownItem>
                    Reset
                  </DropdownItem>
                </DropdownMenu>
              </UncontrolledDropdown>
            </Nav>
            { !this.state.isLoggedIn && <NavLink href="/auth/login">Login</NavLink> }
            { !this.state.isLoggedIn && <NavLink href="/auth/register">Register</NavLink> }
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
