import React from 'react';
import { useForm } from "react-hook-form";

export const CreateParcelBagForm = (props) => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    const onSubmit = async data => {

        const newData = {
            ShipmentId: props.shipmentId,
            BagNumber: data.bagNumber,
            IsFinalized: false
        }
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newData)
        }
        console.log(newData);
        fetch('https://localhost:44382/api/Bag/CreateParcelBag', requestOptions)
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
                alert("Bag number must be unique! You have allready inserted this bag number into database.")
                console.error('There was an error!', error);
            });
    };
    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <div className="form-group">
                <label htmlFor="BagNumber">Bag number</label>
                <input type="text" className="form-control" id="BagNumber"
                    {...register("bagNumber", { required: true, pattern: /^[0-9a-zA-Z''-'\s]{1,15}$/i })} />
                {errors.bagNumber && errors.bagNumber.type === "required" && <span>This is required</span>}
                {errors.bagNumber && errors.bagNumber.type === "pattern" && <span>Max length 15 characters, no special symbols allowed</span>}
            </div>
            <div className="form-group">
                <button className="form-control btn btn-success" type="submit">
                    Create Parcel bag
        </button>
            </div>
        </form>
    );
};
export default CreateParcelBagForm;