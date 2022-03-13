import { defineStore } from 'pinia';
import {EntryType, Nullable} from "../../types/types";
import {jsonApi} from "../../api";
import {ApiGetResponse} from "../../api/get";
import {date} from "quasar";
import {dateStrToISOStr} from "../../utils/utils";

export declare type EntryAddModel = {
    entryType: EntryType,
    name: string,
    reputation: number,
    description: Nullable<string>,
    startAt: Nullable<string>,
    endAt: Nullable<string>
}

declare type EntryAddStoreState = {
    model: EntryAddModel,
    isCreating: boolean,
    isShowModal: boolean,
}
export const useEntryAddStore = defineStore('entryAddStore', {
    state: (): EntryAddStoreState => {
        return {
            model: {
                name: '',
                reputation: 5,
                entryType: 'Person',
                description: null,
                startAt: null,
                endAt: null,
            },
            isCreating: false,
            isShowModal: false,
        }
    },
    actions: {
        async addEntry() : Promise<ApiGetResponse> {
            if (this.isCreating) {
                return Promise.reject();
            }
            
            this.isCreating = true;
            
            try {
                const data = Object.assign({}, this.model);
                data.startAt = dateStrToISOStr(data.startAt);
                data.endAt = dateStrToISOStr(data.endAt);
                return await jsonApi.post({resource: 'entries'}, {
                    data: {
                        type: 'entries',
                        attributes: data
                    }
                });
            } finally {
                this.isCreating = false;   
            }
        }
    }
})