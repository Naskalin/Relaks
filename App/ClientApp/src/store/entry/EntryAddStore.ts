import { defineStore } from 'pinia';
import {EntryType} from "../../types/types";
import {jsonApi} from "../../api";
import {ApiGetResponse} from "../../api/get";

export declare type EntryAddModel = {
    entryType: EntryType,
    name: string,
    reputation: number,
    description?: string
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
                entryType: 'Person'
            },
            isCreating: false,
            isShowModal: false,
        }
    },
    actions: {
        async addEntry(data: EntryAddModel) : Promise<ApiGetResponse> {
            if (this.isCreating) {
                return Promise.reject();
            }
            
            this.isCreating = true;
            
            try {
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