import {defineStore} from 'pinia';
import {UpdateEntryTextRequest, EntryText} from "../../api/api_types";
import {apiEntryText} from "../../api/rerources/api_entry_text";

declare type EntryTextEditStoreState = {
    model: null | UpdateEntryTextRequest,
    isLoading: boolean
}
export const useEntryTextEditStore = defineStore('EntryTextEditStore', {
    state: (): EntryTextEditStoreState => {
        return {
            isLoading: false,
            model: null
        }
    },
    actions: {
        async update(entryId: string, entryTextId: string): Promise<null> {
            if (this.isLoading || !this.model) {
                return Promise.reject();
            }
            this.isLoading = true;
            
            try {
                return await apiEntryText.update(entryId, entryTextId, this.model);
            } finally {
                this.isLoading = false;
            }
        }
    }
})