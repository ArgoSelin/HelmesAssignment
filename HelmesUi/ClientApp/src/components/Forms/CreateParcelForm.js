import React from 'react';
import { useForm } from "react-hook-form";

export const CreateParcelForm = (props) => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    const onSubmit = async data => {

        const newData = {
            ParcelBagId: props.parcelBagId,
            ParcelNumber: data.parcelNumber,
            RecipientName: data.receipentName,
            DestinationCountry: data.destinationCountry,
            Weight: data.weight,
            Price: data.price
        }
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newData)
        }
        fetch('https://localhost:44382/api/Parcel/CreateParcel', requestOptions)
            .then(async response => {
                const isJson = response.headers.get('content-type')?.includes('application/json');
                const data = isJson && await response.json();
                if (!response.ok) {
                    const error = (data && data.message) || response.status;
                    return Promise.reject(error);
                }
                window.location.href = '/shipmentdetails/' + props.shipmentId;
            })
            .catch(error => {
                alert("Parcel number must be unique! You have allready inserted this parcel number into database. Also check if your destination code is correct.")
                console.error('There was an error!', error);
            });
    };
    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <div className="form-group">
                <label htmlFor="ParcelNumber">Parcel number</label>
                <input type="text" className="form-control" id="ParcelNumber"
                    placeholder="LLNNNNNNLL"
                    {...register("parcelNumber", { required: true, pattern: /^[a-zA-Z]{2}[1-9]{6}[a-zA-Z]{2}$/i })} />
                {errors.parcelNumber && errors.parcelNumber.type === "required" && <span>This is required</span>}
                {errors.parcelNumber && errors.parcelNumber.type === "pattern" && <span>Format “LLNNNNNNLL”, where L – letter, N – digit. Must be unique within entire database</span>}
            </div>
            <div className="form-group">
                <label htmlFor="ReceipentName">Receipent name</label>
                <input type="text" className="form-control" id="ReceipentName"
                    {...register("receipentName", { required: true, maxLength: 100 })} />
                {errors.receipentName && errors.receipentName.type === "required" && <span>This is required</span>}
                {errors.receipentName && errors.receipentName.type === "maxLength" && <span>Max length exceeded</span>}
            </div>
            <div className="form-group">
                <label htmlFor="DestinationCountry">Destination country two letter code</label>
                <input type="text" className="form-control" id="DestinationCountry"
                    placeholder="ET"
                    {...register("destinationCountry", { required: true, pattern: /^[A-Z]{2}$/i })} />
                {errors.destinationCountry && errors.destinationCountry.type === "required" && <span>This is required</span>}
                {errors.destinationCountry && errors.destinationCountry.type === "pattern" && <span>Country code with two UPPERCASE letters</span>}
            </div>
            <div className="form-group">
                <label htmlFor="Weight">Weight</label>
                <input type="text" className="form-control" id="Weight"
                    placeholder="1"
                    {...register("weight", { required: true, pattern: /^\d+(.\d{1,3})?$/i })} />
                {errors.weight && errors.weight.type === "required" && <span>This is required</span>}
                {errors.weight && errors.weight.type === "pattern" && <span>Max three spaces after comma</span>}
            </div>
            <div className="form-group">
                <label htmlFor="Price">Price</label>
                <input type="text"  className="form-control" id="Price"
                    placeholder="1"
                    {...register("price", { required: true, pattern: /^\d+(.\d{1,2})?/i })} />
                {errors.price && errors.price.type === "pattern" && <span>Max two spaces after comma</span>}
                {errors.price && errors.price.type === "required" && <span>This is required</span>}
            </div>
            <div className="form-group">
                <button className="form-control btn btn-success" type="submit">
                    Create Parcel
        </button>
            </div>
        </form>
    );
};
export default CreateParcelForm;