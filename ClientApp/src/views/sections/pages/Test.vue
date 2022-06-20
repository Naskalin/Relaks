<template>
    <div>
        <q-card>
            <q-card-section>
                <q-splitter
                    v-model="splitterModel"
                    :limits="[40, 80]"
                >
                    <template v-slot:before>
                        <div class="q-pa-md">
                            <q-tree
                                v-if="structureTree.length"
                                :nodes="structureTree"
                                node-key="id"
                                selected-color="primary"
                                v-model:selected="selectedId"
                                :default-expand-all="true"
                                no-selection-unset
                            >
                                <template v-slot:default-header="prop">
                                    <div>
                                        <div>{{ prop.node.data.title }}</div>
                                        <div v-if="prop.node.data.description" class="text-grey-8">
                                            <q-icon name="las la-comment q-mr-xs" size="1.1em"/>
                                            <span style="font-size: .9rem">{{ prop.node.data.description }}</span>
                                        </div>
                                    </div>
                                </template>
                            </q-tree>
                        </div>
                    </template>

                    <template v-slot:after>
                        {{ selectedId }}
                        <hr>
                        {{ structures[selectedId] }}
                    </template>
                </q-splitter>
            </q-card-section>
        </q-card>

    </div>
</template>

<script setup lang="ts">
import {ref, onMounted} from 'vue'
import {apiStructure, Structure, StructureTree} from "../../../api/rerources/api_structure";

onMounted(async () => {
    await getStructures();
});

const structureTree = ref<StructureTree[]>([]);
const selectedId = ref<null | string>(null);
const structures = ref<{[index: string]: Structure}>({});

const getStructures = async () => {
    const dataTree = await apiStructure.tree('01b137da-a3cf-4c08-ac3e-752b3f156ed4', {});
    structures.value = {};
    rebuildTreeWithId(dataTree);
    
    structureTree.value = dataTree;
    if (dataTree.length) selectedId.value = dataTree[0].id;
}

const rebuildTreeWithId = (items: StructureTree[]) => {
    items.forEach((tree: StructureTree, key: number) => {
        structures.value[tree.data.id] = tree.data;
        
        items[key] = {
            ...tree,
            id: tree.data.id,
        }
        if (tree.children.length) {
            rebuildTreeWithId(tree.children);
        }
    })
}
const splitterModel = ref(70);
</script>