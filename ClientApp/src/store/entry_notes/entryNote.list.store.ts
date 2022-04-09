import {defineStore} from 'pinia';
import {EntryNote} from "../../api/api_types";
import {apiEntryNote} from "../../api/rerources/api_entry_info";
import {ApiListRequest} from "../../api";

declare type EntryNoteListStoreState = {
    notes: EntryNote[],
    isLoading: boolean,
    listRequest: ApiListRequest
}
export const useEntryNoteListStore = defineStore('EntryNoteListStore', {
    state: (): EntryNoteListStoreState => {
        return {
            notes: [],
            isLoading: false,
            listRequest: {
                page: 1,
                perPage: 50,
                search: '',
                orderBy: '',
                orderByDesc: '',
                isDeleted: false
            }
        }
    },
    actions: {
        async getNotes(entryId: string) {
            if (this.isLoading) {
                return;
            }
            this.isLoading = true;

            try {
                this.notes = await apiEntryNote.list(entryId, this.listRequest);
            } finally {
                this.isLoading = false;
            }
        }
    }
})