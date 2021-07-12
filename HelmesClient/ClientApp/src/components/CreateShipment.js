import React, { Component } from 'react';
import { FormErrors } from './FormErrors';
import AirportList from "./AirportList";
import CreateShipmentForm from "./CreateShipmentForm";

export class CreateShipment extends Component {
    constructor(props) {
        super(props);
        this.state = {
            shipmentNumber: '',
            flightNumber: '',
            formErrors: { shipmentNumber: '',  flightNumber: '' },
            shipmentNumberValid: false,
            flightNumberValid: false,
            formValid: false
        };
        this.handleChange = this.handleChange.bind(this);
        this.onFormSubmit = this.onFormSubmit.bind(this);
    }

    validateField(fieldName, value) {
        let fieldValidationErrors = this.state.formErrors;
        let shipmentNumberValid = this.state.shipmentNumberlValid;
        let flightNumberValid = this.state.flightNumberlValid;

        switch (fieldName) {
            case 'shipmentNumber':
                shipmentNumberValid = value.match(/^([a-zA-Z1-9]{3}-[a-zA-Z1-9]{6})$/i);
                fieldValidationErrors.shipmentNumber = shipmentNumberValid ? '' : ' is invalid';
                break;
            case 'flightNumber':
                flightNumberValid = value.match(/^([1-9]{2}[a-zA-Z]{4})$/i);
                fieldValidationErrors.flightNumber = flightNumberValid ? '' : ' is invalid';
                break;
            default:
                break;
        }
        this.setState({
            formErrors: fieldValidationErrors,
            shipmentNumberValid: shipmentNumberValid,
            flightNumberValid: flightNumberValid
        }, this.validateForm);
    }

    validateForm() {
        this.setState({ formValid: this.state.shipmentNumberValid && this.state.flightNumberValid});
    }

    errorClass(error) {
        return (error.length === 0 ? '' : 'has-error');
    }

    handleChange(e) {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value },
            () => { this.validateField(name, value) });
    }

    onFormSubmit(e) {
        e.preventDefault();
    }

    render() {
        return (                

            <div>
                <h1>Create Shipment</h1>
                <CreateShipmentForm />
            </div>

               
        )
    }
}
