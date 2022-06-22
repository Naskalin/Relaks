import {defineStore} from 'pinia';
import {StructureItem, StructureItemListRequest} from "../../../api/rerources/api_structure_items";
import {apiStructureItems} from "../../../api/rerources/api_structure_items";

declare type StructureItemsStoreState = {
    items: StructureItem[],
    listRequest: StructureItemListRequest
}
export const useStructureItemsStore = defineStore('StructureItemsStore', {
    state: (): StructureItemsStoreState => {
        return {
            items: [],
            listRequest: {
                isDeleted: null,
                entryType: null
            }
        }
    },
    actions: {
        async getItemsAsync(structureId: string)
        {
            this.items = await apiStructureItems.list({
                structureId,
                ...this.listRequest
            });
        }
    }
})