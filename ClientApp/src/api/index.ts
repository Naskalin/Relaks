import {
    ApiGetAllRequest,
    ApiGetAllResponse,
    ApiGetRequest,
    getAllUrlBuilder,
    getUrlBuilder,
    ApiGetResponse
} from "./get";
import axios, {AxiosRequestConfig} from "axios";
import {apiUrl} from "../default";
import {useQuasar} from "quasar";

// const quasar = useQuasar();

axios.interceptors.response.use((resp) => resp, (error) => {
    // show popup
    console.log(error);
    // quasar.notify({
    //     message: 'Jim pinged you.',
    //     color: 'purple'
    // })
    throw error;
})
const defaultHeadersFormat = {
    'Accept': 'application/vnd.api+json',
    'Content-Type': 'application/vnd.api+json',
}
// axios.defaults.headers.common = {
//     ...defaultHeadersFormat
// }
// axios.defaults.headers.post = defaultHeadersFormat
// axios.defaults.headers.common = {
//     'Accept': 'application/vnd.api+json',
//     'Content-Type': 'application/vnd.api+json',
// }
// axios.defaults.headers.post = {
//     'Accept': 'application/vnd.api+json',
//     'Content-Type': 'application/vnd.api+json',
// }

export const jsonApi = {
    getAll: async (endPoint: EndpointParams, apiRequest: ApiGetAllRequest): Promise<ApiGetAllResponse> => {
        // https://www.jsonapi.net/usage/reading/filtering.html
        const endpointPath = endpointBuilder(endPoint);
        const url = getAllUrlBuilder(endpointPath, apiRequest);
        return (await axios.get(url)).data;
    },
    get: async (endPoint: EndpointParams, apiRequest?: ApiGetRequest): Promise<ApiGetResponse> => {
        const endpointPath = endpointBuilder(endPoint);
        const url = getUrlBuilder(endpointPath, apiRequest);
        return (await axios.get(url)).data;
    },
    post: async (endPoint: EndpointParams, apiPostData: ApiPostData): Promise<ApiGetResponse> => {
        // https://www.jsonapi.net/usage/writing/creating.html
        const endpointPath = endpointBuilder(endPoint);
        const url = apiUrl + endpointPath;
        return (await axios.post(url, apiPostData, {
            headers: defaultHeadersFormat
        })).data?.data;
    }
}

export declare type ApiRequestRelationItem = {
    type: string,
    id: string
}
export declare type ApiPostData = {
    data: {
        type: string
        attributes: {
            [index: string]: string | number | null
        }
        relationships?: {
            [index: string]: {
                data: ApiRequestRelationItem | Array<ApiRequestRelationItem>
            }
        }
    }
}
export declare type EndpointParams = {
    resource: string
    resourceId?: string
    relation?: string
}
export const endpointBuilder = (params: EndpointParams): string => {

    let urlPart = '/' + params.resource;
    if (params?.resourceId) {
        urlPart += '/' + params.resourceId;
    }
    if (params?.relation) {
        urlPart += '/' + params.relation;
    }

    return urlPart;
}