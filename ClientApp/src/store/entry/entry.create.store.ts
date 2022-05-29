import {defineStore} from 'pinia';
import {apiEntry} from "../../api/rerources/api_entry";
import {Entry, EntryCreateRequest} from "../../api/api_types";

declare type EntryCreateStoreState = {
    model: EntryCreateRequest,
    isLoading: boolean,
}

export const useEntryCreateStore = defineStore('entryCreateStore', {
    state: (): EntryCreateStoreState => {
        return {
            model: {
                name: '',
                reputation: 5,
                entryType: 'Person',
                description: '',
                startAt: null,
                endAt: null,
                deletedReason: '',
                deletedAt: null,
                avatar: null,
            },
            isLoading: false,
        }
    },
    actions: {
        async createEntry(): Promise<Entry> {
            if (this.isLoading) {
                return Promise.reject();
            }

            this.isLoading = true;

            try {
                return await apiEntry.create(this.model);
            } finally {
                this.isLoading = false;
            }
        }
    }
})