import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { FetchShipments } from './components/FetchShipments';
import { CreateShipment } from './components/CreateShipment';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route path='/' exact={true} component={FetchShipments} />
                <Route exact path='/fetch-shipments' component={FetchShipments} />
                <Route path='/createshipment' component={CreateShipment} />
            </Layout>
        );
    }
}
