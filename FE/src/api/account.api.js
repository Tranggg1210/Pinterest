import { ApiConstant } from "@/constant/api.constant";
import { api } from ".";

const accountApi = () => ({
    getAllAccount: async() => api.get(ApiConstant.account.getAllAccount),
    editRoles: async(id, roles) => api.post(`${ApiConstant.account.editAccount}/${id}`, [...roles])
})

export const { getAllAccount,editRoles } = accountApi();
