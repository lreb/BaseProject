
import React from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";

import Layout from "./layout/layout";
import Favorites from "./favorities";

const WebPages = () => {
    return (
        <Router>
            <Layout>
                <Route exact path="/" component="{Home}" />
                <Route exact path="/Favorities" component="{Favorites}" />
            </Layout>
        </Router>
    );
}

export default WebPages;