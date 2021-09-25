import axios, { AxiosInstance } from "axios";

let baseURL: string;

if ('__PRERENDER_INJECTED' in window) {
  baseURL = "http://localhost:46590/api";
} else {
  baseURL = process.env.VUE_APP_API_BASE_URL;
}

const apiClient: AxiosInstance = axios.create({
  baseURL: baseURL,
  headers: {
    "Content-type": "application/json",
  },
});

export default apiClient;
