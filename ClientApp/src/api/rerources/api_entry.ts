import {ActualPart, Entry, EntryType} from "../resource_types";
import {ApiListRequest, appApi} from "../index";
import {dateToISONormalizer} from "../../utils/utils";

export declare type CreateEntryRequest = {
    name: string
    entryType: EntryType
    description: string
    reputation: number
    startAt: string | null
    endAt: string | null
} & ActualPart

export declare type UpdateEntryRequest = {
    entryId: string
} & CreateEntryRequest

function requestDateNormalizer(request: CreateEntryRequest | UpdateEntryRequest)
{
    request.startAt = dateToISONormalizer(request.startAt);
    request.endAt = dateToISONormalizer(request.endAt);
    request.actualStartAt = dateToISONormalizer(request.actualStartAt) as string;
    request.actualEndAt = dateToISONormalizer(request.actualEndAt);
}

export const apiEntry = {
    create: async (request: CreateEntryRequest) : Promise<Entry> => {
        const reqCopy = Object.assign({}, request);
        requestDateNormalizer(reqCopy);
        const resp = await appApi.post({resource: 'entries'}, reqCopy);
        return resp.data;
    },
    update: async (entryId: string, request: UpdateEntryRequest): Promise<null> => {
        const reqCopy = Object.assign({}, request);
        requestDateNormalizer(reqCopy);
        const resp = await appApi.put({resource: 'entries', resourceId: entryId}, reqCopy);
        return resp.data;
    },
    get: async (entryId: string): Promise<Entry> => {
        const resp = await appApi.get({resource: 'entries', resourceId: entryId});
        return resp.data;
    },
    list: async (request: ApiListRequest): Promise<Entry[]> => {
        const resp = await appApi.list({resource: 'entries'}, request);
        return resp.data;
    },
    delete: async (entryId: string) : Promise<null> => {
        const resp = await appApi.delete({resource: 'entries', resourceId: entryId});
        return resp.data;
    },
}