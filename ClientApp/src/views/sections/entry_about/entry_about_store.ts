import {defineStore} from 'pinia';
import {EntryInfo, EntryInfoListRequest} from "../../../api/api_types";
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {filterByType} from "../../../utils/entry_info_helper";

declare type EntryAboutStoreState = {
    list: EntryInfo[]
    listRequest: EntryInfoListRequest
    isLoading: boolean
}
export const useEntryAboutStore = defineStore('EntryAboutStore', {
    state: (): EntryAboutStoreState => {
        return {
            list: [],
            isLoading: false,
            listRequest: {
                type: ['CUSTOM'],
                isDeleted: null,
                search: '',
            }
        }
    },
    getters: {
        customs: (state) => filterByType(state.list, 'CUSTOM', state.listRequest.isDeleted)
    },
    actions: {
        async getItems(entryId: string) {
            if (this.isLoading) return;
            this.isLoading = true;

            try {
                this.list = await apiEntryInfo.list(entryId, this.listRequest);
            } finally {
                this.isLoading = false;
            }
        }
    }
})