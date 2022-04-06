import {defineStore} from 'pinia';
import {apiEntryInfo} from "../../api/rerources/api_entry_info";
import {EntryEmail, EntryDate, EntryNote, EntryPhone, EntryUrl, EntryInfoType} from "../../api/api_types";
import {ApiListRequest} from "../../api";

declare type EntryInfoListStoreState = {
    Email: {
        list: EntryEmail[]
        isLoading: boolean,
        request: ApiListRequest
    },
    Date: {
        list: EntryDate[]
        isLoading: boolean
        request: ApiListRequest
    },
    Phone: {
        list: EntryPhone[]
        isLoading: boolean
        request: ApiListRequest
    },
    Note: {
        list: EntryNote[]
        isLoading: boolean
        request: ApiListRequest
    },
    Url: {
        list: EntryUrl[]
        isLoading: boolean
        request: ApiListRequest
    }
}
export const useEntryInfoListStore = defineStore('EntryInfoListStore', {
    state: (): EntryInfoListStoreState => {
        return {
            Email: {
                list: [],
                isLoading: false,
                request: {page: 1, perPage: 50, search: ''}
            },
            Date: {
                list: [],
                isLoading: false,
                request: {page: 1, perPage: 50, search: ''}
            },
            Phone: {
                list: [],
                isLoading: false,
                request: {page: 1, perPage: 50, search: ''}
            },
            Note: {
                list: [],
                isLoading: false,
                request: {page: 1, perPage: 50, search: ''}
            },
            Url: {
                list: [],
                isLoading: false,
                request: {page: 1, perPage: 50, search: ''}
            },
        }
    },
    actions: {
        async getAllContacts(entryId: string) {
            await this.getPhones(entryId);
            await this.getEmails(entryId);
            await this.getDates(entryId);
            await this.getUrls(entryId);
        },
        async getBase(entryId: string, entryInfoType: EntryInfoType) {
            if (this[entryInfoType].isLoading) {
                return;
            }
            this[entryInfoType].isLoading = true;

            try {
                this[entryInfoType].list = await apiEntryInfo[entryInfoType].list(entryId, this[entryInfoType].request);
            } finally {
                this[entryInfoType].isLoading = false;
            }
        },
        async getPhones(entryId: string) {
            await this.getBase(entryId, 'Phone');
        },
        async getUrls(entryId: string) {
            await this.getBase(entryId, 'Url');
        },
        async getEmails(entryId: string) {
            await this.getBase(entryId, 'Email');
        },
        async getNotes(entryId: string) {
            await this.getBase(entryId, 'Note');
        },
        async getDates(entryId: string) {
            await this.getBase(entryId, 'Date');
        }
    }
})