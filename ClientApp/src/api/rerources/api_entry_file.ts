import {appApi} from "../index";
import {EntryFileCreateRequest} from "../api_types";

const subResource = 'files';
export const apiEntryFile = {
    create: async (entryId: string, request: EntryFileCreateRequest) => {
        const resp = await appApi.post({
            resource: 'entries',
            resourceId: entryId,
            subResource: subResource
        }, request, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });

        return resp.data;
    }
}