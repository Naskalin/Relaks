import axios, {AxiosRequestConfig, AxiosResponse} from "axios";
import {appDefaults} from "../app_defaults";
const apiUrl = appDefaults.apiUrl;
import {useApiErrorsStore} from "../store/api_errors_store";

axios.interceptors.response.use((resp) => resp, (error) => {
    // console.log(error?.response?.data);
    const apiErrorsStore = useApiErrorsStore();
    apiErrorsStore.apiError = error?.response?.data;
    // console.log(error);
    throw error;
})

export const appApi = {
    list: async (endPoint: EndpointParams, listRequest?: ApiAnyRequest): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.get(url, {params: listRequest}));
    },
    get: async (endPoint: EndpointParams): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.get(url));
    },
    post: async (endPoint: EndpointParams, createRequest: ApiCreateRequest, config?: AxiosRequestConfig): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.post(url, createRequest, config));
    },
    put: async (endPoint: EndpointParams, putRequest: ApiUpdateRequest): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.put(url, putRequest));
    },
    delete: async (endPoint: EndpointParams): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.delete(url));
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

export declare type EndpointParams = {
    resource: string
    resourceId?: string
    subResource?: string
    subResourceId?: string
}
export const endpointBuilder = ({resource, resourceId, subResource, subResourceId}: EndpointParams): string => {
    let urlParts = [resource];
    if (resourceId) {
        urlParts.push(resourceId);
    }
    if (subResource) {
        urlParts.push(subResource);
    }
    if (subResourceId) {
        urlParts.push(subResourceId)
    }

    return '/' + urlParts.join('/');
}