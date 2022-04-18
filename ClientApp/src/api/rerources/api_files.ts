import {FileDownloadRequest} from "../api_types";
import {appApi} from "../index";
import {AxiosResponse} from "axios";

export const apiFiles = {
    download: async (req: FileDownloadRequest): Promise<AxiosResponse> => {
        return await appApi.get({
            resource: 'files',
            resourceId: req.fileId,
            subResource: 'download',
        }, {
            params: {
                imageFilter: req.imageFilter,
            },
            responseType: 'blob'
        });
    },
    explorer: async (fileId: string): Promise<null> => {
        const resp = await appApi.get({
            resource: 'files',
            resourceId: fileId,
            subResource: 'explorer',
        });
        
        return resp.data;
    },
}