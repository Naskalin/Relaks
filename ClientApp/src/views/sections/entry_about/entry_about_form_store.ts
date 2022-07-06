import {defineStore} from 'pinia';
import {EntryInfoFormRequest} from "../../../api/api_types";

declare type EntryAboutFormStoreState = {
    model: EntryInfoFormRequest,
    isLoading: boolean,
}
export const useEntryAboutFormStore = defineStore('EntryAboutFormStore', {
    state: (): EntryAboutFormStoreState => {
        return {
            isLoading: false,
            model: {
                title: '',
                isFavorite: false,
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