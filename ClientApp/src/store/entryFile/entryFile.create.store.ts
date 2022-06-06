import {defineStore} from 'pinia';
import {apiEntryFile} from "../../api/rerources/api_entry_file";

declare type EntryFileCreateStoreState = {
    isCreateCategory: boolean
    newCategory: string
    category: string
    files: null | any
    isLoading: boolean
    isError: boolean
}
export const useEntryFileCreateStore = defineStore('EntryFileCreateStore', {
    state: (): EntryFileCreateStoreState => {
        return {
            isCreateCategory: false,
            newCategory: '',
            category: '',
            files: null,
            isLoading: false,
            isError: false,
        }
    },
    actions: {
        async uploadFiles(entryId: string) {
            if (this.isLoading || !this.files) return;
            try {
                this.isLoading = true;
                const formData = new FormData();
                for (const [key, val] of Object.entries(this.files)) {
                    formData.append('files', val as any);
                }
                if (this.isCreateCategory && this.newCategory !== '') {
                    formData.append('category', this.newCategory);
                } else if (this.category) {
                    formData.append('category', this.category);
                }
                const resp = await apiEntryFile.create(entryId, formData);
                this.files = null;
                this.isError = false;

                return resp;
            } catch (e) {
                this.isError = true;
            } finally {
                this.isLoading = false;
            }
        }
    }
})