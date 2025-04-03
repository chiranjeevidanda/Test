
import { API_URL, API_URL_FINANANCIAL_DOCUMENT } from "../constents/constants";
import axios from "axios";

export const FinancialDocumentFormGenerationData = {
    formName: "Add",
    formMethod: "post",
    formApiUrl: "",
    formPurpose: "",
    headers: "",
    blocks: {
        basic: {
            blockName: "Basic",
            blockDescription: "",
            blockInputs: {
                documentType: {
                    inputType: "Select",
                    inputLabel: "Document Type",
                    inputName: "documentType",
                    inputId: "documentType",
                    inputValue: "",
                    inputClasses: "input-text text-sm",
                    inputPlaceHolder: "Enter Name",
                    inputValidate: "required, NoSpecChar",
                    inputDataAtribute: 'formid="from9876544", date="9876544"',
                },
                dateFrom: {
                    inputType: "date",
                    inputLabel: "Date From",
                    inputObjextKey: "dateFrom",
                    inputName: "dateFrom",
                    inputId: "dateFrom",
                    inputValue: "",
                    inputClasses: "input-text text-sm",
                    inputPlaceHolder: "Enter Display Name",
                    inputValidate: "required, NoSpecChar",
                    inputDataAtribute: 'formid="from9876544", date="9876544"',
                },
                dateTo: {
                    inputType: "date",
                    inputLabel: "Date To",
                    inputObjextKey: "dateTo",
                    inputName: "dateTo",
                    inputId: "dateTo",
                    inputValue: "",
                    inputClasses: "input-text text-sm",
                    inputPlaceHolder: "Enter Display Name",
                    inputValidate: "required, NoSpecChar",
                    inputDataAtribute: 'formid="from9876544", date="9876544"',
                },

                documentId: {
                    inputType: "text",
                    inputLabel: "Document Id",
                    inputName: "documentId",
                    inputId: "documentId",
                    inputValue: "",
                    inputClasses: "input-text text-sm",
                    inputPlaceHolder: "Enter Name",
                    inputValidate: "required, NoSpecChar",
                    inputDataAtribute: 'formid="from9876544", date="9876544"',
                },

                status: {
                    inputType: "Select",
                    inputLabel: "Status",
                    inputName: "status",
                    inputId: "status",
                    inputValue: "",
                    inputClasses: "input-text text-sm",
                    inputPlaceHolder: "Enter Name",
                    inputValidate: "required, NoSpecChar",
                    inputDataAtribute: 'formid="from9876544", date="9876544"',
                },


            },
        },
    },
};

export const FINANCIAL_DOCUMENT = API_URL + API_URL_FINANANCIAL_DOCUMENT;



// export const getAllFinancialDocument = async () => {
//     try {
//         const response = await axios.get(FINANCIAL_DOCUMENT,);
//         console.log(response);
//         if (response.status === 200) {
//             return response.data;
//         } else {
//             console.error("Error retrieving products:", response.status);
//         }
//     } catch (error) {
//         console.error("Error retrieving products:", error);
//     }
// };


