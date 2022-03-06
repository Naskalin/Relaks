import {ApiGetAllRequest, ApiGetAllResponse, getAllToUrl} from "./get_all";
import axios, {AxiosResponse} from "axios";
axios.defaults.headers.common = {
    'Accept': 'application/vnd.api+json',
    'Content-Type': 'application/vnd.api+json',
}
axios.interceptors.response.use((resp) => resp, (error) => {
    console.log(error);
    throw error;
})

export const jsonApi = {
    getAll: async (endPoint: string, apiRequest: ApiGetAllRequest) : Promise<ApiGetAllResponse> => {
        const url = getAllToUrl(endPoint, apiRequest);
        return (await axios.get(url)).data;
    }
}