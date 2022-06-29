import {defineStore} from 'pinia';
import {
    StructureConnectionDirection,
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
                title: '',
                description: '',
                direction: 'Normal',
                structureFirstId: '',
                structureSecondId: ''
            },
            isShowEdit: false,
            isShowCreate: false,
            editId: null,
        }
    },
    actions: {}
})