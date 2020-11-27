import React, { Component } from 'react';
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import * as userActions from "../../redux/actions/userActions"
import { Form, FormGroup, Button, Label, Input } from "reactstrap"
import Cookies from "universal-cookie"

class Avatar extends Component {
    constructor(props) {
        super(props)

        this.state = {
            imageFile: null
        }
    }

    handleImageChange(event) {
        this.setState({
            imageFile: event.target.files[0]
        })
    }

    handleSubmit(event) {
        event.preventDefault()
     
        let formData = new FormData()
        formData.append('avatar', this.state.imageFile)

        const cookies = new Cookies()
        const token = cookies.get("access_token")

        this.props.actions.uploadAvatar(formData, token)
    }

    render() {
        return (
            <div>
                <Form onSubmit={(event) => this.handleSubmit(event)}>
                    <FormGroup>
                        <Label for="file">Change Avatar</Label>
                        <Input type="file" name="file" id="file" onChange={(event) => this.handleImageChange(event)} />
                    </FormGroup>
                    <Button>Submit</Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return { }
}
  
function mapDispatchToProps(dispatch) {
    return {
        actions: {
            uploadAvatar: bindActionCreators(userActions.uploadAvatar, dispatch)
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Avatar)
