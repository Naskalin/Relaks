import {defineStore} from 'pinia';
import {apiEntryInfo} from "../../api/rerources/api_entry_info";
import {
    EntryEmail,
    EntryDate,
    EntryPhone,
    EntryUrl, 
} from "../../api/api_types";

// type Dict = {[key in EntryInfoType]: string};
declare type EntryCardContactsStoreState = {
    isShowDeleted: boolean | null,
    Email: {
        list: EntryEmail[]
        isLoading: boolean,
    },
    Date: {
        list: EntryDate[]
        isLoading: boolean
    },
    Phone: {
        list: EntryPhone[]
        isLoading: boolean
    },
    Url: {
        list: EntryUrl[]
        isLoading: boolean
    }
}
export const useEntryCardContactsStore = defineStore('EntryCardContactsStore', {
    state: (): EntryCardContactsStoreState => {
        return {
            isShowDeleted: false,
            Email: {
                list: [],
                isLoading: false,
            },
            Date: {
                list: [],
                isLoading: false,
            },
            Phone: {
                list: [],
                isLoading: false,
            },
            Url: {
                list: [],
                isLoading: false,
            },
        }
    },
    getters: {
        dates: (state) => state.Date.list.filter(eInfo => {
            if (state.isShowDeleted === true) return eInfo.deletedAt;
            if (state.isShowDeleted === false) return !eInfo.deletedAt;
            return eInfo;
        }),
        phones: (state) => state.Phone.list.filter(eInfo => {
            if (state.isShowDeleted === true) return eInfo.deletedAt;
            if (state.isShowDeleted === false) return !eInfo.deletedAt;
            return eInfo;
        }),
        emails: (state) => state.Email.list.filter(eInfo => {
            if (state.isShowDeleted === true) return eInfo.deletedAt;
            if (state.isShowDeleted === false) return !eInfo.deletedAt;
            return eInfo;
        }),
        urls: (state) => state.Url.list.filter(eInfo => {
            if (state.isShowDeleted === true) return eInfo.deletedAt;
            if (state.isShowDeleted === false) return !eInfo.deletedAt;
            return eInfo;
        }),
    },
    actions: {
        async getAllContacts(entryId: string) {
            await this.getPhones(entryId);
            await this.getEmails(entryId);
            await this.getDates(entryId);
            await this.getUrls(entryId);
        },
        async getBase(entryId: string, key: 'Phone' | 'Email' | 'Url' | 'Date') {
            if (this[key].isLoading) {
                return;
            }
            this[key].isLoading = true;

            try {
                this[key].list = await apiEntryInfo[key].list(entryId, {});
            } finally {
                this[key].isLoading = false;
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
        async getDates(entryId: string) {
            await this.getBase(entryId, 'Date');
        }
    }
})