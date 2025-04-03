import { API_URL, API_HEADER_MULTIPART_FORMDATA } from "@/constents/constants";
import { API_URL_TEXTURE } from "@/constents/pagesurls";
import axios from "axios";

export const TEXTURE_API = API_URL + API_URL_TEXTURE;

export const TextureFormGenerationData = {
  formName: "Add",
  formMethod: "post",
  formApiUrl: { API_URL_TEXTURE },
  formPurpose: "",
  headers: { ...API_HEADER_MULTIPART_FORMDATA },
  blocks: {
    basic: {
      blockName: "Basic",
      blockDescription: "",
      blockInputs: {
        name: {
          inputType: "text",
          inputLabel: "Name",
          inputName: "name",
          inputId: "name",
          inputValue: "",
          inputClasses: "input-text text-sm",
          inputPlaceHolder: "Enter Name",
          inputValidate: "required, NoSpecChar",
          inputDataAtribute: 'formid="from9876544", date="9876544"',
        },
        displayName: {
          inputType: "text",
          inputLabel: "Display Name",
          inputObjextKey: "displayName",
          inputName: "displayName",
          inputId: "displayName",
          inputValue: "",
          inputClasses: "input-text text-sm",
          inputPlaceHolder: "Enter Display Name",
          inputValidate: "required, NoSpecChar",
          inputDataAtribute: 'formid="from9876544", date="9876544"',
        },

        productOptionIds: {
          inputType: "multiselect",
          inputLabel: "Product Option",
          inputName: "productOptionIds",
          inputNameAlias: "productOptionIds",
          inputId: "productOptionIds",
          inputOptions: [{ value: "option1", text: "s" }],
          inputOptionValue: "id",
          inputOptionLabel: "displayName",
          inputValue: "",
          inputClasses: "input-text text-sm",
          inputPlaceHolder: "select product option",
          inputValidate: "required",
        },
        textureFile: {
          inputType: "file",
          inputLabel: "Texture File",
          inputName: "textureFile",
          inputNameAlias: "",
          inputId: "textureFile",
          inputValue: "",
          inputFileType: "jpg, jpeg, png",
          inputFileSize: "1",
          inputFileMultiple: false,
          inputClasses: "input-file text-sm",
          inputPlaceHolder: "Upload file",
          inputValidate: "required",
          inputDataAtribute: 'formid="from9876544", date="9876544"',
          inputAcceptedFormats: ".jpg,.png,.jpeg",
        },

        active: {
          inputType: "switch",
          inputLabel: "Active",
          inputName: "active",
          inputId: "active",
          inputValue: "",
          inputClasses: "input-text text-sm",
          inputPlaceHolder: "Enter Active",
          inputValidate: "",
          inputDataAtribute: 'formid="from9876544", date="9876544"',
        },
      },
    },
  },
};

export const addTextures = async (formData) => {
  const postHeader = formData?.headers;
  delete formData?.headers;
  try {
    const response = await axios.post(`${TEXTURE_API}`, formData, {
      headers: postHeader,
    });
    if (response.status === 200 || response.status === 201) {
      return response.data;
    } else {
      console.error("Error updating Options:", response.status);
    }
  } catch (error) {
    console.error("Error updating Options:", error);
    throw error;
  }
};

export const texturesById = async (id) => {
  // console.log("PRODUCT_CONFIG_API", PRODUCT_CONFIG_API);
  // console.log("id", id);
  try {
    const response = await axios.get(`${TEXTURE_API}/${id}`);
    if (response.status === 200) {
      return response.data;
    } else {
      console.error("Error retrieving Configurations:", response.status);
    }
  } catch (error) {
    console.error("Error retrieving Configurations:", error);
  }
};

export const updateTextures = async (formData) => {
  const id = formData.id;
  const postHeader = formData?.headers;
  delete formData?.headers;
  try {
    const response = await axios.put(`${TEXTURE_API}/${id}`, formData, {
      headers: postHeader,
    });
    if (response.status === 200 || response.status === 201) {
      return response.data;
    } else {
      console.error("Error updating Options:", response.status);
    }
  } catch (error) {
    console.error("Error updating Options:", error);
    throw error;
  }
};

export const getAllTextures = async (data) => {
  try {
    const response = await axios.get(TEXTURE_API, { params: data });
    if (response.status === 200) {
      return response.data;
    } else {
      console.error("Error retrieving products:", response.status);
    }
  } catch (error) {
    console.error("Error retrieving products:", error);
  }
};

export const getAllTexturesForSelectInput = async (data) => {
  const results = [{ text: "select", value: "0", item: {} }];
  try {
    const response = await axios.get(TEXTURE_API, { params: data });
    if (response.status === 200) {
      if (response.data && response.data.results) {
        response.data.results.map((item) => {
          results.push({ text: item.name, value: item.id, item: item });
        });
        return results;
      }
    } else {
      console.error("Error retrieving products:", response.status);
    }
  } catch (error) {
    console.error("Error retrieving products:", error);
  }
  return results;
};
export const deleteTextureById = async (id) => {
  try {
    const response = await axios.delete(`${TEXTURE_API}/${id}`);
    if (response.status === 200 || response.status === 201) {
      return response.data;
    } else {
      console.error("Error deleting Texture. Status:", response.status);
      throw new Error("Error deleting Texture");
    }
  } catch (error) {
    console.error("Error deleting Texture:", error);
    throw error;
  }
};
