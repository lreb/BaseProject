import React, { Component, PropTypes } from "react";
import { BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect } from 'react-router-dom'
import Dashboard from "../dashboard";
import Home from "../home";
import Login from "../login";

class HomeSection extends Component {
	constructor(props) {
		super(props);
		this.state = {
			dataState: this.props.dataState
		 }
	}

	handleLoginCallback = (childData) =>{
		console.log(childData);
		this.setState({dataState: childData})
	}

	render() {

		const dataUser = this.state.dataState;
		console.log(dataUser);

		let contentBody;

		if (dataUser.isUserAuthenticated) {
			contentBody = <Dashboard />
		} else {
			contentBody =
			<>
				<Router>
					<ul>
						<li>
							<Link to="/home">Home</Link>
						</li>
						<li>
							<Link to="/login">Login</Link>
						</li>
					</ul>
					<Switch>
						<Route exact path="/home" component={Home} />
						<Route exact path="/login">
							<Login loginCallback={this.handleLoginCallback} />
						</Route>
					</Switch>
				</Router>
			</>
		}

		return ( <div>
			<label >HomeSection</label>  {contentBody}
		</div>);
	}
}

export default HomeSection;