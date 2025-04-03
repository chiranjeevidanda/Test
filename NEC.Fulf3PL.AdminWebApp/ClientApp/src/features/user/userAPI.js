// A mock function to mimic making an async request for data
export function loginUser(detail = {}) {
  return new Promise((resolve) =>
    setTimeout(() => resolve({ data: detail }), 1000)
  );
}

export function registerUser(detail = {}) {
  return new Promise((resolve) =>
    setTimeout(() => resolve({ data: detail }), 1000)
  );
}
// https://nebyu-api-us-dev.azurewebsites.net/data/menu
