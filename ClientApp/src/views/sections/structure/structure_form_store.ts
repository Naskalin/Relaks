import {defineStore} from 'pinia';
import {StructureFormRequest} from "../../../api/rerources/api_structure";

declare type StructureFormStoreState = {
    request: StructureFormRequest
    isShowCreate: boolean,
    isShowEdit: boolean,
    editId: string | null
}
export const useStructureFormStore = defineStore('StructureFormStore', {
    state: (): StructureFormStoreState => {
        return {
            isShowEdit: false,
            isShowCreate: false,
            editId: null,
            request: {
                title: '',
                description: '',
                parentId: null,
                deletedReason: '',
                deletedAt: null,
                startAt: null,
            }
        }
    },
})