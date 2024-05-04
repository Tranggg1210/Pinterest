import { apiDefault } from './index';
import { ApiConstant } from '@/constant/api.constant';

const authApi = () => ({
  login: async({userName, password}) => apiDefault.post(ApiConstant.auth.login, {userName, password}),
  register: async ({firstName, lastName, email, password}) => {
    return apiDefault.post(ApiConstant.auth.register, {
      firstName,
      lastName,
      email,
      password
    });
  }
});

export const { login,register } = authApi();
