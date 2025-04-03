interface MeshFormData {
  inputType: string;
  inputSelectedValueType?: string;
}
export const isValidJSON = (jsonString) => {
  try {
    JSON.parse(jsonString);
    return true;
  } catch (error) {
    return false;
  }
};

export const base64toFile = (dataURI: any) => {
  if (dataURI) {
    const byteString = atob(dataURI.split(",")[1]);
    const mimeString = dataURI.split(",")[0].split(":")[1].split(";")[0];
    const ab = new ArrayBuffer(byteString.length);
    const ia = new Uint8Array(ab);
    for (let i = 0; i < byteString.length; i++) {
      ia[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([ab], { type: mimeString });

    // Create a File object from blob
    const file = new File([blob], "canvasimage.jpg", { type: "image/jpeg" });

    return file;
  }
};

export const jsonToFormdata = (data: Record<string, any>) => {
  const formData = new FormData();
  for (const [key, value] of Object.entries(data)) {
    formData.append(key, value);
  }
  return formData;
};

export const getDefaultValueByType = (meshFormData: MeshFormData): any => {
  switch (meshFormData?.inputType) {
    case "color":
      return "#FFF";
    case "text":
      return "";
    case "number":
      return 0;
    case "select":
      if (meshFormData?.inputSelectedValueType == "number") {
        return 0;
      } else {
        return "-";
      }
    case "boolean":
      return false;
    // Add more cases for other types as needed
    default:
      return null; // Or any other default value you prefer for unknown types
  }
};
