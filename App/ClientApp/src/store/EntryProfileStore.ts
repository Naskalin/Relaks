import {defineStore} from 'pinia';
import {jsonApi} from "../api";
import {ApiGetRequest, JsonApiResource} from "../api/get";
import {Nullable} from "../types/types";

interface IEntryProfile {
    entry: null | JsonApiResource
}

export const useEntryProfileStore = defineStore('entryProfile', {
    state: (): IEntryProfile => {
        return {
            entry: null
        }
    },
    actions: {
        async getEntry(endPoint: string, apiRequest?: ApiGetRequest) {
            // JsonApiResource
            // @ts-ignore
            // this.entry = await jsonApi.get(endPoint, apiRequest);
            const resp = await jsonApi.get(endPoint, apiRequest);
            this.entry = resp as JsonApiResource;
            console.log(resp);
            // this.entry = resp;
            // return await jsonApi.get(endPoint, apiRequest);
        }
    }
})