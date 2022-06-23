import {appApi} from "../index";
import {Entry, SoftDeletableType, TimestampTypes} from "../api_types";
// export declare type StructureTree = {
//     data: null,
//     children: TreeItem[],
//     isRoot: boolean,
//     isLeaf: boolean,
//     level: number
// }
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
    date?: string
    isTree: boolean
}

export const apiStructure = {
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

//     [FromRoute]
// public Guid EntryId { get; set; }
//
// [FromQuery]
// public bool? IsDeleted { get; set; }
//
//     [FromQuery]
// public DateTime? Date { get; set; }