

export const formatDate = (date) => {
  const d = new Date(date);
  const year = d.getFullYear();
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const day = String(d.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
};

export const isValidJSON = (jsonString) => {
  try {
    JSON.parse(jsonString);
    return true;
  } catch (error) {
    return false;
  }
};

export const base64toFile = (dataURI) => {
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

export const jsonToFormdata = (data) => {
  const formData = new FormData();
  for (const [key, value] of Object.entries(data)) {
    formData.append(key, value);
  }
  return formData;
};

export const getDefaultValueByType = (meshFormData) => {
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

//for downloading excels
export const downloadExcel = ({ payload, filename }) => {
  const blob = new Blob([payload], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
  const url = window.URL.createObjectURL(blob);
  const link = document.createElement('a');
  link.href = url;
  link.setAttribute('download', filename);
  document.body.appendChild(link);
  link.click();
  link.parentNode.removeChild(link);
};