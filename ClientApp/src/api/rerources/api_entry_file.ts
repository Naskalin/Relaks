import {ApiListRequest, appApi} from "../index";
import {EntryFileCreateResponse, EntryFile, FileModelUpdateRequest, EntryFileMeta, FileListRequest} from "../api_types";

const subResource = 'files';
export const apiEntryFile = {
    create: async (entryId: string, formData: FormData): Promise<EntryFileCreateResponse> => {
        const resp = await appApi.post(
            ['entries', entryId, subResource],
            formData,
            {
                // responseType: 'blob',
                headers: {'Content-Type': 'multipart/form-data'}
            }
        );

        return resp.data;
    },
    update: async (entryId: string, fileId: string, req: FileModelUpdateRequest): Promise<null> => {
        const resp = await appApi.put(['entries', entryId, subResource, fileId], req);

        return resp.data;
    },
    delete: async (entryId: string, entryFileId: string): Promise<null> => {
        const resp = await appApi.delete(['entries', entryId, subResource, entryFileId]);

        return resp.data;
    },
    get: async (entryId: string, fileId: string): Promise<EntryFile> => {
        const resp = await appApi.get(['entries', entryId, subResource, fileId]);
        return resp.data;
    },
    getMeta: async(entryId: string): Promise<EntryFileMeta> => {
        const resp = await appApi.get(['entries', entryId, subResource, 'meta']);
        return resp.data;
    },
    list: async (entryId: string, listRequest: FileListRequest): Promise<EntryFile[]> => {
        const resp = await appApi.list(['entries', entryId, subResource], listRequest);

        return resp.data;
    }
}