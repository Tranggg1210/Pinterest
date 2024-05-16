import { LocalStorage } from '@/constant/localStorage.constant';
import { defineStore, acceptHMRUpdate } from 'pinia';

export const useCurrentUserStore = defineStore('currentUser', () => {
  const currentUser = ref(JSON.parse(localStorage.getItem(LocalStorage.currentUser)) || {});
  const save = (_currentUser) => {
    currentUser.value = _currentUser;
    localStorage.setItem(LocalStorage.currentUser, JSON.stringify(currentUser.value));
  };
  const clear = () => {
    currentUser.value = {};
    localStorage.removeItem(LocalStorage.currentUser);
  };

  return { currentUser, save, clear };
});

if (import.meta.hot) {
  import.meta.hot.accept(acceptHMRUpdate(useCurrentUserStore, import.meta.hot));
}
