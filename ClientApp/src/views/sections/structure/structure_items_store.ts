import {defineStore} from 'pinia';
import {StructureItem, StructureItemListRequest} from "../../../api/rerources/api_structure_items";
import {apiStructureItems} from "../../../api/rerources/api_structure_items";
import {useStructureStore} from "./structure_store";

declare type StructureItemsStoreState = {
    items: StructureItem[],
    listRequest: StructureItemListRequest
}
export const useStructureItemsStore = defineStore('StructureItemsStore', {
    state: (): StructureItemsStoreState => {
        return {
            items: [],
            listRequest: {
                page: 1,
                perPage: 50,
            }
        }
    },
    actions: {
        async getItemsAsync(structureId: string)
        {
            const structureStore = useStructureStore();
            this.items = await apiStructureItems.list({
                ...this.listRequest,
                structureId,
                date: structureStore.listRequest.date,
                isDeleted: structureStore.listRequest.isDeleted
            });
        }
    }
})