import axios, {AxiosResponse} from "axios";
import {apiUrl} from "../default";
axios.interceptors.response.use((resp) => resp, (error) => {
    // show popup
    console.log(error);
    throw error;
})

export const appApi = {
    list: async (endPoint: EndpointParams, apiRequest?: ApiListRequest | ApiAnyRequest): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.get(url, {params: apiRequest})).data;
    },
    get: async (endPoint: EndpointParams): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.get(url)).data;
    },
    post: async (endPoint: EndpointParams, createRequest: ApiCreateRequest): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.post(url, createRequest)).data;
    },
    put: async (endPoint: EndpointParams, createRequest: ApiUpdateRequest): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.put(url, createRequest)).data;
    },
    delete: async (endPoint: EndpointParams): Promise<AxiosResponse> => {
        const url = apiUrl + endpointBuilder(endPoint);
        return (await axios.delete(url)).data;
    }
}

export declare type ApiListRequest = {
    page?: number
    perPage?: number
    search?: string
    orderBy?: string
    orderByDesc?: string
}

export declare type ApiAnyRequest = {
    [index: string]: string | number | null
}

export declare type ApiCreateRequest = {
    [index: string]: string | number | null
}
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