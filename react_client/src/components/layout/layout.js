import React from 'react';
import Header from './header';
import Navigation from './navigation';
import Footer from './footer';

const Layout = ({ children }) => {
    return (
    <React.Fragment>
        <Header />
        <div className="navigationWrapper">
            <Navigation />
            <main>{children}</main>
        </div>
        <Footer />
    </React.Fragment>
    );
};
export default Layout;