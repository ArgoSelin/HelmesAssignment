import React, { Component } from 'react';
import CreateShipmentForm from "../Forms/CreateShipmentForm";

export class CreateShipment extends Component {
    render() {
        return (                
            <div>
                <h1>Create Shipment</h1>
                <CreateShipmentForm />
            </div>              
        )
    }
}
