import {defineStore} from 'pinia';
import {EntryInfoFormRequest} from "../../../../api/api_types";

declare type EntryInfoCustomFormStoreState = {
    model: EntryInfoFormRequest,
    isLoading: boolean,
}
export const useEntryInfoCustomFormStore = defineStore('EntryInfoCustomFormStore', {
    state: (): EntryInfoCustomFormStoreState => {
        return {
            isLoading: false,
            model: {
                title: '',
                deletedAt: null,
                deletedReason: '',
                type: 'CUSTOM',
                info: {
                    groups: []
                }
            }
        }
    },
})