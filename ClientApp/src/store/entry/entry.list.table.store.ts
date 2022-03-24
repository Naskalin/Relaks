import {defineStore} from 'pinia';
import {Entry, ListEntryRequest} from "../../api/api_types";
import {apiEntry} from "../../api/rerources/api_entry";

declare type EntryListStoreState = {
    isLoading: boolean,
    isEnd: boolean,
    listRequest: ListEntryRequest,
    entries: Entry[]
}
export const useEntryListStore = defineStore('EntryListStore', {
    state: (): EntryListStoreState => {
        return {
            entries: [],
            isLoading: false,
            isEnd: false,
            listRequest: {
                page: 1,
                perPage: 50,
                search: '',
                orderBy: '',
                orderByDesc: '',
                entryType: 'Person'
            }
        }
    },
    actions: {
        async getEntries() {
            if (this.isLoading || this.isEnd) {
                return;
            }

            this.isLoading = true;

            try {
                const items = await apiEntry.list(this.listRequest);
                this.entries = this.entries.concat(items);
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