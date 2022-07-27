import {defineStore} from 'pinia';
import {FileListRequest, FileModel} from "../../api/api_types";
import {apiEntryFile} from "../../api/rerources/api_entry_file";

export declare type FileListTableStoreState = {
    files: FileModel[],
    isLoading: boolean,
    isEnd: boolean,
    listRequest: FileListRequest,
}
export const useFileListTableStore = defineStore('FileListTableStore', {
    state: (): FileListTableStoreState => {
        return {
            files: [],
            isLoading: false,
            isEnd: false,
            listRequest: {
                page: 1,
                perPage: 50,
                search: '',
                orderBy: '',
                orderByDesc: false,
                isDeleted: false,
                tags: [],
                category: null
            }
        }
    },
    actions: {
        async getFiles(entryId: string) {
            if (this.isLoading || this.isEnd) return;
            this.isLoading = true;

            try {
                const items = await apiEntryFile.list(entryId, this.listRequest);
                if (this.listRequest.page > 1) {
                    this.files = this.files.concat(items);
                } else {
                    this.files = items;
                }
                this.listRequest.page += 1;
                if (items.length < this.listRequest.perPage) {
                    this.isEnd = true;
                }
            } finally {
                this.isLoading = false;
            }
        }
    }
})