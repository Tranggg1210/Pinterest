import { ApiConstant } from "@/constant/api.constant";
import { api } from (".")

const adminApi = () => {
    checkAdmin: async(id) => api.get(`${ApiConstant.checkAdmin}/${id}`)
}

export const {checkAdmin} = adminApi;
