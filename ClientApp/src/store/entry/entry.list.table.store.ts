﻿import {defineStore} from 'pinia';
import {Entry, EntryListRequest} from "../../api/api_types";
import {apiEntry} from "../../api/rerources/api_entry";

declare type EntryListStoreState = {
    isLoading: boolean,
    isEnd: boolean,
    listRequest: EntryListRequest,
    entries: Entry[],
    previewEntry: Entry | null,
}
export const useEntryListStore = defineStore('EntryListStore', {
    state: (): EntryListStoreState => {
        return {
            previewEntry: null,
            entries: [],
            isLoading: false,
            isEnd: false,
            listRequest: {
                page: 1,
                perPage: 50,
                search: '',
                orderBy: '',
                orderByDesc: false,
                entryType: null,
                isDeleted: false,
            }
        }
    },
    actions: {
        resetListRequest()
        {
            this.listRequest.page = 1;
            this.isEnd = false;
        },
        async getEntriesAsync() {
            if (this.isLoading || this.isEnd) {
                return;
            }

            this.isLoading = true;

            try {
                const items = await apiEntry.list(this.listRequest);
                if (this.listRequest.page > 1) {
                    this.entries = this.entries.concat(items);   
                } else {
                    this.entries = items;    
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