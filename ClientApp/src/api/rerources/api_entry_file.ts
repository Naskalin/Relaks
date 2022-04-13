import {ApiListRequest, appApi} from "../index";
import {EntryFileCreateResponse, EntryFile, FileModelUpdateRequest} from "../api_types";

const subResource = 'files';
export const apiEntryFile = {
    create: async (entryId: string, formData: FormData): Promise<EntryFileCreateResponse> => {
        const resp = await appApi.post(
            {resource: 'entries', resourceId: entryId, subResource: subResource},
            formData,
            {headers: {'Content-Type': 'multipart/form-data'}}
        );

        return resp.data;
    },
    update: async (entryId: string, fileId: string, req: FileModelUpdateRequest): Promise<null> => {
        const resp = await appApi.put({
            resource: 'entries',
            resourceId: entryId,
            subResource: subResource,
            subResourceId: fileId
        }, req);

        return resp.data;
    },
    delete: async (entryId: string, entryFileId: string): Promise<null> => {
        const resp = await appApi.delete({
            resource: 'entries',
            resourceId: entryId,
            subResource: subResource,
            subResourceId: entryFileId
        });

        return resp.data;
    },
    get: async (entryId: string, fileId: string): Promise<EntryFile> => {
        const resp = await appApi.get({
            resource: 'entries',
            resourceId: entryId,
            subResource: subResource,
            subResourceId: fileId
        });

        return resp.data;
    },
    list: async (entryId: string, listRequest: ApiListRequest): Promise<EntryFile[]> => {
        const resp = await appApi.list({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'files'
        }, listRequest);

        return resp.data;
    }

}