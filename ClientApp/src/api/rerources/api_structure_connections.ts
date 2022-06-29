import {appApi} from "../index";

export declare type StructureConnectionDirection = 'Normal' | 'Reverse' | 'Bidirectional';
export declare type StructureConnection = {
    id: string
    description: string,
    structureFirstId: string,
    direction: StructureConnectionDirection
    structureSecondId: string,
    createdAt: string,
    updatedAt: string,
}
export declare type StructureConnectionFormRequest = {
    description: string
    direction: StructureConnectionDirection
    structureFirstId: string
    structureSecondId: string
}
export declare type StructureConnectionListRequest = {
    entryId: string
}

export const apiStructureConnection = {
    list: async (listRequest: StructureConnectionListRequest): Promise<StructureConnection[]> => {
        const resp = await appApi.list(['structure-connections'], listRequest);
        return resp.data;
    },
    get: async (connectionId: string): Promise<StructureConnection> => {
        const resp = await appApi.get(['structure-connections', connectionId]);
        return resp.data;
    },
    create: async (request: StructureConnectionFormRequest): Promise<StructureConnection> => {
        const resp = await appApi.post(['structure-connections'], request);
        return resp.data;
    },
    update: async (connectionId: string, request: StructureConnectionFormRequest): Promise<null> => {
        const resp = await appApi.put(['structure-connections', connectionId], request);
        return resp.data;
    },
    delete: async (connectionId: string): Promise<null> => {
        const resp = await appApi.delete(['structure-connections', connectionId]);
        return resp.data;
    },
}