import {defineStore} from 'pinia';
import {apiEntryFile} from "../../api/rerources/api_entry_file";
import {EntryFileMeta} from "../../api/api_types";

declare type EntryFileMetaListStoreState = {
    isLoading: boolean,
    meta: EntryFileMeta | null
}
export const useEntryFileMetaListStore = defineStore('EntryFileMetaListStore', {
    state: (): EntryFileMetaListStoreState => {
        return {
            isLoading: false,
            meta: null
        }
    },
    actions: {
        async getMeta(entryId: string)
        {
            if (this.isLoading) return;
            this.isLoading = true;

            try {
                this.meta = await apiEntryFile.getMeta(entryId);
            } finally {
                this.isLoading = false;
            }
        },
    }
})