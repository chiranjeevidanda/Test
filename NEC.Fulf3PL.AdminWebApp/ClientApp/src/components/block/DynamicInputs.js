import { PhotoIcon } from "@heroicons/react/24/outline";
import React, { useState, useEffect, useRef, useCallback } from "react";
import { FORM_PURPOSE_VIEW } from "@/constents/constants";
import { HexColorPicker } from "react-colorful";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import Select from "react-select";



const currentDate = new Date();

const options = {
  year: "numeric",
  month: "long",
  day: "numeric",
  hour: "numeric",
  minute: "numeric",
  second: "numeric",
  hour12: true,
};

const formattedDate = currentDate.toLocaleDateString("en-US", options);

const DynamicInputs = ({
  inputData,
  initialData,
  formErrors,
  inputHandleChange,
  rowIndex,
}) => {
  const [inputDefaultValue, setInputDefaultValue] = useState({});
  const [showColorPicker, setShowColorPicker] = useState(false);
  const [selectedColor, setSelectedColor] = useState("");
  const [typedColor, setTypedColor] = useState("");
  const [typedColorCode, setTypedColorCode] = useState("");
  const [colorValidationMessage, setColorValidationMessage] = useState("");
  const [showInfoBubble, setShowInfoBubble] = useState(false);

  const [loading, setLoading] = useState(false);

  const colorInputRef = useRef([]);
  const closeColorPicker = useCallback(() => {
    setShowColorPicker(false);
  }, []);

  useEffect(() => {
    if (initialData) {
      setInputDefaultValue(initialData);
    }
  }, [initialData]);

  useEffect(() => {
    // console.log(inputDefaultValue);
  }, [inputDefaultValue]);

  function ShowSelectedFile(inputName, inputNameAlias) {
    if (inputDefaultValue && inputDefaultValue["name"]) {
      return inputDefaultValue["name"];
    } else if (inputDefaultValue && inputDefaultValue["fileName"]) {
      return inputDefaultValue["fileName"];
    } else {
      if (
        (inputDefaultValue && inputDefaultValue.toString().includes("jpg")) ||
        inputDefaultValue.toString().includes("png") ||
        inputDefaultValue.toString().includes("jpeg")
      ) {
        return (
          <div className=" mt-2 flex justify-center rounded-lg border border-dashed border-gray-900/25 px-3 py-5">
            <div className={`text-center`}>
              <img
                className="mx-auto "
                width="100"
                src={inputDefaultValue.toString().trim()}
              />
              {/* <h6 className="text-base">
              {inputDefaultValue.toString().split("/").pop()}
            </h6> */}
            </div>
          </div>
        );
      } else {
        //  return JSON.stringify(inputDefaultValue);
        return (
          <PhotoIcon
            className="mx-auto h-12 w-12 text-gray-300"
            aria-hidden="true"
          />
        );
      }
    }
  }

  const showFileName = (inputName, inputNameAlias) => {
    if (inputDefaultValue && inputDefaultValue["name"]) {
      return inputDefaultValue["name"];
    } else if (inputDefaultValue && inputDefaultValue["fileName"]) {
      return inputDefaultValue["fileName"];
    } else {
      if (
        (inputDefaultValue && inputDefaultValue.toString().includes("jpg")) ||
        inputDefaultValue.toString().includes("png") ||
        inputDefaultValue.toString().includes("jpeg")
      ) {
        return "<img src='" + inputDefaultValue + "'/>";
      } else {
        return JSON.stringify(inputDefaultValue);
      }
    }
  };

  const validateColorCode = (colorCode) => {
    const hexColorRegex = /^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$/;

    if (!hexColorRegex.test(colorCode)) {
      return "Invalid color code. Please enter a valid color code.";
    }

    return "";
  };

  const handleColorChange = (index) => (color) => {
    const targetColorInputRef = colorInputRef.current[index];

    if (targetColorInputRef) {
      const colorCode = color || "";

      const validationMessage = validateColorCode(colorCode);
      setColorValidationMessage(validationMessage);

      if (!validationMessage) {
        targetColorInputRef.value = colorCode;

        inputHandleChange({ target: targetColorInputRef });
        setSelectedColor(colorCode);
      }
    }
  };

  const handleColorPickerClick = (inputName) => {
    setShowColorPicker((prevShowColorPicker) => !prevShowColorPicker);

    if (inputDefaultValue && inputDefaultValue[inputName]) {
      setSelectedColor(inputDefaultValue[inputName]);
      setTypedColor(inputDefaultValue[inputName]);
    }
    if (typedColor) {
      setSelectedColor(typedColor);
    }
  };

  const removeOption = (value, selectedOptions) => {
    const updatedOptions = selectedOptions.filter((option) => option !== value);
    return updatedOptions;
  };

  const handleMultiSelect =
    (inputName, valueIndex) => (e) => {
      // console.log(e);
      const targetColorInputRef = {
        name: inputName,
        value: [],
        type: "multiselect",
      };
      targetColorInputRef.value = Array.isArray(e)
        ? e.map((selected) => selected[valueIndex])
        : [];
      inputHandleChange({ target: targetColorInputRef });
    };

  const getSelectedMultiSelectData = (
    multiSelectOptions,
    initialData,
    optionValueIndex,
    optionLabelIndex
  ) => {
    if (Array.isArray(initialData) && initialData.length > 0) {
      return initialData.map((selectedOption) =>
        multiSelectOptions.find(
          (opt) => opt[optionValueIndex] === selectedOption
        )
      );
    }

    return [];
  };

  const generateInput = (
    input,
    index
  ) => {
    const {
      inputType,
      inputLabel,
      inputId,
      inputName,
      inputNameAlias,
      inputValue,
      inputClasses,
      inputPlaceHolder,
      inputOptions,
      inputOptionValue,
      inputOptionLabel,
      inputOptionSelectFirst,
      inputDataAtribute,
      inputReadonly,
      inputAcceptedFormats,
      inputFileType,
      inputFileSize,
      inputFileMultiple,
      inputFileReUpload = true,
      inputPurpose,
      inputMessage,
      inputMinDate,
    } = input;

    switch (inputType) {
      case "text":
      case "email":
      case "password":
      case "number":
        return (
          <div key={inputName} className="bg-white p-1 w-full">
            {inputLabel && (
              <label
                htmlFor={inputId || inputName}
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                {inputLabel}
              </label>
            )}
            <div className="mt-2">
              <div
                className={`flex rounded-md shadow-sm ring-1 ring-inset ring-gray-300 focus-within:ring-2 focus-within:ring-inset focus-within:ring-indigo-600  w-full border ${formErrors && formErrors[inputName]
                  ? "border-red-500"
                  : "border-gray-300"
                  }`}
              >
                <input
                  type={inputType}
                  id={inputId || inputName}
                  name={inputName}
                  value={initialData ?? inputValue ?? inputDefaultValue ?? ""}
                  className={`${inputClasses} read-only:cursor-not-allowed read-only:text-gray-500 disabled:cursor-not-allowed w-full flex-1 border-0 bg-transparent py-1.5 pl-1.5 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6`}
                  placeholder={inputPlaceHolder}
                  onChange={inputHandleChange}
                  readOnly={inputReadonly ?? false}
                  disabled={inputPurpose == FORM_PURPOSE_VIEW}
                  data-index={rowIndex ?? ""}
                  key={`${rowIndex}-${inputName}`}
                />
              </div>
              {formErrors && formErrors[inputName] && (
                <span className="error text-red-500 text-sm animate-pulse">
                  {formErrors[inputName]}
                </span>
              )}
            </div>
          </div>
        );
      case "date":
        return (
          <div key={inputName} className="bg-white p-1 w-full">
            {inputLabel && (
              <label
                htmlFor={inputName}
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                {inputLabel}
              </label>
            )}
            <div className="mt-2">
              <div
                className={`flex rounded-md shadow-sm ring-1 ring-inset ring-gray-300 focus-within:ring-2 focus-within:ring-inset focus-within:ring-indigo-600  w-full border ${formErrors && formErrors[inputName]
                  ? "border-red-500"
                  : "border-gray-300"
                  }`}
              >
                <DatePicker
                  id={inputId || inputName}
                  name={inputName}
                  onChange={inputHandleChange}
                  dateFormat="MMMM d, yyyy"
                  className="read-only:cursor-not-allowed disabled w-full flex-1 border-0 bg-transparent py-1.5 pl-1.5 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6"
                  readOnly={inputReadonly ?? false}
                  disabled={inputPurpose == FORM_PURPOSE_VIEW}
                  data-index={rowIndex ?? ""}
                  key={`${rowIndex}-${inputName}`}
                  maxDate={currentDate}
                />
              </div>
              {formErrors && formErrors[inputName] && (
                <span className="error text-red-500 text-sm animate-pulse">
                  {formErrors[inputName]}
                </span>
              )}
            </div>
          </div>
        );
      case "datetime":
        return (
          <div key={inputName} className="bg-white p-1 w-full">
            {inputLabel && (
              <label
                htmlFor={inputName}
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                {inputLabel}
              </label>
            )}
            <div className="mt-2">
              <div
                className={`flex rounded-md shadow-sm ring-1 ring-inset ring-gray-300 focus-within:ring-2 focus-within:ring-inset focus-within:ring-indigo-600  w-full border ${formErrors && formErrors[inputName]
                  ? "border-red-500"
                  : "border-gray-300"
                  }`}
              >
                <DatePicker
                  id={inputId || inputName}
                  name={inputName}
                  selected={initialData ? new Date(initialData) : null}
                  onChange={(date) =>
                    inputHandleChange({
                      target: { name: inputName, value: date.toISOString() },
                    })
                  }
                  showTimeSelect
                  timeFormat="HH:mm"
                  timeIntervals={15}
                  timeCaption="Time"
                  dateFormat="MMMM d, yyyy h:mm aa"
                  className="read-only:cursor-not-allowed disabled w-full  border-0 bg-transparent py-1.5 pl-1.5 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6"
                  readOnly={inputReadonly ?? false}
                  disabled={inputPurpose == FORM_PURPOSE_VIEW}
                  data-index={rowIndex ?? ""}
                  key={`${rowIndex}-${inputName}`}
                  minDate={inputMinDate ? new Date(inputMinDate) : new Date()}
                />
              </div>
              {formErrors && formErrors[inputName] && (
                <span className="error text-red-500 text-sm animate-pulse">
                  {formErrors[inputName]}
                </span>
              )}
            </div>
          </div>
        );
      case "hidden":
        return (
          <input
            type={inputType}
            id={inputId || inputName}
            name={inputName}
            defaultValue={initialData ?? inputValue ?? inputDefaultValue ?? ""}
            placeholder={inputPlaceHolder}
            onChange={inputHandleChange}
            readOnly={inputReadonly ?? false}
            disabled={inputPurpose == FORM_PURPOSE_VIEW}
            data-index={rowIndex ?? ""}
            key={`${rowIndex}-${inputName}`}
          />
        );
      case "checkbox":
      case "switch":
        return (
          <div key={index} className="bg-white p-2 px-1">
            <label
              htmlFor={inputName}
              className="block text-sm font-medium leading-6 text-gray-900"
            >
              {inputLabel}
            </label>
            <div className="mt-2 ">
              <div className="flex sm:max-w-md relative z-0">
                <div className="mb-[0.125rem] mr-4 inline-block min-h-[1.5rem] pl-[1.5rem]">
                  <input
                    type="checkbox"
                    role="switch"
                    id={inputId || inputName}
                    name={inputName}
                    checked={initialData === true || initialData === "true"}
                    className={`${inputClasses} disabled:cursor-not-allowed mr-2 mt-[0.3rem] h-3.5 w-8 appearance-none rounded-[0.4375rem] bg-neutral-300 before:pointer-events-none before:absolute before:h-3.5 before:w-3.5 before:rounded-full before:bg-transparent before:content-[''] after:absolute after:z-[2] after:-mt-[0.1875rem] after:h-5 after:w-5 after:rounded-full after:border-none after:bg-neutral-100 after:shadow-[0_0px_3px_0_rgb(0_0_0_/_7%),_0_2px_2px_0_rgb(0_0_0_/_4%)] after:transition-[background-color_0.2s,transform_0.2s] after:content-[''] checked:bg-primary checked:after:absolute checked:after:z-[2] checked:after:-mt-[3px] checked:after:ml-[1.0625rem] checked:after:h-5 checked:after:w-5 checked:after:rounded-full checked:after:border-none checked:after:bg-green-600 checked:after:shadow-[0_3px_1px_-2px_rgba(0,0,0,0.2),_0_2px_2px_0_rgba(0,0,0,0.14),_0_1px_5px_0_rgba(0,0,0,0.12)] checked:after:transition-[background-color_0.2s,transform_0.2s] checked:after:content-[''] hover:cursor-pointer focus:outline-none focus:ring-0 focus:before:scale-100 focus:before:opacity-[0.12] focus:before:shadow-[3px_-1px_0px_13px_rgba(0,0,0,0.6)] focus:before:transition-[box-shadow_0.2s,transform_0.2s] focus:after:absolute focus:after:z-[1] focus:after:block focus:after:h-5 focus:after:w-5 focus:after:rounded-full focus:after:content-[''] checked:focus:border-primary checked:focus:bg-primary checked:focus:before:ml-[1.0625rem] checked:focus:before:scale-100 checked:focus:before:shadow-[3px_-1px_0px_13px_#3b71ca] checked:focus:before:transition-[box-shadow_0.2s,transform_0.2s] dark:bg-neutral-600 dark:after:bg-neutral-400 dark:checked:bg-primary dark:checked:after:bg-primary dark:focus:before:shadow-[3px_-1px_0px_13px_rgba(255,255,255,0.4)] dark:checked:focus:before:shadow-[3px_-1px_0px_13px_#3b71ca]`}
                    onChange={inputHandleChange}
                    data-index={rowIndex ?? ""}
                    disabled={inputReadonly}
                    key={`${rowIndex}-${inputName}`}
                  />
                </div>
              </div>
              {formErrors && formErrors[inputName] && (
                <span className="error text-red-500 text-sm animate-pulse">
                  {formErrors[inputName]}
                </span>
              )}
            </div>
          </div>
        );
      case "color":
        return (
          <div key={index} className="bg-white p-1 w-full">
            <div className="relative">
              {inputLabel && (
                <label
                  htmlFor={inputName}
                  className="block text-sm font-medium leading-6 text-gray-900"
                >
                  {inputLabel}
                </label>
              )}
              <div className="mt-2">
                <div
                  className={`flex rounded-md shadow-sm ring-1 ring-inset ring-gray-300 focus-within:ring-2 focus-within:ring-inset focus-within:ring-indigo-600 sm:max-w-md ${formErrors && formErrors[inputName]
                    ? "border-red-500"
                    : "border-gray-300"
                    }`}
                >
                  <input
                    type="text"
                    ref={(input) => (colorInputRef.current[inputName] = input)}
                    id={inputId || inputName}
                    name={inputName}
                    defaultValue={
                      initialData ?? inputValue ?? inputDefaultValue ?? ""
                    }
                    className={`${inputClasses} read-only:cursor-not-allowed disabled w-full flex-1 border-0 bg-transparent py-1.5 pl-1.5 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6 cursor-pointer`}
                    placeholder={inputPlaceHolder}
                    readOnly={inputReadonly ?? false}
                    disabled={inputReadonly ?? false}
                    data-index={rowIndex ?? ""}
                    key={`${rowIndex}-${inputName}`}
                    onChange={(event) => {
                      inputHandleChange(event);
                      setTypedColor(event.target.value);
                      setSelectedColor(event.target.value);
                      setColorValidationMessage(
                        validateColorCode(event.target.value)
                      );
                    }}
                  />
                  <div
                    className="w-10 h-10 rounded-lg ml-1 cursor-pointer"
                    style={{
                      background: initialData || selectedColor,
                      border: "1px solid #D1D5DB",
                    }}
                    onClick={handleColorPickerClick}
                  />
                </div>
                {colorValidationMessage && (
                  <span className="error text-red-500 text-sm">
                    {colorValidationMessage}
                  </span>
                )}
                {!inputReadonly && showColorPicker && (
                  <HexColorPicker
                    color={
                      typedColorCode ||
                      initialData ||
                      selectedColor ||
                      typedColor ||
                      "#fff"
                    }
                    onChange={handleColorChange(inputName)}
                    onBlur={(event) => {
                      if (!event.currentTarget.contains(event.relatedTarget)) {
                        setShowColorPicker(false);
                      }
                    }}
                    className="right-0"
                    style={{ position: "absolute", zIndex: 1000 }}
                  />
                )}
              </div>
              {formErrors && formErrors[inputName] && (
                <span className="error text-red-500 text-sm animate-pulse">
                  {formErrors[inputName]}
                </span>
              )}
            </div>
          </div>
        );
      case "radio":
        return (
          <div key={index} className="bg-white p-2 px-1">
            <label
              htmlFor={inputName}
              className="block text-sm font-medium leading-6 text-gray-900"
            >
              {inputLabel}
            </label>
            <div className="mt-2 flex justify-center align-middle">
              <input
                type="radio"
                id={inputId || inputName}
                name={inputName}
                onClick={inputHandleChange}
                onChange={(e) => {
                  console.log(e.target);
                }}
                data-index={rowIndex ?? ""}
                disabled={inputReadonly}
                checked={initialData ?? false}
                className="h-4 w-4 text-primary focus:ring-primary border-gray-300 rounded-md disabled:cursor-not-allowed"
                value={"true"}
              />
              <span className="ml-2 text-sm text-gray-700">
                {/* Label text here */}
              </span>
            </div>
            {formErrors[inputName] && (
              <span className="error text-red-500 text-sm animate-pulse">
                {formErrors[inputName]}
              </span>
            )}
          </div>
        );
      case "file":
        return (
          <div key={index} className="col-span-full  p-2 px-1 ">
            {inputLabel && (
              <div className="relative inline-block">
                <div className="flex items-center">
                  <label
                    htmlFor={inputId || inputName}
                    className="block text-sm font-medium leading-6 text-gray-900"
                  >
                    {inputLabel}
                  </label>
                  {inputMessage && (
                    <div className="relative group ml-2">
                      {" "}
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        className="h-8 w-4 text-gray-500"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                      >
                        <path
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          strokeWidth="2"
                          d="M12 16v1m0-5h.01m0-6a9 9 0 11-9 9 9 9 0 019-9z"
                        />
                      </svg>
                      {/* <div className="absolute left-full top-1/2 transform -translate-y-1/2 bg-gray-800 text-white text-sm p-2 rounded-lg opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                        {inputMessage}
                      </div> */}
                      <div className="absolute left-full top-1/2 transform -translate-y-1/2 bg-gray-800 text-white text-sm p-2 rounded-lg opacity-0 group-hover:opacity-100 transition-opacity duration-300 w-64">
                        {inputMessage}
                      </div>
                    </div>
                  )}
                </div>
              </div>
            )}

            <div
              className={`mt-2 flex justify-center rounded-lg border border-dashed border-gray-900/25 px-2 py-2 ${formErrors[inputName] ? "border-red-500" : "border-gray-300"
                }`}
            >
              <div className={`text-center`}>
                <>
                  {inputFileType?.toString().includes("GLB") &&
                    typeof initialData == "object" ? (
                    <ShowSelectedFile
                      inputName={inputName}
                      inputNameAlias={inputNameAlias}
                      initialData={initialData}
                    />
                  ) : initialData instanceof File ||
                    initialData instanceof Blob ? (
                    <div className=" mt-2 flex justify-center rounded-lg border border-dashed border-gray-900/25 px-2 py-3">
                      <div className={`text-center`}>
                        <img
                          src={URL.createObjectURL(initialData)}
                          alt={`Image ${1}`}
                          width="100"
                          className="mx-auto"
                        />
                      </div>
                    </div>
                  ) : (
                    <ShowSelectedFile
                      inputName={inputName}
                      inputNameAlias={inputNameAlias}
                      initialData={initialData}
                    />
                  )}
                  {/* {showFileName(inputName, inputNameAlias)} */}
                  {inputReadonly != true &&
                    inputFileReUpload == true &&
                    inputPurpose != FORM_PURPOSE_VIEW && (
                      <>
                        <div className="mt-4  flex flex-row justify-center text-sm leading-6 text-gray-600">
                          <label
                            htmlFor={inputId || inputName}
                            className="relative cursor-pointer mx-3 rounded-md bg-white font-semibold text-indigo-600 focus-within:outline-none focus-within:ring-2 focus-within:ring-indigo-600 focus-within:ring-offset-2 hover:text-indigo-500"
                          >
                            <span>Upload a file</span>
                            <input
                              type="file"
                              id={inputId || inputName}
                              name={inputName}
                              data-filetype={inputFileType}
                              data-filesize={inputFileSize}
                              data-fileindex={inputName}
                              className={`${inputClasses} sr-only ${formErrors[inputName]
                                ? "border-red-500"
                                : "border-gray-300"
                                }`}
                              accept={inputAcceptedFormats}
                              onChange={inputHandleChange}
                              data-index={rowIndex ?? ""}
                              key={`${rowIndex}-${inputName}`}
                              multiple={inputFileMultiple}
                            />
                          </label>
                          {/* <p className="pl-1">or drag and drop</p> */}
                        </div>
                        <p className="text-xs leading-5 text-black">
                          Allowed Types <strong>{inputFileType}</strong> up to{" "}
                          <strong>{inputFileSize}MB</strong>
                        </p>
                        {formErrors[inputName] && (
                          <span className="error text-red-500 text-sm animate-pulse">
                            {formErrors[inputName]}
                          </span>
                        )}
                      </>
                    )}
                </>
              </div>
            </div>
          </div>
        );
      case "select":
        const selectOptions = inputOptions ?? {};
        const selectOptionValueIndex = inputOptionValue
          ? inputOptionValue
          : "value";
        const selectOptionLabelIndex = inputOptionLabel
          ? inputOptionLabel
          : "text";
        const selectOptionsArray = Object.entries(selectOptions).map(
          ([key, value]) => ({
            ...value,
          })
        );

        // Ensure initialData is not null or undefined
        const dataToUse = initialData ?? inputValue;

        // If initialData is "-", choose the appropriate value based on conditions
        const updatedData =
          dataToUse === "-"
            ? inputOptionSelectFirst
              ? selectOptions[0][selectOptionValueIndex]
              : dataToUse
            : dataToUse;

        return (
          <div key={index} className="bg-white p-1 px-1">
            {inputLabel && (
              <label
                htmlFor={inputName}
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                {inputLabel}
              </label>
            )}
            <div className="mt-2">
              {/* <div className="flex rounded-md shadow-sm ring-1 ring-inset ring-gray-300 focus-within:ring-2 focus-within:ring-inset focus-within:ring-indigo-600  w-full border border-gray-300"> */}
              <div
                className={`flex rounded-md shadow-sm ring-1 ring-inset ring-gray-300 focus-within:ring-2 focus-within:ring-inset focus-within:ring-indigo-600  w-full border ${formErrors && formErrors[inputName]
                  ? "border-red-500"
                  : "border-gray-300"
                  }`}
              >
                <select
                  id={inputId || inputName}
                  name={inputName}
                  value={updatedData || "no-data"}
                  className={`${inputClasses} border ${formErrors && formErrors[inputName]
                    ? "border-red-500"
                    : "border-gray-300"
                    } disabled:cursor-not-allowed h-[36px] flex-1 border-0 bg-transparent py-1.5 pl-1.5 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6 w-full`}
                  onChange={inputHandleChange}
                  data-index={rowIndex ?? ""}
                  key={`${rowIndex}-${inputName}`}
                  disabled={inputReadonly ?? false}
                >
                  <option value={""}>Select</option>
                  {selectOptionsArray.length >= 1 ? (
                    selectOptionsArray.map(
                      (
                        option,
                        optionIndex
                      ) => (
                        <option
                          key={optionIndex}
                          value={option[selectOptionValueIndex]}
                        >
                          {option[selectOptionLabelIndex]}
                        </option>
                      )
                    )
                  ) : (
                    <option value={"no-data"}>No data</option>
                  )}
                </select>
              </div>
              {formErrors && formErrors[inputName] && (
                <span className="error text-red-500 text-sm animate-pulse">
                  {formErrors[inputName]}
                </span>
              )}
            </div>
          </div>
        );
      case "multiselect":
        const multiSelectOptionValueIndex = inputOptionValue
          ? inputOptionValue
          : "value";
        const multiSelectOptionLabelIndex = inputOptionLabel
          ? inputOptionLabel
          : "text";

        // Ensure the selected values are in the correct format expected by react-select
        const formattedSelectedOptions = getSelectedMultiSelectData(
          inputOptions,
          initialData,
          multiSelectOptionValueIndex,
          multiSelectOptionLabelIndex
        );
        const multiSelectOptions = inputOptions || [];

        return (
          <div key={index} className="bg-white p-1 px-1">
            {inputLabel && (
              <label
                htmlFor={inputName}
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                {inputLabel}
              </label>
            )}
            <div className="mt-2 text-black">
              <div
                className={`flex rounded-md shadow-sm ring-1 ring-inset ring-gray-300 focus-within:ring-2 focus-within:ring-inset focus-within:ring-indigo-600  w-full ${formErrors && formErrors[inputName]
                  ? "border-red-500"
                  : "border-gray-300"
                  }`}
              >
                <Select
                  name={inputName}
                  value={formattedSelectedOptions}
                  options={multiSelectOptions}
                  closeMenuOnSelect={false}
                  onChange={handleMultiSelect(
                    inputName,
                    multiSelectOptionValueIndex
                  )}
                  getOptionLabel={(option) => {
                    return option[multiSelectOptionLabelIndex];
                  }}
                  getOptionValue={(option) => {
                    return option[multiSelectOptionValueIndex];
                  }}
                  isMulti
                  isSearchable={true}
                  isDisabled={inputReadonly ?? false}
                  className={`${inputClasses} border ${formErrors && formErrors[inputName]
                    ? "border-red-500"
                    : "border-gray-300"
                    } disabled:cursor-not-allowed w-full focus:outline-none focus:border-blue-500`}
                />
              </div>
              {formErrors && formErrors[inputName] && (
                <span className="error text-red-500 text-sm animate-pulse">
                  {formErrors[inputName]}
                </span>
              )}
            </div>
          </div>
        );
      case "textarea":
        return (
          <div key={index} className="bg-white p-2 px-1">
            <label
              htmlFor={inputName}
              className="block text-sm font-medium leading-6 text-gray-900"
            >
              {inputLabel}
            </label>
            <div className="mt-2">
              <div
                className={`flex rounded-md shadow-sm ring-1 ring-inset ring-gray-300 focus-within:ring-2 focus-within:ring-inset focus-within:ring-indigo-600 sm:max-w-md ${formErrors && formErrors[inputName]
                  ? "border-red-500"
                  : "border-gray-300"
                  }`}
              >
                <textarea
                  id={inputId || inputName}
                  name={inputName}
                  value={inputValue || inputValue || ""}
                  className={inputClasses}
                  placeholder={inputPlaceHolder}
                  onChange={() => inputHandleChange}
                  data-index={rowIndex ?? ""}
                  key={`${rowIndex}-${inputName}`}
                />
              </div>
              {formErrors[inputName] && (
                <span className="error text-red-500 text-sm animate-pulse">
                  {formErrors[inputName]}
                </span>
              )}
            </div>
          </div>
        );
      default:
        return null;
    }
  };

  return <>{generateInput(inputData)}</>;
};

export default DynamicInputs;
