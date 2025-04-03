import DeleteConformaionModel from "./DeleteConformaionModel";


import React, { useEffect, useState } from "react";
import FormButtonsSckeleton from "../sckeletons/FormButtonsSckeleton";
import { deleteTableRowApi } from "@/apis";
import { useRouter } from "next/router";
import { toast } from "react-toastify";
import { ALERT_SUCCESS_DEACTIVATE } from "@/constents/constants";

const DeleteFromEditForm = ({
  apiUrl = "",
  deleteRecordId = "0",
}) => {
  const [showModal, setShowModal] = useState(false);

  const handleCloseModal = () => {
    setShowModal(false);
  };

  const handleConfirmDelete = () => {
    // Perform the actual delete operation here
    deleteTableRowApi({ apiurl: apiUrl, id: deleteRecordId }).then(
      (responce) => {
        toast.success(ALERT_SUCCESS_DEACTIVATE + " ");

        // // Once deleted, close the modal
        // setDeleteRecordId(0);
        // setCustomDeleteMessage("Deactivated.");
        setTimeout(() => {
          setShowModal(false);
        }, 1000);
      }
    );
  };

  useEffect(() => { }, []);

  return (
    <>
      {showModal ? (
        <DeleteConformaionModel
          message={""}
          onClose={handleCloseModal}
          onDelete={handleConfirmDelete}
        ></DeleteConformaionModel>
      ) : (
        ""
      )}
    </>
  );
};

export default DeleteFromEditForm;
