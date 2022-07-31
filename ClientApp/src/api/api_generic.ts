import {appApi} from "./index";

export function generateDefaultApi<TModel, TCreateRequest, TUpdateRequest, TListRequest, TGetRequest>(endpoint: string[]) {
    return {
        create: async (request: TCreateRequest): Promise<TModel> => {
            const resp = await appApi.post(endpoint, request);
            return resp.data
        },
        update: async (resourceId: string, request: TUpdateRequest): Promise<null> => {
            const resp = await appApi.put([...endpoint, resourceId], request);
            return resp.data;
        },
        get: async (resourceId: string): Promise<TModel> => {
            const resp = await appApi.get([...endpoint, resourceId]);
            return resp.data;
        },
        list: async (request: TListRequest): Promise<TModel[] | any> => {
            const resp = await appApi.list(endpoint, request);
            return resp.data;
        },
        delete: async (resourceId: string): Promise<null> => {
            const resp = await appApi.delete([...endpoint, resourceId]);
            return resp.data;
        }
    }
}

// export class GenerateDefaultApi<TModel, TCreateRequest, TUpdateRequest, TListRequest, TGetRequest> {
//     private readonly _endpoint: string[];
//
//     constructor(endpoint: string[]) {
//         this._endpoint = endpoint;
//     }
//     create = async (request: TCreateRequest): Promise<TModel> => {
//         const resp = await appApi.post(this._endpoint, request);
//         return resp.data
//     }
//     update = async (resourceId: string, request: TUpdateRequest): Promise<null> => {
//         const resp = await appApi.put([...this._endpoint, resourceId], request);
//         return resp.data;
//     }
//     get = async (resourceId: string): Promise<TModel> => {
//         const resp = await appApi.get([...this._endpoint, resourceId]);
//         return resp.data;
//     }
//     list = async (request: TListRequest): Promise<TModel[] | any> => {
//         const resp = await appApi.list(this._endpoint, request);
//         return resp.data;
//     }
//     delete = async (resourceId: string): Promise<null> => {
//         const resp = await appApi.delete([...this._endpoint, resourceId]);
//         return resp.data;
//     }
// }