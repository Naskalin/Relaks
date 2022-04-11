import {ApiListRequest, appApi} from "../index";
import {EntryFileCreateResponse, EntryFile} from "../api_types";

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
    list: async (entryId: string, listRequest: ApiListRequest): Promise<EntryFile[]> => {
        const resp = await appApi.list({
            resource: 'entries',
            resourceId: entryId,
            subResource: 'files'
        }, listRequest);

        return resp.data;
    }

}