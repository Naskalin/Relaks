import {defineStore} from 'pinia';
import {apiEntryInfo} from "../../api/rerources/api_entry_info";
import {EntryInfoType, EntryInfo} from "../../api/api_types";

declare type EntryInfoListStoreState = {
    isLoading: boolean,
    list: EntryInfo[],
}

function filterByType(isShowDeleted: boolean | null, type: EntryInfoType, list: EntryInfo[]) {
    return list.filter(eInfo => {
        if (eInfo.type != type) return false;
        if (isShowDeleted === true) return eInfo.deletedAt;
        if (isShowDeleted === false) return !eInfo.deletedAt;
        return eInfo;
    });
}

export const useEntryInfoListStore = defineStore('EntryInfoListStore', {
    state: (): EntryInfoListStoreState => {
        return {
            isLoading: false,
            list: [],
        }
    },
    getters: {
        notes: (state) => (isShowDeleted: boolean | null) => filterByType(isShowDeleted, 'NOTE', state.list),
        emails: (state) => (isShowDeleted: boolean | null) => filterByType(isShowDeleted, 'EMAIL', state.list),
        urls: (state) => (isShowDeleted: boolean | null) => filterByType(isShowDeleted, 'URL', state.list),
        phones: (state) => (isShowDeleted: boolean | null) => filterByType(isShowDeleted, 'PHONE', state.list),
        dates: (state) => (isShowDeleted: boolean | null) => filterByType(isShowDeleted, 'DATE', state.list),
        passports: (state) => (isShowDeleted: boolean | null) => filterByType(isShowDeleted, 'PASSPORT', state.list),
        companyDetails: (state) => (isShowDeleted: boolean | null) => filterByType(isShowDeleted, 'COMPANY_DETAILS', state.list),
        customs: (state) => (isShowDeleted: boolean | null) => filterByType(isShowDeleted, 'CUSTOM', state.list),
    },
    actions: {
        async getAll(entryId: string) {
            if (this.isLoading) {
                return;
            }
            this.isLoading = true;

            try {
                this.list = await apiEntryInfo.list(entryId, {});
            } finally {
                this.isLoading = false;
            }
        },
    }
})