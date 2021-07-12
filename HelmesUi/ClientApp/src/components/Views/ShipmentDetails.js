import React, { Component } from 'react';
import { format } from 'date-fns'
import { Link } from "react-router-dom";
import AirportList from '../AirportList';
import { LetterBagModal } from '../Modals/LetterBagModal';
import { ParcelBagModal } from '../Modals/ParcelBagModal';
import { ParcelModal } from '../Modals/ParcelModal';
import DoneOutlineOutlinedIcon from '@material-ui/icons/DoneOutlineOutlined';
import { confirmAlert } from 'react-confirm-alert'; 
import 'react-confirm-alert/src/react-confirm-alert.css'; 


export class ShipmentDetails extends Component {

    constructor(props) {
        super(props);
        this.state = {
            showDialog: false,
            shipmentId: this.props.match.params.id,
            shipmentDetails: [], loadingShipment: true,
            shipmentLetterBagDetails: [], loadingLetter: true,
            shipmentParcelBagDetails: [], loadingParcel: true,
        };
    }

    componentDidMount() {
        this.populateShipmentsData().then(this.populateShipmentLetterBagData()).then(this.populateShipmentParcelBagData());
    }

    static renderShipmentDetailsTable(shipment) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Created date</th>
                        <th>Shipment number</th>                      
                        <th>Airport</th>
                        <th>Flight number</th>
                        <th>Flight date</th>
                        <th>Is finalized</th>
                    </tr>
                </thead>
                <tbody>                   
                    <tr key={shipment.id} className={`${shipment.isFinalized ? "finalized-shipment" : ""}`} >
                        <td>{format(new Date(shipment.createdTime), 'dd.MM.yyyy HH:mm')}</td>
                        <td>{shipment.shipmentNumber}</td>
                        <td>{AirportList[shipment.airport-1].label}</td>
                        <td>{shipment.flightNumber}</td>
                        <td>{format(new Date(shipment.flightDate), 'dd.MM.yyyy HH:mm')}</td>
                        <td>{shipment.isFinalized ? "Yes" : "No"}</td>
                        </tr>                   
                </tbody>
            </table>
        );
    }

    static renderLetterBagListForShipment(letterBag) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Created date</th>
                        <th>Bag number</th>
                        <th>Letter count</th>
                        <th>Weight</th>
                        <th>Price</th>                        
                    </tr>
                </thead>
                <tbody>
                    {letterBag.length === 0 &&
                        <tr className="text-center"><td colSpan='5'>No letter bags in the shipment </td></tr>
                    }

                    {letterBag.map(letterBag =>
                        <tr key={letterBag.id} >
                            <td>{format(new Date(letterBag.createdTime), 'dd.MM.yyyy HH:mm')}</td>
                            <td>{letterBag.bagNumber}</td>
                            <td>{letterBag.letterCount}</td>
                            <td>{letterBag.weight}</td>
                            <td>{letterBag.price}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    static renderParcelBagListForShipment(parcelBag) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Created date</th>
                        <th>Bag number</th>
                        <th>Parcel count</th>
                        <th>Add parcel to bag</th>
                    </tr>
                </thead>
                <tbody>
                    { 
                        parcelBag.length === 0 &&
                        <tr className="text-center"><td colSpan='4'>No parcel bags in the shipment</td></tr>
                    }                    
                    {parcelBag.map(parcelBag =>
                        <tr key={parcelBag.id} >
                            <td>{format(new Date(parcelBag.createdTime), 'dd.MM.yyyy HH:mm')}</td>
                            <td>{parcelBag.bagNumber}</td>
                            <td>{parcelBag.parcelList === null ? '0' : parcelBag.parcelList.length}</td>
                            <td> {parcelBag.isFinalized ? " " : <ParcelModal shipmentId={parcelBag.shipmentId} parcelBagId={parcelBag.id} />}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    submit = () => {
        confirmAlert({
            customUI: ({ onClose }) => {
                return (
                    <div className='custom-ui'>
                        <h1>Are you sure?</h1>
                        <p>Click yes for submit</p>
                        <div className="col-md-12">
                            <button className="btn btn-success"
                            onClick={() => {
                                this.handleSubmitData();
                                onClose();
                            }}> Yes</button>
                            <button onClick={onClose} className="btn btn-danger float-right">No</button></div>
                    </div>
                );
            }
        });
    };

    render() {
        let contents = this.state.loadingShipment
            ? <p><em>Loading...</em></p>
            : ShipmentDetails.renderShipmentDetailsTable(this.state.shipmentDetails);

        let letterBagContents = this.state.loadingLetter
            ? <p><em>Loading letter bag list...</em></p>
            : ShipmentDetails.renderLetterBagListForShipment(this.state.shipmentLetterBagDetails);

        let parcelBagContents = this.state.loadingParcel
            ? <p><em>Loading parcel bag list...</em></p>
            : ShipmentDetails.renderParcelBagListForShipment(this.state.shipmentParcelBagDetails);

        return (
            <div className="row">
                <div className="col-md-12">
                    <h1 id="tabelLabel" >Shipment details</h1>
                    <Link to="/">
                        <button className="btn btn-info">Back</button>
                    </Link>
                    <button className="btn btn-outline-success float-right" disabled={(this.state.shipmentDetails.isFinalized ? true : false)} onClick={this.submit}>
                        Finalize Shipment <DoneOutlineOutlinedIcon />
                     </button>
                </div>
                <div className="col-md-12">
                    <div className="table-responsive">{contents}</div>
                </div>
                <div className="col-md-12">
                    <h2 className="table-top-heading">Letter bag list in shipment {this.state.shipmentDetails.isFinalized ? " " : <LetterBagModal shipmentId={this.state.shipmentId} />} </h2>
                    <div className="table-responsive">{letterBagContents}</div>
                </div>
                <div className="col-md-12">
                    <h2 className="table-top-heading">Parcel bag list in shipment {this.state.shipmentDetails.isFinalized ? " " : <ParcelBagModal shipmentId={this.state.shipmentId} />} </h2>
                    <div className="table-responsive">{parcelBagContents}</div>
                </div>                
            </div>
        );
    }

    async handleSubmitData() {
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' }
        }
        await  fetch('https://localhost:44382/api/Shipment/FinalizeShipment?id=' + this.state.shipmentId, requestOptions)
            .then(async response => {
                const isJson = response.headers.get('content-type')?.includes('application/json');
                const data = isJson && await response.json();
                if (!response.ok) {
                    const error = (data && data.message) || response.status;
                    return Promise.reject(error);
                }
                console.log(data)
                window.location.href = '/';
            })
            .catch(error => {
                console.error('There was an error!', error);
            });
    };

    async populateShipmentsData() {
        await fetch(
            'https://localhost:44382/api/Shipment/GetShipmentDetails?id=' + this.state.shipmentId ).then(res => res.json())
            .then(
                (result) => {
                    this.setState({ shipmentDetails: result, loadingShipment: false });
                },

                (error) => {
                    this.setState({ shipmentDetails: [], loadingShipment: false, error });
                }
        )
    }

    async populateShipmentLetterBagData() {
            await fetch(
                'https://localhost:44382/api/Bag/GetShipmentLetterBagList?id=' + this.state.shipmentId).then(res => res.json())
                .then(
                    (result) => {
                        this.setState({ shipmentLetterBagDetails: result, loadingLetter: false });
                    },

                    (error) => {
                        this.setState({ shipmentLetterBagDetails: [], loadingLetter: false, error });
                    }
                )
    }

    async populateShipmentParcelBagData() {
        await fetch(
            'https://localhost:44382/api/Bag/GetShipmentParcelBagList?id=' + this.state.shipmentId).then(res => res.json())
            .then(
                (result) => {
                    console.log(result);
                    this.setState({ shipmentParcelBagDetails: result, loadingParcel: false });
                },

                (error) => {
                    this.setState({ shipmentParcelBagDetails:[], loadingParcel: false, error });
                }
            )
    }

}