import {defineStore} from 'pinia';
import {EntryInfo, EntryInfoType, EntryInfoFormRequest} from "../../api/api_types";
import {apiEntryInfo} from "../../api/rerources/api_entry_info";

declare type EntryInfoCreateStoreState = {
    request: EntryInfoFormRequest,
    isLoading: boolean,
}

export const useEntryInfoCreateStore = defineStore('EntryInfoCreateStore', {
    state: (): EntryInfoCreateStoreState => {
        return {
            isLoading: false,
            request: {
                title: '',
                isFavorite: false,
                deletedReason: '',
                deletedAt: null,
                type: "EMAIL",
                info: {email: ''},
            }
        }
    },
    actions: {
        resetRequestInfo(type: EntryInfoType)
        {
            switch (type)
            {
                case "EMAIL":
                    this.$state.request.info = {email: ''};
                    break;
                case "PHONE":
                    this.$state.request.info = {number: '', region: ''};
                    break;
                case "DATE":
                    this.$state.request.info = {date: ''};
                    break;
                case "NOTE":
                    this.$state.request.info = {note: ''};
                    break;
                case "URL":
                    this.$state.request.info = {url: ''};
                    break;
                default:
                    throw new Error(`Type ${type} is not supported. resetRequestInfo`);
            }
        },
        async create(entryId: string): Promise<EntryInfo> {
            if (this.isLoading) {
                return Promise.reject();
            }
            this.isLoading = true;

            try {
                return await apiEntryInfo.create(entryId, this.request);
            } finally {
                this.isLoading = false;
            }
        },
    }
})

