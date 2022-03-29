import {defineStore} from 'pinia';
import {CreateEntryTextRequest, EntryText} from "../../api/api_types";
import {apiEntryText} from "../../api/rerources/api_entry_text";

declare type EntryTextCreateStoreState = {
    model: CreateEntryTextRequest,
    isLoading: boolean
}
export const useEntryTextCreateStore = defineStore('EntryTextCreateStore', {
    state: (): EntryTextCreateStoreState => {
        return {
            isLoading: false,
            model: {
                actualEndAt: null,
                actualStartAt: '',
                actualStartAtReason: '',
                actualEndAtReason: '',
                title: '',
                textType: 'Phone',
                val: '',
            }
        }
    },
    actions: {
        async create(entryId: string): Promise<EntryText> {
            if (this.isLoading) {
                return Promise.reject();
            }
            this.isLoading = true;
            
            try {
                return await apiEntryText.create(entryId, this.model);
            } finally {
                this.isLoading = false;
            }
        }
    }
})