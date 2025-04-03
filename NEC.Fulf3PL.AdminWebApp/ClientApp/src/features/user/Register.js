import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  Button,
  Card,
  CardContent,
  CardMeta,
  Checkbox,
  Form,
  FormGroup,
  FormInput,
  Message,
} from "semantic-ui-react";
import { EMAIL_REGEX, PASSWORD_CRITERIA } from "./helper";
import {
  changeActiveAuthScreen,
  changeIsLoogedIn,
  getIsBusy,
  registerUserAsync,
} from "./userSlice";

const Register = () => {
  const [showPassword, setShowPassword] = useState(false);

  const isBusy = useSelector(getIsBusy);

  const dispatch = useDispatch();

  const [formFields, setFormFields] = useState({
    first_name: "",
    last_name: "",
    email: "",
    password: "",
  });

  const [errorFields, setErrorFields] = useState({
    first_name: "",
    last_name: "",
    email: "",
    password: "",
  });

  const onFormFieldChange = (evt) => {
    const { name, value } = evt?.target || {};

    let sanitizedValue = value;

    // if (name === "first_name" || name === "last_name") {
    //   sanitizedValue = (value + "").replace(/[^\w\s]/gi, "");
    // }

    setFormFields((prevState) => ({ ...prevState, [name]: sanitizedValue }));
  };

  useEffect(() => {
    const isFirstNameValid = /^[a-zA-Z0-9\s]+$/gi.test(formFields.first_name);
    const isLastNameValid = /^[a-zA-Z0-9]+$/gi.test(formFields.last_name);

    const isEmailValid = EMAIL_REGEX.test(formFields.email);

    const isPasswordValid = PASSWORD_CRITERIA.test(formFields.password);

    setErrorFields((prevState) => {
      const newState = { ...prevState };

      if (isFirstNameValid) {
        newState.first_name = "";
      } else {
        newState.first_name = "Letters, numbers and spaces only";
      }

      if (isLastNameValid) {
        newState.last_name = "";
      } else {
        newState.last_name = "Letters and numbers only";
      }

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

  const onSubmit = (evt) => {
    evt?.preventDefault?.();
    evt?.stopPropogation?.();
  };

  const onShowLoginClick = () => {
    dispatch(changeActiveAuthScreen("login"));
  };

  const isFormValid = () => {
    return !(
      errorFields.first_name ||
      errorFields.last_name ||
      errorFields.email ||
      errorFields.password
    );
  };

  const onRegisterClick = () => {
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
    <div className="register-container">
      <Card className="login-card">
        <CardContent>
          <CardMeta className="card-meta" textAlign="center">
            REGISTER
          </CardMeta>

          <div className="card-header">CREATE AN ACCOUNT</div>

          <CardMeta className="card-meta">
            Enter your info below to access your New Era account.
          </CardMeta>

          <Form onSubmit={onSubmit}>
            <FormGroup widths="equal">
              <div style={{ width: "50%" }}>
                <FormInput
                  name="first_name"
                  label="First name"
                  value={formFields.first_name}
                  onChange={onFormFieldChange}
                  width={16}
                  // error={!!(formFields.first_name && errorFields.first_name)}
                  className={
                    !!(formFields.first_name && errorFields.first_name)
                      ? "error-input"
                      : ""
                  }
                  required
                />

                {formFields.first_name && errorFields.first_name && (
                  <div className="error-label">{errorFields.first_name}</div>
                )}
              </div>

              <div style={{ width: "50%" }}>
                <FormInput
                  name="last_name"
                  label="Last name"
                  value={formFields.last_name}
                  onChange={onFormFieldChange}
                  width={16}
                  // error={!!(formFields.last_name && errorFields.last_name)}
                  className={
                    !!(formFields.last_name && errorFields.last_name)
                      ? "error-input"
                      : ""
                  }
                  required
                />

                {formFields.last_name && errorFields.last_name && (
                  <div className="error-label">{errorFields.last_name}</div>
                )}
              </div>
            </FormGroup>

            <FormGroup>
              <div style={{ width: "100%" }}>
                <FormInput
                  name="email"
                  label="Email"
                  value={formFields.email}
                  onChange={onFormFieldChange}
                  width={16}
                  // error={!!(formFields.email && errorFields.email)}
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
                  type={!showPassword ? "password" : "text"}
                  label="Password"
                  value={formFields.password}
                  onChange={onFormFieldChange}
                  width={16}
                  // error={!!(formFields.password && errorFields.password)}
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

            <FormGroup>
              <Checkbox
                label="Show Password"
                onChange={() => {
                  setShowPassword((prevState) => !prevState);
                }}
                checked={showPassword}
                style={{ padding: ".67857143em 1em" }}
              />
            </FormGroup>

            <FormGroup className="links-container">
              <div className="register-section-container">
                <CardMeta>Already have an account?</CardMeta>

                <div
                  className="link"
                  onClick={onShowLoginClick}
                  style={{ marginLeft: "0.5rem" }}
                >
                  SIGN IN NOW
                </div>
              </div>
            </FormGroup>
            <FormGroup>
              <label
                id="nebyu-register-error-lbl"
                style={{ color: "red" }}
              ></label>
            </FormGroup>
            <FormGroup>
              <Button
                type="button"
                fluid
                color="black"
                loading={isBusy}
                onClick={onRegisterClick}
                disabled={!isFormValid()}
                id="nebyu-register-btn"
              >
                CREATE YOUR ACCOUNT
              </Button>
            </FormGroup>
          </Form>
        </CardContent>
      </Card>
    </div>
  );
};

export default Register;
