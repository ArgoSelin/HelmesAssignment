
import React, { Component } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

const today = new Date();
export class FlightDatePicker extends Component {

    state = {
        startDate: new Date()
    };

    handleChange = date => {
        this.setState({
            startDate: date
        });
    };

    render() {

        return (
            <div>
                <DatePicker
                    selected={this.state.startDate}
                    onChange={this.handleChange}
                    minDate={today}
                    showTimeSelect
                    dateFormat="dd.MM.yyyy hh:mm"
                />
            </div>
        )
    };
}
