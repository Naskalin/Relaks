﻿import {ActualPart, EntryText, TextType} from "../resource_types";

import {ApiListRequest, appApi} from "../index";

export declare type CreateEntryTextRequest = {
    about: string
    val: string
    textType: TextType
} & ActualPart

export declare type UpdateEntryTextRequest = {} & CreateEntryTextRequest

export declare type ListEntryTextRequest = {
    textType: TextType
} & ApiListRequest

export const apiEntryText = {
    create: async (entryId: string, request: CreateEntryTextRequest): Promise<EntryText> => {
        const resp = await appApi.post({resource: 'entries', resourceId: entryId, subResource: 'texts'}, request);
        return resp.data;
    },
    update: async (entryId: string, entryTextId: string, request: UpdateEntryTextRequest): Promise<null> => {
        const resp = await appApi.put({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'texts',
            subResourceId: entryTextId
        }, request);
        return resp.data;
    },
    get: async (entryId: string, entryTextId: string): Promise<EntryText> => {
        const resp = await appApi.get({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'texts',
            subResourceId: entryTextId
        });
        return resp.data;
    },
    list: async (entryId: string, request: ListEntryTextRequest): Promise<EntryText[]> => {
        const resp = await appApi.list({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'texts',
        }, request);
        return resp.data;
    },
    delete: async (entryId: string, entryTextId: string): Promise<null> => {
        const resp = await appApi.delete({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'texts',
            subResourceId: entryTextId
        });
        return resp.data;
    },
}