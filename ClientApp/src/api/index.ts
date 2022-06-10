import axios, {AxiosRequestConfig, AxiosResponse} from "axios";
import {useApiErrorsStore} from "../store/api_errors_store";
import qs from 'qs';

const axiosApi = axios.create({
    baseURL: '/api',
    paramsSerializer: params => qs.stringify(params, {arrayFormat: 'repeat', skipNulls: true})
})

axiosApi.interceptors.response.use((resp) => resp, (error) => {
    const apiErrorsStore = useApiErrorsStore();
    apiErrorsStore.apiError = error?.response?.data;
    throw error;
})

export const appApi = {
    list: async (endPoint: string[], listRequest?: ApiAnyRequest): Promise<AxiosResponse> => {
        const url = endpointArrayBuilder(endPoint);
        return await axiosApi.get(url, {params: listRequest});
    },
    get: async (endPoint: string[], config?: AxiosRequestConfig): Promise<AxiosResponse> => {
        const url = endpointArrayBuilder(endPoint);
        return await axiosApi.get(url, config);
    },
    post: async (endPoint: string[], createRequest: ApiCreateRequest, config?: AxiosRequestConfig): Promise<AxiosResponse> => {
        const url = endpointArrayBuilder(endPoint);
        return await axiosApi.post(url, createRequest, config);
    },
    put: async (endPoint: string[], putRequest: ApiUpdateRequest): Promise<AxiosResponse> => {
        const url = endpointArrayBuilder(endPoint);
        return await axiosApi.put(url, putRequest);
    },
    delete: async (endPoint: string[]): Promise<AxiosResponse> => {
        const url = endpointArrayBuilder(endPoint);
        return await axiosApi.delete(url);
    }
}

export declare type ApiListRequest = {
    page?: number
    perPage?: number
    search?: string
    orderBy?: string
    orderByDesc?: string
    isDeleted?: boolean | null
}

export declare type ApiAnyRequest = {
    [index: string]: any
}

export declare type ApiCreateRequest = {} & ApiAnyRequest;
export declare type ApiUpdateRequest = {} & ApiCreateRequest;

export const endpointArrayBuilder = (arr: string[]): string => {
    return '/' + arr.join('/');   
}
