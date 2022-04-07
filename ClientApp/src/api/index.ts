import axios, {AxiosResponse} from "axios";
import {appDefaults} from "../app_defaults";
const apiUrl = appDefaults.apiUrl;
axios.interceptors.response.use((resp) => resp, (error) => {
    // show popup
    console.log(error);
    throw error;
})

export const appApi = {
    list: async (endPoint: EndpointParams, apiRequest?: ApiAnyRequest): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.get(url, {params: apiRequest}));
    },
    get: async (endPoint: EndpointParams): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.get(url));
    },
    post: async (endPoint: EndpointParams, createRequest: ApiCreateRequest): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.post(url, createRequest));
    },
    put: async (endPoint: EndpointParams, createRequest: ApiUpdateRequest): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.put(url, createRequest));
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