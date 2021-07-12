import React, { Component } from 'react';
import { format } from 'date-fns'

const airports = {
    1: 'TLL',
    2: 'RIX',
    3: 'HEL'
};

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
            <th>Airport</th>
            <th>Flight number</th>
                    <th>Flight date</th>
                    <th>Is finalized</th>
            <th>Show details</th>
          </tr>
        </thead>
        <tbody>
                {shipments.map(shipments =>
                    <tr key={shipments.id} className={`${shipments.isFinalized ?  "finalized-shipment" : ""}`} >
                        <td>{format(new Date(shipments.createdTime), 'dd.MM.yyyy HH:mm')}</td>
                        <td>{airports[shipments.airport]}</td>
                        <td>{shipments.flightNumber}</td>
                        <td>{format(new Date(shipments.flightDate), 'dd.MM.yyyy HH:mm')}</td>
                        <td>{shipments.isFinalized ? "Yes" : "No"}</td>
                        <td><button onClick={this._handleButtonClick} className="btn btn-outline-info" >Shipment details</button></td>
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
                {contents}
            </div>
      </div>
    );
  }

    async populateShipmentsData() {
        try {
            const response = await fetch(
                'https://localhost:44382/api/Shipment/GetAllShipments',
                { redirect: 'error' });
            const data = await response.json();
            console.log(data);
            this.setState({ shipments: data, loading: false });
        } catch {
            this.setState({
                shipments: [{ date: 'Unable to get shipment info' }],
                loading: false
            });
        }
    }

    _handleButtonClick = () => {
        //calculate your data here
        //then redirect:
        this.context.router.push({ 
            pathname: "/ShipmentDetails",
            /*state: { yourCalculatedData: data }*/
        });
    }
  
}
