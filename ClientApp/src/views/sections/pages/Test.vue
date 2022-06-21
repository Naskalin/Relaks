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
                                        <div>
                                            {{ prop.node.data.title }}
                                            <div :id="prop.node.id" class="js-structure-connections structure-connection"></div>
                                        </div>
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
import {apiStructureConnection, StructureConnection} from "../../../api/rerources/api_structure_connections";
import LeaderLine from "leader-line-new";

onMounted(async () => {
    await getStructures();
    await getStructureConnections();
});

const structureTree = ref<StructureTree[]>([]);
const selectedId = ref<null | string>(null);
const structures = ref<{ [index: string]: Structure }>({});
// const structureConnections = ref<{[index: string]: StructureConnection[]}>({});
const entryId = '01b137da-a3cf-4c08-ac3e-752b3f156ed4';

const getStructures = async () => {
    const dataTree = await apiStructure.tree(entryId, {});
    structures.value = {};
    rebuildTreeWithId(dataTree);

    structureTree.value = dataTree;
    if (dataTree.length) selectedId.value = dataTree[0].id;
}

const getStructureConnections = async () => {
    // list structure connections
    const connections = await apiStructureConnection.list({entryId: entryId});
    const structuresEls = Array.from(document.querySelectorAll('.js-structure-connections'));
    if (!structuresEls.length) return;
    
    // const connectionMap = [];
    let connectionItem: {[index: string]: Element};

    for (const [index, connection] of connections.entries()) {
        connectionItem = {};
        
        [
            {key: 'first', structureId: connection.structureFirstId},
            {key: 'second', structureId: connection.structureSecondId}
        ].forEach(item => {
            const findStructuresEls = structuresEls.filter(el => el.id === item.structureId);
            if(!findStructuresEls.length) {
                throw new Error('structureId is missing for StructureConnection');
            }
            const boxEl = findStructuresEls[0];
            const connectionEl = document.createElement('div');
            connectionEl.id = item.key + '_' + connection.id;
            connectionEl.classList.add('structure-connection__el', item.key);
            // connectionEl.innerText = connectionEl.id;

            boxEl.appendChild(connectionEl);
            connectionItem[item.key] = connectionEl;
            
            // if(structureConnections.value.hasOwnProperty(structureId))
            // {
            //     if(!structureConnections.value[structureId].includes(connection)) {
            //         structureConnections.value[structureId].push(connection);
            //     }
            // } else {
            //     structureConnections.value[structureId] = [connection]
            // }
        })
        
        new LeaderLine(connectionItem.first, connectionItem.second);
        // connectionMap.push(connectionItem);
        // new LeaderLine(
        //     document.getElementById('start'),
        //     document.getElementById('end')
        // );
    }
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

<style lang="scss">
.structure-connection {
    display: inline-block;
    position: relative;
    width: 15px;
    height: 15px;
    &__el {
        position: absolute;
        left: 0;
        top: 0;
        display: block;
        width: 15px;
        height: 15px;
        border-radius: 50%;
        background: $positive;
        margin: 0 10px;
    }
}
</style>