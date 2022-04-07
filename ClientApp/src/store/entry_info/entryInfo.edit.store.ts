import {defineStore} from 'pinia';
import {
    EntryDate,
    EntryDateFormRequest, EntryEmail,
    EntryEmailFormRequest,
    EntryInfoType, EntryNote,
    EntryNoteFormRequest, EntryPhone, EntryPhoneFormRequest, EntryUrl,
    EntryUrlFormRequest
} from "../../api/api_types";
import {apiEntryInfo} from "../../api/rerources/api_entry_info";
import {apiMappers} from "../../api/api_mappers";

declare type EntryInfoEditStoreState = {
    Note: { model: null | EntryNoteFormRequest },
    Email: { model: null | EntryEmailFormRequest },
    Date: { model: null | EntryDateFormRequest },
    Url: { model: null | EntryUrlFormRequest },
    Phone: { model: null | EntryPhoneFormRequest },
    isLoading: boolean
}
export const useEntryInfoEditStore = defineStore('EntryInfoEditStore', {
    state: (): EntryInfoEditStoreState => {
        return {
            isLoading: false,
            Note: {model: null},
            Email: {model: null},
            Date: {model: null},
            Url: {model: null},
            Phone: {model: null},
        }
    },
    actions: {
        setModel(eInfo: any, eInfoType: EntryInfoType): void {
            this[eInfoType].model = apiMappers.toEntryInfoFormRequest[eInfoType](eInfo);
        },
        async update(entryId: string, entryTextId: string, entryInfoType: EntryInfoType): Promise<null> {
            if (this.isLoading || !this[entryInfoType].model) {
                return Promise.reject();
            }
            this.isLoading = true;

            try {
                return await apiEntryInfo[entryInfoType].update(entryId, entryTextId, this[entryInfoType].model as any);
            } finally {
                this.isLoading = false;
            }
        }
    }
})