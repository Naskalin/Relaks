import {Entry, EntryCreateRequest, EntryUpdateRequest, EntryListRequest} from "../api_types";
import {appApi} from "../index";

export const apiEntry = {
    create: async (request: EntryCreateRequest): Promise<Entry> => {
        const reqCopy = Object.assign({}, request);
        const resp = await appApi.post(['entries'], reqCopy);
        return resp.data;
    },
    update: async (entryId: string, request: EntryUpdateRequest): Promise<null> => {
        const reqCopy = Object.assign({}, request);
        const resp = await appApi.put(['entries', entryId], reqCopy);
        return resp.data;
    },
    get: async (entryId: string): Promise<Entry> => {
        const resp = await appApi.get(['entries', entryId]);
        return resp.data;
    },
    list: async (request: EntryListRequest): Promise<Entry[]> => {
        const resp = await appApi.list(['entries'], request);
        return resp.data;
    },
    delete: async (entryId: string): Promise<null> => {
        const resp = await appApi.delete(['entries', entryId]);
        return resp.data;
    },
}