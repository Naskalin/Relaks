import {defineStore} from "pinia";
import {ApiGetAllResponse, ApiGetRequest, ApiGetResponse} from "../../api/get";
import {jsonApi} from "../../api";

declare type entryProfileNotesState = {
    notes: null | ApiGetAllResponse,
    notesIsLoading: boolean,
    note: null | ApiGetResponse
}

export const useEntryProfileNotesStore = defineStore('entryProfileNotes', {
    state: () : entryProfileNotesState => {
        return {
            notes: null,
            notesIsLoading: false,
            note: null,
        }
    },
    actions: {
        async getNotes() {
            if (this.notesIsLoading) {
                return;
            }
            this.notesIsLoading = true;

            try {

            } catch (e) {
                // log with axios interceptors
            } finally {
                this.notesIsLoading = false;
            }
        },
        async getNote(noteId: string, apiRequest?: ApiGetRequest) {
            const resp = await jsonApi.get({resource: 'notes', resourceId: noteId}, apiRequest);
            this.note = resp as ApiGetResponse;
        }
    }
})