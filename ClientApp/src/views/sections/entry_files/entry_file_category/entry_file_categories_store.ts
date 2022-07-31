import {defineStore} from 'pinia';
import {
    apiFileCategories,
    FileCategory,
    FileCategoryListRequest,
    FileCategoryTree
} from "../../../../api/rerources/api_file_categories";

declare type EntryFileCategoriesStoreState = {
    list: FileCategory[],
    tree: FileCategoryTree[],
    selectedId: string | null,
    listRequest: FileCategoryListRequest
}
export const useEntryFileCategoriesStore = defineStore('EntryFileCategoriesStore', {
    state: (): EntryFileCategoriesStoreState => {
        return {
            list: [],
            tree: [],
            selectedId: null,
            listRequest: {
                entryId: '',
                isDeleted: false
            }
        }
    },
    getters: {
        selectedCategory(): null | FileCategory {
            if (!this.selectedId) return null;
            if (!Object.keys(this.categoriesById).length) return null;

            return this.categoriesById[this.selectedId]
        },
        categoriesById(): {[index: string]: FileCategory} {
            let data: {[index: string]: FileCategory} = {};
            if(!this.list.length) return data;
            this.list.forEach(x => data[x.id] = x);
            return data;
        }
    },
    actions: {
        async getItems() {
            this.list = await apiFileCategories.list({
                ...this.listRequest,
                isTree: false,
            })
            const data = await apiFileCategories.list({
                ...this.listRequest,
                isTree: true,
            })
            this.tree = data.children;
        }
    }
})