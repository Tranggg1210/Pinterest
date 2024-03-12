import { LocalStorage } from '@/constant/localStorage.constant';
import { defineStore, acceptHMRUpdate } from 'pinia';

export const useAuthStore = defineStore('auth', () => {
  const auth = ref(JSON.parse(localStorage.getItem(LocalStorage.auth)) || {});
  const loggedIn = computed(() => !!auth.value.accessToken);
  const save = (_auth) => {
    auth.value = _auth;
    localStorage.setItem(LocalStorage.auth, JSON.stringify(auth.value));
  };
  const clear = () => {
    auth.value = {};
    localStorage.removeItem(LocalStorage.auth);
  };

  return { auth, loggedIn, save, clear };
});

if (import.meta.hot) {
  import.meta.hot.accept(acceptHMRUpdate(useAuthStore, import.meta.hot));
}
