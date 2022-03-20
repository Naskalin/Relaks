import {defineStore} from 'pinia';
import {CreateEntryRequest, apiEntry} from "../../api/rerources/api_entry";
import {Entry} from "../../api/resource_types";

declare type EntryCreateStoreState = {
    model: CreateEntryRequest,
    isCreating: boolean,
    isShowModal: boolean,
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
                actualStartAt: '',
                actualEndAt: null,
                actualEndAtReason: '',
                actualStartAtReason: ''
            },
            isCreating: false,
            isShowModal: false,
        }
    },
    actions: {
        async createEntry(): Promise<Entry> {
            if (this.isCreating) {
                return Promise.reject();
            }

            this.isCreating = true;

            try {
                return await apiEntry.create(this.model);
            } finally {
                this.isCreating = false;
            }
        }
    }
})