import {defineStore} from 'pinia';
import { EntryInfo, EntryInfoFormRequest} from "../../api/api_types";
import {apiEntryInfo} from "../../api/rerources/api_entry_info";

declare type EntryInfoEditStoreState = {
    model: EntryInfoFormRequest | null,
    isLoading: boolean
}
export const useEntryInfoEditStore = defineStore('EntryInfoEditStore', {
    state: (): EntryInfoEditStoreState => {
        return {
            isLoading: false,
            model: null
        }
    },
    actions: {
        setModel(eInfo: EntryInfo): void {
            this.$state.model = eInfo;
        },
        async update(entryId: string, entryInfoId: string): Promise<null> {
            if (this.isLoading || !this.model) {
                return Promise.reject();
            }
            this.isLoading = true;

            try {
                return await apiEntryInfo.update(entryId, entryInfoId, this.model);
            } finally {
                this.isLoading = false;
            }
        }
    }
})