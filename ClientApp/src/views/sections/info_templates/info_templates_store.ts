import {defineStore} from 'pinia';
import {apiInfoTemplate, InfoTemplate, InfoTemplateListRequest} from "../../../api/rerources/api_info_templates";

declare type InfoTemplatesStoreState = {
    items: InfoTemplate[]
    isLoading: boolean
}
export const useInfoTemplatesStore = defineStore('InfoTemplatesStore', {
    state: (): InfoTemplatesStoreState => {
        return {
            items: [],
            isLoading: false,
        }
    },
    actions: {
        async getItemsAsync() {
            this.items = await apiInfoTemplate.list({});
        }
    }
})