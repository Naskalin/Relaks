import {appApi} from "../index";

export declare type StructureConnectionDirection = 'Normal' | 'Reverse' | 'Bidirectional';
export declare type StructureConnection = {
    title: string,
    description: string,
    structureFirstId: string,
    direction: StructureConnectionDirection
    structureSecondId: string,
    options: any,
    createdAt: string,
    updatedAt: string,
    id: string
}

export declare type StructureConnectionListRequest = {
    entryId: string
}

export const apiStructureConnection = {
    list: async (listRequest: StructureConnectionListRequest): Promise<StructureConnection[]> => {
        const resp = await appApi.list(['structure-connections'], listRequest);
        return resp.data;
    }
}