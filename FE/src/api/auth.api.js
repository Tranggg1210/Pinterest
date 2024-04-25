import { apiDefault } from './index';
import { ApiConstant } from '@/constant/api.constant';

const authApi = () => ({
  login: async({email, passowrd}) => apiDefault.post(ApiConstant.auth.login, {email, passowrd}),
  register: async ({ email, password, birthday }) => {
    return apiDefault.post(ApiConstant.auth.register, {
      email,
      password,
      repeatPassword,
      birthday
    });
  }
});

export const { login,register } = authApi();
