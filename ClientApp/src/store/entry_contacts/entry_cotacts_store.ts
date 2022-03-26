import {defineStore} from 'pinia';
import {apiEntryText} from "../../api/rerources/api_entry_text";
import {EntryText} from "../../api/api_types";

declare type EntryContactsStoreState = {
    emails: EntryText[]
    phones: EntryText[]
    urls: EntryText[]
    entryId: string
    isLoadingEmails: boolean,
    isLoadingPhones: boolean,
    isLoadingUrls: boolean,
}
export const useEntryContactsStore = defineStore('EntryContactsStore', {
    state: (): EntryContactsStoreState => {
        return {
            emails: [],
            phones: [],
            urls: [],
            entryId: '',
            isLoadingEmails: false,
            isLoadingPhones: false,
            isLoadingUrls: false,
        }
    },
    actions: {
        // async getAllContacts() {
        //     // await this.getEmails();
        //
        //     // await Promise.all([
        //     //     this.getEmails(),
        //     //     this.getPhones(),
        //     //     this.getUrls(),
        //     // ])
        // },
        async getPhones() {
            if (this.entryId === '') throw Error('entryId missing');
            if (this.isLoadingPhones) {
                return;
            }
            this.isLoadingPhones = true;

            try {
                this.phones = await apiEntryText.list(this.entryId, {textType: 'Phone'});
            } finally {
                this.isLoadingPhones = false;
            }
        },
        async getUrls() {
            if (this.entryId === '') throw Error('entryId missing');
            if (this.isLoadingUrls) {
                return;
            }
            this.isLoadingUrls = true;

            try {
                this.urls = await apiEntryText.list(this.entryId, {textType: 'Url'});
            } finally {
                this.isLoadingUrls = false;
            }
        },
        async getEmails() {
            if (this.entryId === '') throw Error('entryId missing');
            if (this.isLoadingEmails) {
                return;
            }
            this.isLoadingEmails = true;

            try {
                this.emails = await apiEntryText.list(this.entryId, {textType: 'Email'});
            } finally {
                this.isLoadingEmails = false;
            }
        }
    }
})