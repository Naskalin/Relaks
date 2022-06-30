import {defineStore} from 'pinia';
import {
    StructureConnectionFormRequest
} from "../../../api/rerources/api_structure_connections";

declare type StructureConnectionsFormStoreState = {
    request: StructureConnectionFormRequest
    isShowCreate: boolean,
    isShowEdit: boolean,
    editId: string | null
}
export const useStructureConnectionsFormStore = defineStore('StructureConnectionsFormStore', {
    state: (): StructureConnectionsFormStoreState => {
        return {
            request: {
                description: '',
                direction: 'Normal',
                structureFirstId: '',
                structureSecondId: '',
                deletedAt: null,
                deletedReason: '',
                startAt: null
            },
            isShowEdit: false,
            isShowCreate: false,
            editId: null,
        }
    },
    actions: {}
})