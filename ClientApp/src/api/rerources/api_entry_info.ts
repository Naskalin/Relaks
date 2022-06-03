import {EntryInfo, EntryInfoFormRequest, EntryInfoListRequest} from "../api_types";
import {ApiListRequest, appApi} from "../index";

class ApiEntryInfoGeneric<TModel, TCreateRequest, TUpdateRequest, TListRequest> {
    private readonly _subResource: string;

    constructor(subResource: string) {
        this._subResource = subResource;
    }

    create = async (entryId: string, request: TCreateRequest): Promise<TModel> => {
        const resp = await appApi.post({
            resource: 'entries',
            resourceId: entryId,
            subResource: this._subResource
        }, request);
        return resp.data
    }
    update = async (entryId: string, entryInfoId: string, request: TUpdateRequest): Promise<null> => {
        const resp = await appApi.put({
            resource: 'entries',
            resourceId: entryId,
            subResource: this._subResource,
            subResourceId: entryInfoId
        }, request);
        return resp.data;
    }
    get = async (entryId: string, entryInfoId: string): Promise<TModel> => {
        const resp = await appApi.get({
            resource: 'entries',
            resourceId: entryId,
            subResource: this._subResource,
            subResourceId: entryInfoId
        });
        return resp.data;
    }
    list = async (entryId: string, request: TListRequest): Promise<TModel[]> => {
        const resp = await appApi.list({
            resource: 'entries',
            resourceId: entryId,
            subResource: this._subResource,
        }, request);
        return resp.data;
    }
    delete = async (entryId: string, entryInfoId: string): Promise<null> => {
        const resp = await appApi.delete({
            resource: 'entries',
            resourceId: entryId,
            subResource: this._subResource,
            subResourceId: entryInfoId
        });
        return resp.data;
    }
}

// export const apiEntryPhone = new ApiEntryInfoGeneric<InfoPhone, EntryPhoneFormRequest, EntryPhoneFormRequest, ApiListRequest>('phones');
// export const apiEntryDate = new ApiEntryInfoGeneric<InfoDate, EntryDateFormRequest, EntryDateFormRequest, ApiListRequest>('dates');
// export const apiEntryNote = new ApiEntryInfoGeneric<InfoNote, EntryNoteFormRequest, EntryNoteFormRequest, ApiListRequest>('notes');
// export const apiEntryUrl = new ApiEntryInfoGeneric<InfoUrl, EntryUrlFormRequest, EntryUrlFormRequest, ApiListRequest>('urls');
// export const apiEntryEmail = new ApiEntryInfoGeneric<InfoEmail, EntryEmailFormRequest, EntryEmailFormRequest, ApiListRequest>('emails');

// export const apiEntryInfo = {
//     Phone: apiEntryPhone,
//     Date: apiEntryDate,
//     Note: apiEntryNote,
//     Url: apiEntryUrl,
//     Email: apiEntryEmail,
// }

export const apiEntryInfo = new ApiEntryInfoGeneric<EntryInfo, EntryInfoFormRequest, EntryInfoFormRequest, EntryInfoListRequest>('entry-infos');