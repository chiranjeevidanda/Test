"use client";
import { deleteTableRowApi } from "@/apis";
import { toast } from "react-toastify";
import { idText } from "typescript";
import validator from "validator";

export const handleInputChange = (event: {
  target: {
    files?: any;
    name?: any;
    dataset?: any;
    value?: any;
    type?: string;
    checked?: true | false;
  };
}) => {
  const { name, dataset, value, type, checked } = event.target;
  const files: File =
    event?.target?.files && Object.keys(event?.target?.files).length > 0
      ? event?.target?.files
      : null;

  // console.log(typeof value)

  switch (type) {
    case "file":
      const fileType = dataset.filetype.toLowerCase();
      const fileSize = dataset.filesize;
      const allowedFileTypes = fileType; //.split(",");
      if (Object.keys(event?.target?.files).length > 0) {
        const filesArrays = Array.from(event?.target?.files || []);

        filesArrays.map((fileItem: any, index) => {
          let uploadedFileType: any;
          if (fileItem.type) {
            uploadedFileType = fileItem.type.split("/")[1];
          } else {
            uploadedFileType = fileItem.name.split(".")[1];
          }
          if (!allowedFileTypes.includes(uploadedFileType)) {
            alert(`${uploadedFileType} file type is not allowed.`);
            delete filesArrays[index];
            return {};
          }
          // Size validation
          const maxSizeInBytes = fileSize * 1024 * 1024; // 5MB
          if (fileItem && fileItem.size > maxSizeInBytes) {
            alert(`File size should not exceed ${fileSize}MB.`);
            filesArrays.slice(index);
            return {};
          }
        });
        if (filesArrays.length == 1) {
          return {
            [name]: filesArrays[0],
          };
        }
        return {
          [name]: filesArrays,
        };
      }
      return {};
      break;
    case "checkbox":
      return { [name]: checked };
      break;
    case "select-one":
      if (dataset?.selectedvaluetype) {
        let parsedValue;

        if (dataset?.selectedvaluetype === "string") {
          parsedValue = value.toString();
        } else if (dataset?.selectedvaluetype === "number") {
          parsedValue = parseInt(value);
        } else {
          parsedValue = value;
        }
        return {
          [name]: parsedValue,
        };
      }
      return { [name]: value };

      break;
    default:
      return {
        [name]: value,
      };
      break;
  }
};

export const validateForm = (inputsData: any, inputValues: any) => {
  return new Promise((resolve, reject) => {
    const formErrors = {};
    const errors = {};

    Object.values(inputsData).forEach((input: any) => {
      const { inputName, inputValidate } = input;

      // if (!inputValues[inputName]) return;  //validation is not working with this code so commented

      let value =
        inputValues[inputName] && inputValues[inputName] !== undefined
          ? inputValues[inputName].toString()
          : ""; // Use empty string if value is undefined

      // Convert the boolean value to a string for 'required' validation
      if (typeof value === "boolean") {
        value = value.toString();
      }

      const validations = inputValidate.split(",").map((v) => v.trim());

      // Check if there's already an error for this input
      if (errors[inputName]) return;
      delete errors[inputName];

      // Regular expression to match emojis
      const emojiRegex = /\p{Extended_Pictographic}/gu;
      const alphanumericRegex = /^[a-zA-Z0-9-_ ]+$/;

      let valueLength = value.length; // Separate variable for numeric comparison

      errors[inputName] = "";

      validations.forEach((validation) => {
        if (validation === "atleastone" && value.length === 0) {
          errors[inputName] = "Select atleast one item.";
        }
        if (validation === "required" && value.length === 0) {
          errors[inputName] = "This field is required.";
        } else if (validation.startsWith("minlength:") && value) {
          const minLength = parseFloat(validation.split(":")[1]);

          if (valueLength < minLength) {
            errors[inputName] = `Minimum Length should be ${minLength}.`;
          }
        } else if (validation.startsWith("colorcode") && value) {
          const colorCodeRegex = /^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$/;
          if (!colorCodeRegex.test(value)) {
            errors[inputName] = "Invalid color code format.";
          }
        }

        if (emojiRegex.test(value)) {
          errors[inputName] = "Input text contains an emoji.";
        } else if (validation.startsWith("maxlength:") && value) {
          const maxLength = parseFloat(validation.split(":")[1]);

          if (valueLength > maxLength) {
            errors[inputName] = `Maximum Length should be ${maxLength}.`;
          }
          // else {
          //   errors[inputName] = "";
          // }
        } else if (validation.startsWith("minvalue:") && value) {
          const minValue = parseFloat(validation.split(":")[1]);
          if (valueLength < minValue) {
            errors[inputName] = `Minimum value should be ${minValue}.`;
          }
        } else if (validation.startsWith("maxvalue:") && value) {
          const maxValue = parseFloat(validation.split(":")[1]);
          if (value > maxValue) {
            errors[inputName] = `Maximum value should be ${maxValue}.`;
          } else {
            errors[inputName] = "";
          }
        } else if (
          validation === "string" &&
          !validator.isAlpha(value) &&
          value
        ) {
          errors[inputName] = "Only letters are allowed.";
        } else if (
          validation === "number" &&
          !validator.isNumeric(value) &&
          value
        ) {
          errors[inputName] = "Only numbers are allowed.";
        } else if (
          validation === "email" &&
          !validator.isEmail(value) &&
          value
        ) {
          errors[inputName] = "Invalid email format.";
        } else if (validation === "file" && !value) {
          const inputFile = document
            ? (document.getElementById(inputName) as HTMLInputElement)
            : null;

          if (inputFile && inputFile.files && inputFile.files.length === 0) {
            errors[inputName] = "Please select a file.";
          }
        } else if (validation === "NoSpecChar" && value) {
          if (!alphanumericRegex.test(value)) {
            errors[inputName] =
              "Only alphanumeric characters,- and _ are allowed.";
          }
        }

        // Add more validations as needed
      });

      if (!errors[inputName]) {
        delete errors[inputName];
      }
    });
    console.log("errors", errors);
    // Check if there are any errors, and resolve/reject the promise accordingly
    if (Object.keys(errors).length > 0) {
      reject(errors);
    } else {
      resolve(true);
    }
  });
};

export const hasDatePassed = (firstDate: any, secondDate: any) => {
  const selectedDate = new Date(firstDate);
  const givenDate = new Date(secondDate);

  return givenDate > selectedDate;
};
