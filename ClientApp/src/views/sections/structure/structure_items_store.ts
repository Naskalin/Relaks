import {defineStore} from 'pinia';
import {StructureItem, StructureItemListRequest} from "../../../api/rerources/api_structure_items";
import {apiStructureItems} from "../../../api/rerources/api_structure_items";
import {useStructureStore} from "./structure_store";

declare type StructureItemsStoreState = {
    items: StructureItem[],
    listRequest: StructureItemListRequest
    isEnd: boolean
    isLoading: boolean
    previewItem: null | StructureItem
}
export const useStructureItemsStore = defineStore('StructureItemsStore', {
    state: (): StructureItemsStoreState => {
        return {
            items: [],
            isEnd: false,
            isLoading: false,
            previewItem: null,
            listRequest: {
                page: 1,
                perPage: 50,
            }
        }
    },
    actions: {
        resetListRequest()
        {
            this.listRequest.page = 1;
            this.isEnd = false;
        },
        async getItemsAsync()
        {
            if (this.isLoading || this.isEnd) {
                return;
            }
            const structureStore = useStructureStore();
            if (!structureStore.structureSelectedId) return;
            
            this.isLoading = true;
            
            try {
                const respItems = await apiStructureItems.list({
                    ...this.listRequest,
                    structureId: structureStore.structureSelectedId,
                    date: structureStore.listRequest.date,
                    isDeleted: structureStore.listRequest.isDeleted
                });
                if (this.listRequest.page > 1) {
                    this.items = this.items.concat(respItems);
                } else {
                    this.items = respItems;
                }

                this.listRequest.page += 1;
                if (respItems.length < this.listRequest.perPage) {
                    this.isEnd = true;
                }
            } finally {
                this.isLoading = false;
            }
        }
    }
})