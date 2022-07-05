import {TimestampTypes, CustomInfo} from "../api_types";
import {ApiListRequest, appApi} from "../index";

export declare type InfoTemplateListRequest = {
    page?: number
    perPage?: number
    search?: string
}
export declare type InfoTemplateFormRequest = {
    title: string
    template: CustomInfo
}
export declare type InfoTemplate = {
    id: string
} & InfoTemplateFormRequest & TimestampTypes
export const apiInfoTemplate = {
    create: async (formRequest: InfoTemplateFormRequest): Promise<InfoTemplate> => {
        const resp = await appApi.post(['info-templates'], formRequest);
        return resp.data;
    },
    update: async (infoTemplateId: string, formRequest: InfoTemplateFormRequest): Promise<null> => {
        const resp = await appApi.put(['info-templates', infoTemplateId], formRequest);
        return resp.data;
    },
    get: async (infoTemplateId: string): Promise<InfoTemplate> => {
        const resp = await appApi.get(['info-templates', infoTemplateId]);
        return resp.data;
    },
    list: async (request: InfoTemplateListRequest): Promise<InfoTemplate[]> => {
        const resp = await appApi.list(['info-templates'], request);
        return resp.data;
    },
    delete: async (infoTemplateId: string): Promise<null> => {
        const resp = await appApi.delete(['info-templates', infoTemplateId]);
        return resp.data;
    },
}