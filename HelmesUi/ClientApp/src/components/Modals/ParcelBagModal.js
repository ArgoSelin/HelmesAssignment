import React, { useState } from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import CreateParcelBagForm from '../Forms/CreateParcelBagForm '
import AddBoxOutlinedIcon from '@material-ui/icons/AddBoxOutlined';


export function ParcelBagModal(props) {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);


    return (
        <>
            <Button variant="primary" className="float-right" onClick={handleShow}>
                Create parcel bag <AddBoxOutlinedIcon />
      </Button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add parcel bag</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <CreateParcelBagForm shipmentId={props.shipmentId} />
                </Modal.Body>
            </Modal>
        </>
    );
};