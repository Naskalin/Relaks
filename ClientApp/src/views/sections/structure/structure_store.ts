import {defineStore} from 'pinia';
import {apiStructure, Structure, StructureListRequest, StructureTree} from "../../../api/rerources/api_structure";

declare type StructureStoreState = {
    tree: StructureTree[],
    list: Structure[],
    expandedIds: string[],
    structureSelectedId: null | string,
    listRequest: StructureListRequest
}
export const useStructureStore = defineStore('StructureStore', {
    state: (): StructureStoreState => {
        return {
            tree: [],
            list: [],
            structureSelectedId: null,
            expandedIds: [],
            listRequest: {
                date: null,
                isDeleted: null
            }
        }
    },
    getters: {
        structureSelected(): null | Structure {
            if (!this.structureSelectedId) return null;
            if (!Object.keys(this.structuresById).length) return null;
            
            return this.structuresById[this.structureSelectedId]
        },
        structuresById(): {[index: string]: Structure} {
            let data: {[index: string]: Structure} = {};
            if(!this.list.length) return data;
            this.list.forEach(x => data[x.id] = x);
            return data;
        }
    },
    actions: {
        async getStructuresAsync(entryId: string)
        {
            this.tree = await apiStructure.tree(entryId, {
                ...this.listRequest,
                isTree: true,
            });
            this.list = await apiStructure.list(entryId, this.listRequest);
            if(this.tree.length) this.structureSelectedId = this.tree[0].id;
        },
    }
})