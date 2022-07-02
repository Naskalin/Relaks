import {appApi} from "../index";
import {Entry, EntryType, SoftDeletableType, TimestampTypes} from "../api_types";

export declare type StructureItem = {
    id: string
    description: string
    startAt: string,
    structureId: string
    entryId: string
    entry: Entry
} & SoftDeletableType & TimestampTypes
export declare type StructureItemListRequest = {
    structureId?: null | string
    entryId?: null | string
    entryType?: EntryType | null
    isDeleted?: boolean | null
    date?: string | null
    page: number
    perPage: number
}
export declare type StructureItemFormRequest = {
    structureId: string
    entryId: string
    description: string
    startAt?: string | null
} & SoftDeletableType

export const apiStructureItems = {
    list: async (listRequest: StructureItemListRequest): Promise<StructureItem[]> => {
        const resp = await appApi.list(['structure-items'], listRequest);
        return resp.data;
    },
    create: async (request: StructureItemFormRequest): Promise<StructureItem> => {
        const resp = await appApi.post(['structure-items'], request);
        return resp.data;
    },
    update: async (structureItemId: string, request: StructureItemFormRequest): Promise<null> => {
        const resp = await appApi.put(['structure-items', structureItemId], request);
        return resp.data;
    },
    get: async (structureItemId: string): Promise<StructureItem> => {
        const resp = await appApi.get(['structure-items', structureItemId]);
        return resp.data;
    },
    delete: async (structureItemId: string): Promise<null> => {
        const resp = await appApi.delete(['structure-items', structureItemId]);
        return resp.data;
    },
}