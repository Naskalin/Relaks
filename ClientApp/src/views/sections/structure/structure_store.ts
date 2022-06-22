import {defineStore} from 'pinia';
import {apiStructure, Structure, StructureTree} from "../../../api/rerources/api_structure";
import {apiStructureItems} from "../../../api/rerources/api_structure_items";

declare type StructureStoreState = {
    tree: StructureTree[],
    structuresById: {[index: string]: Structure}
    structureSelectedId: null | string
}
export const useStructureStore = defineStore('StructureStore', {
    state: (): StructureStoreState => {
        return {
            tree: [],
            structuresById: {},
            structureSelectedId: null
        }
    },
    getters: {
        structureSelected: (state): null | Structure => {
            if (!state.structureSelectedId) return null;
            return state.structuresById[state.structureSelectedId]
        }
    },
    actions: {
        async getTreeAsync(entryId: string)
        {
            const dataTree = await apiStructure.tree(entryId, {});
            this.structuresById = {};
            this.rebuildTree(dataTree);
            this.tree = dataTree;
            if(dataTree.length) this.structureSelectedId = dataTree[0].id;
        },
        rebuildTree(items: StructureTree[]) {
            items.forEach((tree: StructureTree, key: number) => {
                this.structuresById[tree.data.id] = tree.data;

                items[key] = {
                    ...tree,
                    id: tree.data.id,
                }
                if (tree.children.length) {
                    this.rebuildTree(tree.children);
                }
            })
        }
    }
})