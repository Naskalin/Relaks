import {defineStore} from 'pinia';
import {EntryInfoFormRequest} from "../../../../api/api_types";

declare type EntryInfoCustomFormStoreState = {
    status: 'new' | 'edit' | null
    model: EntryInfoFormRequest & {id?: string},
    isLoading: boolean,
}
export const useEntryInfoCustomFormStore = defineStore('EntryInfoCustomFormStore', {
    state: (): EntryInfoCustomFormStoreState => {
        return {
            status: null,
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