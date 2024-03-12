import { useAuthStore } from '@/stores/auth';

export const auth = (to, from, next) => {
  const authStore = useAuthStore();
  if (!!to.meta.requiresAuth && !authStore.loggedIn) {
    return next('/login?redirect=' + to.fullPath);
  }
  return next();
};
