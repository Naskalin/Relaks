import {defineStore} from 'pinia';
import {StructureItemFormRequest} from "../../../api/rerources/api_structure_items";

declare type StructureItemsFormStoreState = {
    request: StructureItemFormRequest,
    editId: string,
    isShowCreate: boolean,
    isShowEdit: boolean,
}
export const useStructureItemFormStore = defineStore('StructureItemsFormStore', {
    state: (): StructureItemsFormStoreState => {
        return {
            isShowCreate: false,
            isShowEdit: false,
            editId: '',
            request: {
                deletedAt: null,
                deletedReason: '',
                startAt: null,
                structureId: '',
                description: '',
                entryId: ''
            }
        }
    },
    actions: {}
})