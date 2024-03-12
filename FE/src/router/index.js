import { createRouter, createWebHistory } from 'vue-router';
import { setupLayouts } from 'virtual:generated-layouts';
import generatedRoutes from '~pages';

import { auth } from '@/middlewares/auth.js';

const routes = setupLayouts(generatedRoutes);

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
});
router.beforeEach(auth);
router.afterEach((to) => {
  if (to.path === '/') document.title = 'pinterest';
  else document.title = 'pinterest - ' + to.name;
});

export default router;
