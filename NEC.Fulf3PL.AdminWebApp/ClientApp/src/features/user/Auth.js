import { useSelect } from "@react-three/drei";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Modal } from "semantic-ui-react";
import Login from "./Login";
import Register from "./Register";
import { changeActiveAuthScreen, getActiveAuthScreen } from "./userSlice";

const Auth = ({ initialScreen = "register", isOpen, onModalClose }) => {
  const activeScreen = useSelector(getActiveAuthScreen);

  const onShowRegisterClick = (evt) => {
    evt?.preventDefault?.();
    evt?.stopPropogation?.();
  };

  const onShowLoginClick = (evt) => {
    evt?.preventDefault?.();
    evt?.stopPropogation?.();
  };

  return (
    <Modal
      id="nebyu-auth-modal-container"
      closeIcon
      open={isOpen}
      onClose={onModalClose}
      size="tiny"
    >
      {activeScreen === "login" ? (
        <Login onShowRegisterClick={onShowRegisterClick} />
      ) : (
        <Register onShowLoginClick={onShowLoginClick} />
      )}
    </Modal>
  );
};

export default Auth;
