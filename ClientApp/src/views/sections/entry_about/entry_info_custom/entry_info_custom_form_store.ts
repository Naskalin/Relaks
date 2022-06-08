import {defineStore} from 'pinia';
import {EntryInfoFormRequest} from "../../../../api/api_types";

declare type EntryInfoCustomFormStoreState = {
    model: EntryInfoFormRequest
}
export const useEntryInfoCustomFormStore = defineStore('EntryInfoCustomFormStore', {
    state: (): EntryInfoCustomFormStoreState => {
        return {
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
    actions: {
        
    }
})