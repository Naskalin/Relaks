import {defineStore} from 'pinia';
import {Entry} from "../../api/api_types";
import {apiEntry} from "../../api/rerources/api_entry";

interface EntryProfileStoreState {
    entry: null | Entry,
}

export const useEntryProfileStore = defineStore('EntryProfileStore', {
    state: (): EntryProfileStoreState => {
        return {
            entry: null,
        }
    },
    actions: {
        async getEntry(entryId: string) {
            this.entry = await apiEntry.get(entryId);
        }
    }
})