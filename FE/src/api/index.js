import axios from 'axios';
import { LocalStorage } from '@/constant/localStorage.constant';
import router from '@/router';

const api = axios.create({
  baseURL: `${import.meta.env.VITE_API_URL}/api/v1`,
  headers: {
    'Content-Type': 'application/json',
    'Accept-Language': 'vn'
  }
});
api.interceptors.request.use((config) => {
  const accessToken = JSON.parse(localStorage.getItem(LocalStorage.auth))?.accessToken;
  config.headers.Authorization = 'Bearer ' + accessToken;
  return config;
}, Promise.reject);
api.interceptors.response.use(
  (value) => value.data,
  (error) => {
    if (error.response.status === 401 && !error.config.url.startsWith('auth/')) {
      localStorage.removeItem(LocalStorage.auth);
      router.push('/login');
    }
    return Promise.reject(error);
  }
);

const apiDefault = axios.create({
  baseURL: `${import.meta.env.VITE_API_URL}/api/v1`,
  headers: {
    'Content-Type': 'application/json',
    'Accept-Language': 'vn'
  }
});

export { api, apiDefault };
