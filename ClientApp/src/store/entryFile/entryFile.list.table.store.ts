import {defineStore} from 'pinia';
import {EntryFile, PaginateListRequest} from "../../api/api_types";
import {apiEntryFile} from "../../api/rerources/api_entry_file";

declare type EntryFileListTableStoreState = {
    files: EntryFile[],
    isLoading: boolean,
    isEnd: boolean,
    listRequest: PaginateListRequest
}
export const useEntryFileListTableStore = defineStore('EntryFileListTableStore', {
    state: (): EntryFileListTableStoreState => {
        return {
            files: [],
            isLoading: false,
            isEnd: false,
            listRequest: {
                page: 1,
                perPage: 50,
                search: '',
                orderBy: '',
                orderByDesc: '',
                isDeleted: false,
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