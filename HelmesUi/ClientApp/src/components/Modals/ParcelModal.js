import React, { useState } from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import CreateParcelForm from '../Forms/CreateParcelForm'
import AddBoxOutlinedIcon from '@material-ui/icons/AddBoxOutlined';

export function ParcelModal(props) {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);


    return (
        <>
            <Button variant="primary" onClick={handleShow}>
                Add parcel to bag <AddBoxOutlinedIcon/>
      </Button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add parcel </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <CreateParcelForm shipmentId={props.shipmentId} parcelBagId={props.parcelBagId}/>
                </Modal.Body>
            </Modal>
        </>
    );
};