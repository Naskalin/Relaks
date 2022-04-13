import {defineStore} from 'pinia';
import {apiEntryFile} from "../../api/rerources/api_entry_file";
import {EntryFile, FileModelUpdateRequest} from "../../api/api_types";

declare type EntryFileEditStoreState = {
    isLoading: boolean
    file: null | EntryFile
}
export const useEntryFileEditStore = defineStore('EntryFileEditStore', {
    state: (): EntryFileEditStoreState => {
        return {
            file: null,
            isLoading: false
        }
    },
    actions: {
        async update(req: FileModelUpdateRequest) {
            if (this.isLoading || !this.file) return;
            this.isLoading = true;

            try {
                return await apiEntryFile.update(this.file.entryId, this.file.id, req);
            } finally {
                this.isLoading = false;
            }
        }
    }
})