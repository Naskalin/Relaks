import {defineStore} from 'pinia';
import {EntryText} from "../../api/api_types";
import {apiEntryText} from "../../api/rerources/api_entry_text";
import {ApiListRequest} from "../../api";

declare type EntryTextNoteListStoreState = {
    notes: EntryText[],
    isLoading: boolean,
    listRequest: ApiListRequest
}
export const useEntryTextNoteListStore = defineStore('EntryTextNoteListStore', {
    state: (): EntryTextNoteListStoreState => {
        return {
            notes: [],
            isLoading: false,
            listRequest: {
                page: 1,
                perPage: 50,
                search: '',
                orderBy: '',
                orderByDesc: '',
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
                this.notes = await apiEntryText.list(entryId, {
                    textType: 'Note',
                    ...this.listRequest,
                });
            } finally {
                this.isLoading = false;
            }
        }
    }
})