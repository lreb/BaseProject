import React from 'react';
import '../styles/header.css';
import logo from '../../../src/logo.svg';

const Header = () => {
return (
    <header className="header">
        <img src={logo} alt="Xcelvations Logo" height="40" /> header content text
    </header>
    );
};
export default Header;