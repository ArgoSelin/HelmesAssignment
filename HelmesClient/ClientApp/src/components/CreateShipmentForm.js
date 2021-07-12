import React from "react";
import { useForm } from "react-hook-form";

export default function CreateShipmentForm() {
    const { register, handleSubmit, watch, formState: { errors } } = useForm();
    const onSubmit = data => console.log(data);

    console.log(watch("example")); // watch input value by passing the name of it

    return (
        /* "handleSubmit" will validate your inputs before invoking "onSubmit" */
        <form onSubmit={handleSubmit(onSubmit)}>
            {/* register your input into the hook by invoking the "register" function */}
            <input defaultValue="test" {...register("example")} />

            {/* include validation with required or other standard HTML validation rules */}
            <input {...register("exampleRequired", { required: true })} />
            {/* errors will return when field validation fails  */}
            {errors.exampleRequired && <span>This field is required</span>}

            <input type="submit" />
            <button type="submit" className="btn btn-success" disabled={false}>Create shipment</button>
        </form>
    );
}

{/*<div className="panel panel-default">*/ }
{/*    <FormErrors formErrors={this.state.formErrors} />*/ }
{/*</div>*/ }
{/*<div className={`form-group ${this.errorClass(this.state.formErrors.shipmentNumber)}`}>*/ }
{/*    <label htmlFor="ShipmentNumber">Shipment number</label>*/ }
{/*    <input type="text" required className="form-control" name="ShipmentNumber"*/ }
{/*        placeholder="XXX-XXXXXX"*/ }
{/*        value={this.state.shipmentNumber}*/ }
{/*        onChange={this.handleChange} />*/ }
{/*</div>*/ }
{/*<div className={`form-group`}>*/ }
{/*    <label htmlFor="AirportList">Destination airport</label>*/ }
{/*    <AirportList  />                */ }
{/*</div>*/ }
{/*<div className={`form-group ${this.errorClass(this.state.formErrors.flightNumber)}`}>*/ }
{/*    <label htmlFor="FlightNumber">Flight number</label>*/ }
{/*    <input type="text" required className="form-control" name="FlightNumber"*/ }
{/*        placeholder="NN2222"*/ }
{/*        value={this.state.flightNumber}*/ }
{/*        onChange={this.handleChange} />*/ }
{/*</div>*/ }
{/*<div className={`form-group ${this.errorClass(this.state.formErrors.flightDate)}`}>*/ }
{/*    <label htmlFor="FlightDate">Flight date</label>*/ }

{/*    <DatePicker*/ }
{/*        selected={this.state.flightDate}*/ }
{/*        onChange={this.handleChange}*/ }
{/*            minDate={new Date()}*/ }
{/*            showTimeSelect*/ }
{/*            dateFormat="dd.MM.yyyy hh:mm"*/ }
{/*        />*/ }
{/*</div>*/ }