import {ActualTypes, Entry, EntryType} from "../resource_types";
import {ApiListRequest, appApi} from "../index";
import {dateHelper} from "../../utils/date_helper";

export declare type CreateEntryRequest = {
    name: string
    entryType: EntryType
    description: string
    reputation: number
    startAt: string | null
    endAt: string | null
} & ActualTypes

export declare type UpdateEntryRequest = {
    entryId: string
} & CreateEntryRequest

function datesToISO(request: CreateEntryRequest | UpdateEntryRequest) {
    request.startAt = dateHelper.toISO(request.startAt);
    request.endAt = dateHelper.toISO(request.endAt);
    request.actualStartAt = dateHelper.toISO(request.actualStartAt) as string;
    request.actualEndAt = dateHelper.toISO(request.actualEndAt);
}

export const apiEntry = {
    create: async (request: CreateEntryRequest): Promise<Entry> => {
        const reqCopy = Object.assign({}, request);
        datesToISO(reqCopy);
        const resp = await appApi.post({resource: 'entries'}, reqCopy);
        return resp.data;
    },
    update: async (entryId: string, request: UpdateEntryRequest): Promise<null> => {
        const reqCopy = Object.assign({}, request);
        datesToISO(reqCopy);
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
    delete: async (entryId: string): Promise<null> => {
        const resp = await appApi.delete({resource: 'entries', resourceId: entryId});
        return resp.data;
    },
}