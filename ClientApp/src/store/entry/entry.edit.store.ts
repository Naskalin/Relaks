import {defineStore} from 'pinia';
import {apiEntry} from "../../api/rerources/api_entry";
import {EntryUpdateRequest} from "../../api/api_types";

declare type EntryEditStoreState = {
    model: null | EntryUpdateRequest,
    isLoading: boolean,
}
export const useEntryEditStore = defineStore('EntryEditStore', {
    state: (): EntryEditStoreState => {
        return {
            model: null,
            isLoading: false,
        }
    },
    actions: {
        async updateEntry(entryId: string): Promise<null> {
            if (this.isLoading || !this.model) {
                return Promise.reject();
            }

            this.isLoading = true;

            try {
                return await apiEntry.update(entryId, this.model);
            } finally {
                this.isLoading = false;
            }
        }
    }
})