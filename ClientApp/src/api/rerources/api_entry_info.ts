import {EntryInfo, EntryInfoFormRequest, EntryInfoListRequest} from "../api_types";
import {ApiListRequest, appApi} from "../index";

class ApiEntryInfoGeneric<TModel, TCreateRequest, TUpdateRequest, TListRequest> {
    private readonly _subResource: string;

    constructor(subResource: string) {
        this._subResource = subResource;
    }

    create = async (entryId: string, request: TCreateRequest): Promise<TModel> => {
        const resp = await appApi.post(['entries', entryId, this._subResource], request);
        return resp.data
    }
    update = async (entryId: string, entryInfoId: string, request: TUpdateRequest): Promise<null> => {
        const resp = await appApi.put(['entries', entryId, this._subResource, entryInfoId], request);
        return resp.data;
    }
    get = async (entryId: string, entryInfoId: string): Promise<TModel> => {
        const resp = await appApi.get(['entries', entryId, this._subResource, entryInfoId]);
        return resp.data;
    }
    list = async (entryId: string, request: TListRequest): Promise<TModel[]> => {
        const resp = await appApi.list(['entries', entryId, this._subResource], request);
        return resp.data;
    }
    delete = async (entryId: string, entryInfoId: string): Promise<null> => {
        const resp = await appApi.delete(['entries', entryId, this._subResource, entryInfoId]);
        return resp.data;
    }
}

export const apiEntryInfo = new ApiEntryInfoGeneric<EntryInfo, EntryInfoFormRequest, EntryInfoFormRequest, EntryInfoListRequest>('entry-infos');