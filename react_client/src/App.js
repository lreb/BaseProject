import './App.css';
import React from 'react';
import Header from './components/layout/header'
import TeslaBattery from './components/demo/TeslaBattery';

function App() {

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

  return (
    <div className="App">
      <Header />
      main app component
      <TeslaBattery counterDefaultVal={counterDefaultVal}/>
    </div>
  );
}

export default App;
