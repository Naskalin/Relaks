import {defineStore} from 'pinia';
import {Entry} from "../../../api/api_types";

declare type EntryItemStoreState = {
    previewEntry: Entry | null
}
export const useEntryItemStore = defineStore('EntryItemStore', {
    state: (): EntryItemStoreState => {
        return {
            previewEntry: null
        }
    },
    actions: {}
})