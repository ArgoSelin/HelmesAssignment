import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { FetchShipments } from './components/Views/FetchShipments';
import { CreateShipment } from './components/Views/CreateShipment';
import { ShipmentDetails } from './components/Views/ShipmentDetails';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route path='/' exact={true} component={FetchShipments} />
                <Route exact path='/fetch-shipments' component={FetchShipments} />
                <Route path='/createshipment' component={CreateShipment} />
                <Route
                    exact
                    path="/shipmentdetails/:id"
                    component={ShipmentDetails}
                />
            </Layout>
        );
    }
}
