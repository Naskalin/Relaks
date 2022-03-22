import {defineStore} from 'pinia';
import {apiEntry, UpdateEntryRequest} from "../../api/rerources/api_entry";

declare type EntryEditModalState = {
    model: null | UpdateEntryRequest,
    isShowModal: boolean,
    isEditing: boolean,
}
export const useEntryEditModal = defineStore('EntryEditModal', {
    state: (): EntryEditModalState => {
        return {
            model: null,
            isShowModal: false,
            isEditing: false,
        }
    },
    actions: {
        async updateEntry(): Promise<null> {
            if (this.isEditing) {
                return Promise.reject();
            }

            this.isEditing = true;

            try {
                return await apiEntry.update(this.model);
            } finally {
                this.isEditing = false;
            }
        }
    }
})