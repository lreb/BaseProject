import './App.css';
import React from 'react';
import Header from './components/layout/header'
import TeslaBattery from './components/demo/TeslaBattery';
import Dashboard from './components/dashboard';
import Home from './components/home';
import { BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect } from 'react-router-dom'
import Login from './components/login';
import HomeSection from './components/home/HomeSection';

class App extends React.Component {

  constructor() {
    super();
    this.state = {
      isUserAuthenticated: false,
      showDemo: false
    };
  }

  render() {
    const counterDefaultVal = {
      speed: {
        title: "Speed",
        unit: "mph",
        step: 5,
        min: 45,
        max: 70
      },
      temperature: {
        title: "Outside Temperature",
        unit: "°",
        step: 10,
        min: -10,
        max: 40
      }
    };

    let contentBody;
    if(this.state.showDemo){
      contentBody =  <TeslaBattery counterDefaultVal={counterDefaultVal}/>
    } else {
      contentBody = <HomeSection dataState={this.state}/>
    }

    return (
      <div className="App">
        <Header />
        {contentBody}
      </div>
    );
  }
}

export default App;
