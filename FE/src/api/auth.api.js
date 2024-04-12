import { apiDefault } from './index';
import { ApiConstant } from '@/constant/api.constant';

const authApi = () => ({
  register: async ({ email, password, birthday }) => {
    return apiDefault.post(ApiConstant.auth.register, {
      email,
      password,
      repeatPassword,
      birthday
    });
  }
});

export const { register } = authApi();
