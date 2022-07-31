import {appApi} from "../index";
import {SoftDeletableType, TimestampTypes} from "../api_types";
export declare type StructureTree = {
    id: string
    data: Structure,
    children: StructureTree[],
    isRoot: boolean,
    isLeaf: boolean,
    level: number
}

export declare type Structure = {
    id: string
    title: string,
    description: string,
    startAt: string,
    parentId: null | string,
    entryId: string,
} & SoftDeletableType & TimestampTypes

export declare type StructureListRequest = {
    isDeleted?: boolean | null
    date?: string | null
    isTree?: boolean
}
export declare type StructureFormRequest = {
    title: string
    description: string
    startAt: null | string
    parentId: null | string
} & SoftDeletableType

export const apiStructure = {
    create: async (entryId: string, request: StructureFormRequest): Promise<Structure> => {
        const resp = await appApi.post(['entries', entryId, 'structures'], request);
        return resp.data;
    },
    update: async (entryId: string, structureId: string, request: StructureFormRequest): Promise<null> => {
        const resp = await appApi.put(['entries', entryId, 'structures', structureId], request);
        return resp.data;
    },
    delete: async (entryId: string, structureId: string): Promise<null> => {
          const resp = await appApi.delete(['entries', entryId, 'structures', structureId]);
          return resp.data;
    },
    tree: async (entryId: string, listRequest: StructureListRequest): Promise<StructureTree[]> => {
        const resp = await appApi.list(['entries', entryId, 'structures'], {
            ...listRequest,
            isTree: true,
        });
        return resp.data.children;
    },
    list: async (entryId: string, listRequest: StructureListRequest): Promise<Structure[]> => {
        const resp = await appApi.list(['entries', entryId, 'structures'], {
            ...listRequest,
            isTree: false,
        });
        return resp.data;
    }
}