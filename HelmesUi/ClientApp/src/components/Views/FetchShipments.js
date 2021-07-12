import React, { Component } from 'react';
import { format } from 'date-fns'
import { Link } from "react-router-dom";
import AirportList from '../AirportList'


export class FetchShipments extends Component {
    static displayName = FetchShipments.name;

  constructor(props) {
      super(props);
    this.state = { shipments: [], loading: true };
    }

  componentDidMount() {
      this.populateShipmentsData();
    }

    static renderShipmentsTable(shipments) {
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
            <th>Show details</th>
          </tr>
        </thead>
            <tbody>
                {shipments.length === 0 &&
                    <tr className="text-center"><td colSpan='7'>No shipments </td></tr>
                }

                {shipments.map(shipments =>
                    <tr key={shipments.id} className={`${shipments.isFinalized ? "finalized-shipment" : ""}`} >                        
                        <td>{format(new Date(shipments.createdTime), 'dd.MM.yyyy HH:mm')}</td>
                        <td>{shipments.shipmentNumber}</td>
                        <td>{AirportList[shipments.airport - 1].label}</td>
                        <td>{shipments.flightNumber}</td>
                        <td>{format(new Date(shipments.flightDate), 'dd.MM.yyyy HH:mm')}</td>
                        <td>{shipments.isFinalized ? "Yes" : "No"}</td>
                        <td>
                            <Link
                                to={{
                                    pathname: `/shipmentdetails/${shipments.id}`,
                                }}
                            >
                                <button className="btn btn-outline-info">Shipment Details</button>
                            </Link>
                        </td>
                    </tr>
               
                )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : FetchShipments.renderShipmentsTable(this.state.shipments);

    return (
        <div className="row">
            <div className="col-md-12">
                <h1 id="tabelLabel" >Shipments table</h1>
                <div className="table-responsive">{contents}</div>
            </div>
      </div>
    );
  }

    async populateShipmentsData() {
        await fetch(
            'https://localhost:44382/api/Shipment/GetAllShipments').then(res => res.json())
            .then(
                (result) => {
                    this.setState({ shipments: result, loading: false });
                },

                (error) => {
                    this.setState({ shipments: [], loading: false, error });
                }
            )
    }
  
}
