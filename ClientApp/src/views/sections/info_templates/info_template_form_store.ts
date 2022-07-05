import {defineStore} from 'pinia';
import {apiInfoTemplate, InfoTemplate, InfoTemplateFormRequest} from "../../../api/rerources/api_info_templates";

declare type InfoTemplateFormStoreState = {
    request: InfoTemplateFormRequest & { id?: string }
    status: 'edit' | 'create' | null;
    isLoading: boolean,
}
export const useInfoTemplateFormStore = defineStore('InfoTemplateFormStore', {
    state: (): InfoTemplateFormStoreState => {
        return {
            isLoading: false,
            status: null,
            request: {
                title: '',
                template: {
                    groups: []
                },
            }
        }
    },
    actions: {
        async createAsync(): Promise<InfoTemplate> {
            if (this.isLoading) return Promise.reject('form isLoading');
            this.isLoading = true;

            try {
                return await apiInfoTemplate.create(this.request);
            } finally {
                this.isLoading = false;
            }
        },
        async editAsync(): Promise<null> {
            if (this.isLoading) return Promise.reject('form isLoading');
            if (!this.request.id) return Promise.reject('edit id is missing');
            this.isLoading = true;

            try {
                return await apiInfoTemplate.update(this.request.id, this.request);
            } finally {
                this.isLoading = false;
            }
        }
    }
})