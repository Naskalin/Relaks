import {defineStore} from 'pinia';
import {StructureItemFormRequest} from "../../../api/rerources/api_structure_items";

declare type StructureItemsFormStoreState = {
    request: StructureItemFormRequest,
    editId: string | null,
    isShowCreate: boolean,
    isShowEdit: boolean,
}
export const useStructureItemFormStore = defineStore('StructureItemsFormStore', {
    state: (): StructureItemsFormStoreState => {
        return {
            isShowCreate: false,
            isShowEdit: false,
            editId: null,
            request: {
                deletedAt: null,
                deletedReason: '',
                startAt: '',
                structureId: '',
                description: '',
                entryId: ''
            }
        }
    },
    actions: {}
})