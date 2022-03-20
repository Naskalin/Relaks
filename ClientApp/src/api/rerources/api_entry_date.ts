import {ActualPart, EntryDate} from "../resource_types";

import {ApiListRequest, appApi} from "../index";

export declare type CreateEntryDateRequest = {
    about: string
    val: string
} & ActualPart

export declare type UpdateEntryDateRequest = {} & CreateEntryDateRequest

export declare type ListEntryDateRequest = {
} & ApiListRequest

export const apiEntryDate = {
    create: async (entryId: string, request: CreateEntryDateRequest): Promise<EntryDate> => {
        const resp = await appApi.post({resource: 'entries', resourceId: entryId, subResource: 'dates'}, request);
        return resp.data;
    },
    update: async (entryId: string, EntryDateId: string, request: UpdateEntryDateRequest): Promise<null> => {
        const resp = await appApi.put({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'dates',
            subResourceId: EntryDateId
        }, request);
        return resp.data;
    },
    get: async (entryId: string, EntryDateId: string): Promise<EntryDate> => {
        const resp = await appApi.get({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'dates',
            subResourceId: EntryDateId
        });
        return resp.data;
    },
    list: async (entryId: string, request: ListEntryDateRequest): Promise<EntryDate[]> => {
        const resp = await appApi.list({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'dates',
        }, request);
        return resp.data;
    },
    delete: async (entryId: string, EntryDateId: string): Promise<null> => {
        const resp = await appApi.delete({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'dates',
            subResourceId: EntryDateId
        });
        return resp.data;
    },
}