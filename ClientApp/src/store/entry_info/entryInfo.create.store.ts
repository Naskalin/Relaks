import {defineStore} from 'pinia';
import {
    EntryInfoType,
    EntryDateFormRequest,
    EntryEmailFormRequest,
    EntryNoteFormRequest,
    EntryPhoneFormRequest,
    EntryUrlFormRequest, EntryEmail, EntryNote, EntryPhone, EntryDate, EntryUrl
} from "../../api/api_types";
import {apiEntryInfo} from "../../api/rerources/api_entry_info";

declare type EntryInfoCreateStoreState = {
    Email: EntryEmailFormRequest,
    Phone: EntryPhoneFormRequest,
    Url: EntryUrlFormRequest,
    Note: EntryNoteFormRequest,
    Date: EntryDateFormRequest,
    isLoading: boolean,
}
const commonFields = {
    title: '',
    deletedReason: '',
    deletedAt: null,
}

export const useEntryInfoCreateStore = defineStore('EntryInfoCreateStore', {
    state: (): EntryInfoCreateStoreState => {
        return {
            isLoading: false,
            Email: {
                email: '',
                ...commonFields,
            },
            Url: {
                url: '',
                ...commonFields,
            },
            Note: {
                note: '',
                ...commonFields,
            },
            Date: {
                date: '',
                ...commonFields,
            },
            Phone: {
                phoneNumber: '',
                phoneRegion: '',
                ...commonFields,
            },
        }
    },
    actions: {
        async create(entryId: string, eInfoType: EntryInfoType): Promise<any> {
            if (this.isLoading) {
                return Promise.reject();
            }
            this.isLoading = true;

            try {
                return await apiEntryInfo[eInfoType].create(entryId, this[eInfoType] as any) as any;
            } finally {
                this.isLoading = false;
            }
        },
        async createEmail(entryId: string): Promise<EntryEmail> {
            return await this.create(entryId, 'Email');
        },
        async createNote(entryId: string): Promise<EntryNote> {
            return await this.create(entryId, 'Note');
        },
        async createPhone(entryId: string): Promise<EntryPhone> {
            return await this.create(entryId, 'Phone');
        },
        async createDate(entryId: string): Promise<EntryDate> {
            return await this.create(entryId, 'Date');
        },
        async createUrl(entryId: string): Promise<EntryUrl> {
            return await this.create(entryId, 'Url');
        }
    }
})

