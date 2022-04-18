import {FileDownloadRequest} from "../api_types";
import {appApi} from "../index";

export const apiFiles = {
    download: async (req: FileDownloadRequest): Promise<string> => {
        const resp = await appApi.get({
            resource: 'files',
            resourceId: req.fileId,
            subResource: 'download',
        }, {
            params: {
                imageFilter: req.imageFilter,
            },
            responseType: 'blob'
        });
        // return URL.createObjectURL(resp.data);
        return URL.createObjectURL(resp.data);
    },
}