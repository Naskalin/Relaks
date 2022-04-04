import {
    EntryDate, EntryDateFormRequest,
    EntryEmail, EntryEmailFormRequest,
    EntryNote, EntryNoteFormRequest,
    EntryPhone, EntryPhoneFormRequest,
    EntryUrl, EntryUrlFormRequest
} from "../api_types";
import {ApiListRequest, appApi} from "../index";

class ApiEntryInfoGeneric<TModel, TCreateRequest, TUpdateRequest> {
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
    update = async (entryId: string, entryTextId: string, request: TUpdateRequest): Promise<null> => {
        const resp = await appApi.put({
            resource: 'entries',
            resourceId: entryId,
            subResource: this._subResource,
            subResourceId: entryTextId
        }, request);
        return resp.data;
    }
    get = async (entryId: string, entryTextId: string): Promise<TModel> => {
        const resp = await appApi.get({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'dates',
            subResourceId: entryTextId
        });
        return resp.data;
    }
    list = async (entryId: string, request: ApiListRequest): Promise<TModel[]> => {
        const resp = await appApi.list({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'dates',
        }, request);
        return resp.data;
    }
    delete = async (entryId: string, entryTextId: string): Promise<null> => {
        const resp = await appApi.delete({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'dates',
            subResourceId: entryTextId
        });
        return resp.data;
    }
}

export const apiEntryPhone = new ApiEntryInfoGeneric<EntryPhone, EntryPhoneFormRequest, EntryPhoneFormRequest>('phones');
export const apiEntryDate = new ApiEntryInfoGeneric<EntryDate, EntryDateFormRequest, EntryDateFormRequest>('dates');
export const apiEntryNote = new ApiEntryInfoGeneric<EntryNote, EntryNoteFormRequest, EntryNoteFormRequest>('notes');
export const apiEntryUrl = new ApiEntryInfoGeneric<EntryUrl, EntryUrlFormRequest, EntryUrlFormRequest>('urls');
export const apiEntryEmail = new ApiEntryInfoGeneric<EntryEmail, EntryEmailFormRequest, EntryEmailFormRequest>('urls');