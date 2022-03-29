import {defineStore} from 'pinia';
import {apiEntryText} from "../../api/rerources/api_entry_text";
import {EntryText} from "../../api/api_types";

declare type EntryContactsStoreState = {
    emails: EntryText[]
    phones: EntryText[]
    urls: EntryText[]
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
            isLoadingEmails: false,
            isLoadingPhones: false,
            isLoadingUrls: false,
        }
    },
    actions: {
        async getAllContacts(entryId: string) {
            await this.getEmails(entryId);
            await this.getPhones(entryId);
            await this.getUrls(entryId);
        },
        async getPhones(entryId: string) {
            if (this.isLoadingPhones) {
                return;
            }
            this.isLoadingPhones = true;

            try {
                this.phones = await apiEntryText.list(entryId, {textType: 'Phone'});
            } finally {
                this.isLoadingPhones = false;
            }
        },
        async getUrls(entryId: string) {
            if (this.isLoadingUrls) {
                return;
            }
            this.isLoadingUrls = true;

            try {
                this.urls = await apiEntryText.list(entryId, {textType: 'Url'});
            } finally {
                this.isLoadingUrls = false;
            }
        },
        async getEmails(entryId: string) {
            if (this.isLoadingEmails) {
                return;
            }
            this.isLoadingEmails = true;

            try {
                this.emails = await apiEntryText.list(entryId, {textType: 'Email'});
            } finally {
                this.isLoadingEmails = false;
            }
        }
    }
})