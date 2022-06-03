import {defineStore} from 'pinia';
import {EntryInfo, EntryInfoType, EntryInfoObjectType, EntryInfoFormRequest} from "../../api/api_types";
import {apiEntryInfo} from "../../api/rerources/api_entry_info";
import {types} from "sass";

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
                deletedReason: '',
                deletedAt: null,
                type: "Email",
                info: {email: ''},
            }
        }
    },
    actions: {
        resetRequestInfo(type: EntryInfoType)
        {
            switch (type)
            {
                case "Email":
                    this.$state.request.info = {email: ''};
                    break;
                case "Phone":
                    this.$state.request.info = {number: '', region: ''};
                    break;
                case "Date":
                    this.$state.request.info = {date: ''};
                    break;
                case "Note":
                    this.$state.request.info = {note: ''};
                    break;
                case "Url":
                    this.$state.request.info = {url: ''};
                    break;
                // case "Passport":
                //     break;
                // case "CompanyDetails":
                //     break;
                // case "Custom":
                //     break;
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

