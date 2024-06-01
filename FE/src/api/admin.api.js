import { ApiConstant } from "@/constant/api.constant";
import { api } from ".";

const adminApi = () => ({
    checkAdmin: async(id) => api.get(`${ApiConstant.admin.checkAdmin}/${id}`),
    getAnalysisToday: async() => api.get(`${ApiConstant.analysises.getAnalysises}`)
})

export const { checkAdmin,getAnalysisToday } = adminApi();
