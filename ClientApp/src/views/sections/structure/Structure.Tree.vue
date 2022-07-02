<template>
    <structure-new-modal @onSave="resetTree"/>
    <structure-edit-modal @onUpdate="resetTree"/>

    <div class="row q-mb-lg items-center">
        <div class="col">
            <h6 class="q-my-none">Группы</h6>
        </div>
        <div class="col-auto">
            <q-btn
                v-if="connectionsStore.connections.length"
                size="md"
                @click="switchShowLines"
                color="secondary"
                outline
                :icon="connectionsStore.isShowLines ? 'las la-eye-slash' : 'las la-eye'"
                :label="connectionsStore.isShowLines ? 'Скрыть связи' : 'Показать связи'"/>
        </div>
    </div>
    
    <q-tree
        v-if="structureStore.tree.length"
        ref="treeEl"
        :nodes="structureStore.tree"
        :duration="0"
        node-key="id"
        selected-color="primary"
        default-expand-all
        no-selection-unset
        v-model:selected="structureStore.structureSelectedId"
        v-model:expanded="structureStore.expandedIds"
    >
        <template v-slot:default-header="prop">
            <div :id="prop.node.id" class="js-structure-connections structure-connection q-py-xs q-px-sm">
                <div>
                    {{ prop.node.data.title }}
                    <q-icon v-if="prop.node.data.description" v-tooltip="prop.node.data.description" class="q-ml-sm" name="las la-comment"/>
                </div>
            </div>
        </template>
    </q-tree>
</template>

<script setup lang="ts">
import {useStructureStore} from "./structure_store";
import {useStructureConnectionsStore} from "./structure_connections_store";
import {onMounted, ref, watch} from "vue";
import StructureNewModal from './Structure.New.Modal.vue';
import StructureEditModal from './Structure.Edit.Modal.vue';
// import {useStructureFormStore} from "./structure_form_store";
// import {Structure} from "../../../api/rerources/api_structure";

const treeEl = ref<any>(null);
const props = defineProps<{
    entryId: string
}>() 
const structureStore = useStructureStore();
const connectionsStore = useStructureConnectionsStore();
const resetTree = async (structureId?: string) => {
    connectionsStore.deleteConnections();
    connectionsStore.$reset();
    let structureSelectedId = structureStore.structureSelectedId;
    if (structureId) structureSelectedId = structureId;

    structureStore.structureSelectedId = null;
    await structureStore.getStructuresAsync(props.entryId);
    await connectionsStore.getConnectionsAsync(props.entryId);
    connectionsStore.isShowLines = true;
    connectionsStore.drawConnections(connectionsStore.connections);
    structureStore.structureSelectedId = structureSelectedId;
    if (structureStore.structureSelectedId) {
        connectionsStore.drawActiveStructureConnections(structureStore.structureSelectedId, null);
    }
    if (treeEl.value) treeEl.value.expandAll();
    
}
let allExpandedCount = ref(0);
onMounted(async () => {
    await structureStore.getStructuresAsync(props.entryId);
    await connectionsStore.getConnectionsAsync(props.entryId);
    connectionsStore.drawConnections(connectionsStore.connections);

    if (treeEl.value) {
        allExpandedCount.value = treeEl.value.getExpandedNodes().length;
    }
})
watch(() => structureStore.expandedIds, (val: string[]) => {
    if (treeEl.value && val.length && val.length !== allExpandedCount.value) {
        connectionsStore.hideLines();
    }
});
watch(() => structureStore.structureSelectedId, async (structureId: any, oldVal: any) => {
    if (!structureId) return;
    connectionsStore.drawActiveStructureConnections(structureId, oldVal);
}, {immediate: true})
const switchShowLines = () => {
    if (connectionsStore.isShowLines) {
        connectionsStore.hideLines();
    } else {
        treeEl.value.expandAll();
        setTimeout(() => {
            // Задержка анимации открытия, иначе линии криво отрисуются
            connectionsStore.showLines();  
        }, 500)
    }
}
</script>

<style lang="scss">
.q-tree__node--selected {
    background: $indigo-2;
}
.q-tree__node-header:hover {
    background: $indigo-2;
}
.structure-connection {
    position: relative;
    &__el {
        position: absolute;
        top: 50%;
        display: block;
        &.bidirectional {
            left: 0;
        }
        &.normal.first {
            right: 0;
        }
        &.normal.second {
            left: 0;
        }
        &.reverse.first {
            left: 0;
        }
        &.reverse.second {
            right: 0;
        }
    }
}
</style>