import React from "react";
import { useForm, Controller } from "react-hook-form";
import Select from "react-select";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

export default function CreateShipmentForm() {
    const { register, handleSubmit, control, formState: { errors } } = useForm();

    const airportList = [
        {
            value: 1,
            label: "TLL"
        },
        {
            value: 2,
            label: "RIX"
        },
        {
            value: 3,
            label: "HEL"
        }
    ];

    const onSubmit = async data => {
        const newData = {
            ShipmentNumber: data.shipmentNumber,
            Airport: data.AirportList.value,
            FlightNumber: data.flightNumber,
            FlightDate: data.FlightDatePicker,
            BagList: null,
            IsFinalized: false
        }
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newData)
        }

        fetch('https://localhost:44382/api/Shipment/CreateShipment', requestOptions)
            .then(async response => {
                const isJson = response.headers.get('content-type')?.includes('application/json');
                const data = isJson && await response.json();
                if (!response.ok) {
                    const error = (data && data.message) || response.status;
                    return Promise.reject(error);
                }
                window.location.href = '/shipmentdetails/' + data.id;               
            })
            .catch(error => {
                alert("Shipment number must be unique! You have allready inserted this shipment number into database.")
                console.error('There was an error!', error);
            });
    };


    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <div className="form-group">
                <label htmlFor="shipmentNumber">Shipment number</label>
                <input type="text" className="form-control" id="shipmentNumber"
                    placeholder="XXX-XXXXXX"
                    {...register("shipmentNumber", { required: true, pattern: /^([a-zA-Z1-9]{3}-[a-zA-Z1-9]{6})$/i})} />
                {errors.shipmentNumber && errors.shipmentNumber.type === "required" && <span>This is required</span>}
                {errors.shipmentNumber && errors.shipmentNumber.type === "pattern" && <span>Must be in form of XXX-XXXXXX where x is letter or digit</span>}
            </div>
            <div className="form-group">
                <label htmlFor="flightNumber">Flight number</label>
                <input type="text" className="form-control" id="flightNumber"
                    placeholder="22LLLL"
                    {...register("flightNumber", { required: true, pattern: /^[1-9]{2}[a-zA-Z]{4}$/i })} />
                {errors.flightNumber && errors.flightNumber.type === "required" && <span>This is required</span>}
                {errors.flightNumber && errors.flightNumber.type === "pattern" && <span>Must be in form of LLNNNN where L is letter and N is digit</span>}
            </div>
            <div className="form-group">
                <label htmlFor="AirportList">Destination airport</label>
                <Controller
                    rules={{ required: true }}
                    control={control}
                    name="AirportList"
                    render={({ field  }) => (
                        <Select id="airportList"
                            {...field}
                            options={airportList}
                        />
                    )}
                />
                {errors.airportList && errors.airportList.type === "required" && <span>This is required</span>}
            </div>
            <div className="form-group">
                <label htmlFor="FlightDatePicker">Destination airport</label><br/>
                <Controller
                    rules={{ required: true }}
                    control={control}
                    name='FlightDatePicker'
                    render={({ field }) => (
                        <DatePicker className="form-control" id="flightDatePicker"
                            placeholderText='Select date'
                            onChange={(date) => field.onChange(date)}
                            selected={field.value}
                            minDate={new Date()}
                            showTimeSelect
                            dateFormat="dd.MM.yyyy hh:mm"
                        />
                    )}
                />
                {errors.flightDatePicker && errors.flightDatePicker.type === "required" && <span>This is required</span>}
            </div>

            <button type="submit" className="btn btn-success">Create shipment</button>
        </form>
    );
}