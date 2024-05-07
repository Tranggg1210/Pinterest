import { ApiConstant } from "@/constant/api.constant";
import { api } from ".";

const userApi = () => ({
    getCurrentUser: async() => api.get(ApiConstant.user.currentUser),
})

export const {getCurrentUser} = userApi();
