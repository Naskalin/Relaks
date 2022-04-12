import {FileDownloadRequest} from "../api_types";
import {appApi} from "../index";

export const apiFiles = {
    entryFile: async (req: FileDownloadRequest): Promise<any> => {
        const resp = await appApi.get({resource: 'entryFiles', resourceId: req.fileId}, {
            params: {
                imageFilter: req.imageFilter,  
            },
            responseType: 'blob'
        });
        return URL.createObjectURL(resp.data);
    },
}