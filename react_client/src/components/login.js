import React from 'react';

class Login extends React.Component {

	onClickLoginButton = (event) => {
			this.props.loginCallback(true)
			console.log(event)
			event.preventDefault();
	}

	render() {
		return ( <>
			<label>Login</label> <button onClick={this.onClickLoginButton}>login</button>
			</>);
	}
}

export default Login;