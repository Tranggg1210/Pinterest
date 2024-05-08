import axios from 'axios';
import { LocalStorage } from '@/constant/localStorage.constant';
import router from '@/router';

const api = axios.create({
  baseURL: `${import.meta.env.VITE_API_URL}/api`,
  headers: {
    'Content-Type': 'application/json',
    'Accept-Language': 'vn',
    'ngrok-skip-browser-warning': '69420',
  }
});
api.interceptors.request.use((config) => {
  const accessToken = JSON.parse(localStorage.getItem(LocalStorage.auth))?.token;
  config.headers.Authorization = `Bearer ${accessToken}`;
  return config;
}, Promise.reject);
api.interceptors.response.use(
  (value) => value.data,
  (error) => {
    if (error.code === 401) {
      localStorage.removeItem(LocalStorage.auth);
      router.push('/login');
    }
    return Promise.reject(error);
  }
);

const apiUpload = axios.create({
  baseURL: `${import.meta.env.VITE_API_URL}/api`,
  headers: {
    'Content-Type': 'multipart/form-data',
    'Accept-Language': 'vn',
    'ngrok-skip-browser-warning': '69420',
  }
});
apiUpload.interceptors.request.use((config) => {
  const accessToken = JSON.parse(localStorage.getItem(LocalStorage.auth))?.token;
  config.headers.Authorization = `Bearer ${accessToken}`;
  return config;
}, Promise.reject);
apiUpload.interceptors.response.use(
  (value) => value.data,
  (error) => {
    if (error.code === 401) {
      localStorage.removeItem(LocalStorage.auth);
      router.push('/login');
    }
    return Promise.reject(error);
  }
);

const apiDefault = axios.create({
  baseURL: `${import.meta.env.VITE_API_URL}/api`,
  headers: {
    'Content-Type': 'application/json',
    'Accept-Language': 'vn',
    'ngrok-skip-browser-warning': '69420'
  }
});

export { api, apiDefault, apiUpload };
