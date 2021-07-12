import React, { useState } from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import CreateLetterBagForm from '../Forms/CreateLetterBagForm'
import AddBoxOutlinedIcon from '@material-ui/icons/AddBoxOutlined';

export function LetterBagModal(props){
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
  

  return (
    <>
      <Button variant="primary" className="float-right" onClick={handleShow}>
              Create letter bag <AddBoxOutlinedIcon />
      </Button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Add letter bag</Modal.Title>
        </Modal.Header>
              <Modal.Body>
                  <CreateLetterBagForm shipmentId = {props.shipmentId}/>
              </Modal.Body>
              </Modal>
    </>
  );
};