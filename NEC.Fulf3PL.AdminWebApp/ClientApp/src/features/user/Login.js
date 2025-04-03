import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import {
  Button,
  Card,
  CardContent,
  CardMeta,
  Form,
  FormGroup,
  FormInput,
} from "semantic-ui-react";
import { EMAIL_REGEX, PASSWORD_CRITERIA } from "./helper";
import {
  changeActiveAuthScreen,
  changeIsLoogedIn,
  getIsBusy,
  loginUserAsync,
} from "./userSlice";

const Login = () => {
  const [formFields, setFormFields] = useState({ email: "", password: "" });
  const [errorFields, setErrorFields] = useState({ email: "", password: "" });

  const isBusy = useSelector(getIsBusy);

  const dispatch = useDispatch();

  const onFormFieldChange = (evt) => {
    const { name, value } = evt?.target || {};

    setFormFields((prevState) => ({ ...prevState, [name]: value }));
  };

  const onSubmit = (evt) => {
    evt?.preventDefault?.();
    evt?.stopPropogation?.();
  };

  const onShowRegisterClick = () => {
    dispatch(changeActiveAuthScreen("register"));
  };

  useEffect(() => {
    const isEmailValid = EMAIL_REGEX.test(formFields.email);

    const isPasswordValid = PASSWORD_CRITERIA.test(formFields.password);

    setErrorFields((prevState) => {
      const newState = { ...prevState };

      if (isEmailValid) {
        newState.email = "";
      } else {
        newState.email = "Email is Invalid";
      }

      if (isPasswordValid) {
        newState.password = "";
      } else {
        newState.password = "Atleast 8 characters, 1 letter and 1 number";
      }

      return newState;
    });
  }, [formFields]);

  const isFormValid = () => {
    return !(errorFields.email || errorFields.password);
  };

  const onForgotPasswordClick = () => {
    window.location = window.location.origin + ` /account/login#recover`;
  };

  const onLoginClick = () => {
    let count = 0;
    checkLoggedIn(count);
  };

  let checkTimeout = 0;
  const checkLoggedIn = (count) => {
    if (checkTimeout) clearTimeout(checkTimeout);
    checkTimeout = setTimeout(() => {
      count++;
      if (!localStorage.getItem("emailId")) {
        if (count <= 20) {
          checkLoggedIn(count++);
        }
      } else {
        dispatch(changeIsLoogedIn(true));
      }
    }, 2000);
  };

  return (
    <div className="login-container">
      <Card className="card">
        <CardContent>
          <CardMeta className="card-meta" textAlign="center">
            SIGN IN
          </CardMeta>

          <div className="card-header">WELCOME BACK</div>

          <CardMeta className="card-meta" textAlign="center">
            Enter your info below to access your New Era account.
          </CardMeta>

          <Form onSubmit={onSubmit}>
            <FormGroup>
              <div style={{ width: "100%" }}>
                <FormInput
                  name="email"
                  label="Email"
                  value={formFields.email}
                  onChange={onFormFieldChange}
                  width={16}
                  className={
                    !!(formFields.email && errorFields.email)
                      ? "error-input"
                      : ""
                  }
                  required
                />

                {formFields.email && errorFields.email && (
                  <div className="error-label">{errorFields.email}</div>
                )}
              </div>
            </FormGroup>

            <FormGroup>
              <div style={{ width: "100%" }}>
                <FormInput
                  name="password"
                  type="password"
                  label="Password"
                  value={formFields.password}
                  onChange={onFormFieldChange}
                  width={16}
                  className={
                    !!(formFields.password && errorFields.password)
                      ? "error-input"
                      : ""
                  }
                  required
                />

                {formFields.password && errorFields.password && (
                  <div className="error-label">{errorFields.password}</div>
                )}
              </div>
            </FormGroup>

            <FormGroup className="links-container">
              <Link className="link" onClick={onForgotPasswordClick}>
                FORGOT YOUR PASSWORD ?
              </Link>

              <div className="register-section-container">
                <CardMeta>Don't have an account?</CardMeta>

                <div
                  className="link"
                  onClick={onShowRegisterClick}
                  style={{ marginLeft: "0.5rem" }}
                >
                  CREATE ACCOUNT
                </div>
              </div>
            </FormGroup>
            <FormGroup>
              <label
                id="nebyu-login-error-lbl"
                style={{ color: "red" }}
              ></label>
            </FormGroup>
            <FormGroup>
              <Button
                type="button"
                fluid
                color="black"
                loading={isBusy}
                onClick={onLoginClick}
                disabled={!isFormValid()}
                id="nebyu-login-btn"
              >
                SIGN IN
              </Button>
            </FormGroup>
          </Form>
        </CardContent>
      </Card>
    </div>
  );
};

export default Login;
