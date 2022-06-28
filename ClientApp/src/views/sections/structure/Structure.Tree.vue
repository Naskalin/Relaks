<template>
    <div class="q-mb-md">
        <q-btn
            v-if="connectionStore.connections.length"
            size="md"
            @click="switchShowLines"
            :icon="connectionStore.isShowLines ? 'las la-eye-slash' : 'las la-eye'"
            :label="connectionStore.isShowLines ? 'Скрыть связи' : 'Показать связи'"/>
    </div>
    
    <q-tree
        ref="treeEl"
        v-if="structureStore.tree.length"
        :nodes="structureStore.tree"
        node-key="id"
        selected-color="primary"
        v-model:selected="structureStore.structureSelectedId"
        default-expand-all
        no-selection-unset
        v-model:expanded="structureStore.expandedIds"
    >
        <template v-slot:default-header="prop">
            <div class="structure-connection-wrapper">
                <div :id="prop.node.id" class="js-structure-connections structure-connection q-py-xs q-px-sm">
                    <div>{{ prop.node.data.title }}</div>
                </div>
                <div class="q-gutter-x-sm">
                    <q-btn
                        class="structure-connection__edit-btn"
                        color="primary"
                        @click="showEditModal(prop.node.data)"
                        outline
                        icon="las la-edit"
                        size="sm"
                        round
                        v-tooltip="'Изменить'"
                        flat/>
                    <q-btn
                        class="structure-connection__edit-btn"
                        color="primary"
                        @click="showCreateModal(prop.node.id)"
                        outline
                        icon="las la-sitemap"
                        size="sm"
                        v-tooltip="'Добавить группу в эту группу'"
                        round
                        flat/>
                </div>
            </div>
        </template>
        <template v-slot:default-body="prop">
            <div v-if="prop.node.data.description" class="text-grey-8">
                <span style="font-size: .9rem">{{ prop.node.data.description }}</span>
            </div>
        </template>
    </q-tree>
</template>

<script setup lang="ts">
import {useStructureStore} from "./structure_store";
import {useStructureConnectionsStore} from "./structure_connections_store";
import {onMounted, ref, watch} from "vue";
import {useStructureFormStore} from "./structure_form_store";
import {Structure} from "../../../api/rerources/api_structure";

const treeEl = ref<any>(null);
const props = defineProps<{
    entryId: string
}>() 
const structureStore = useStructureStore();
const connectionStore = useStructureConnectionsStore();
const structureFormStore = useStructureFormStore();

const showCreateModal = (structureId: string) => {
    structureFormStore.$reset();
    structureFormStore.request.parentId = structureId;
    // из-за появления модалок глючит leader-line
    // неизбежная задержка на отрисовку leader-line
    setTimeout(() => {
        structureFormStore.isShowCreate = true  
    }, 250);
}
const showEditModal = (structure: Structure) => {
    structureFormStore.$reset();
    structureFormStore.editId = structure.id;
    structureFormStore.request = Object.assign({}, structure);
    setTimeout(() => {
        structureFormStore.isShowEdit = true
    }, 250);
}
let allExpandedCount = ref(0);
onMounted(async () => {
    await structureStore.getStructuresAsync(props.entryId);
    await connectionStore.getConnectionsAsync(props.entryId);
    connectionStore.drawConnections(connectionStore.connections);

    if (treeEl.value) {
        allExpandedCount.value = treeEl.value.getExpandedNodes().length;
    }
})

watch(() => structureStore.expandedIds, (val: string[]) => {
    if (treeEl.value && val.length !== allExpandedCount.value) {
        connectionStore.hideLines();
    }
});
const switchShowLines = () => {
    if (connectionStore.isShowLines) {
        connectionStore.hideLines();
    } else {
        treeEl.value.expandAll();
        connectionStore.showLines();
    }
}
// watch(() => connectionStore.isShowLines, (val: boolean) => {
//     if (val && treeEl.value) treeEl.value.expandAll();
// })
</script>

<style lang="scss">
.q-tree__node--selected {
    background: $indigo-2;
}
.structure-connection-wrapper {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 100%;
    column-gap: 1rem;
}
.q-tree__node-header:hover {
    background: $indigo-2;
    .structure-connection__edit-btn {
        opacity: 1;
    }
    //> .q-tree__node-header-content > .structure-connection > .structure-connection__edit-btn {
    //    opacity: 1;
    //}
}
.structure-connection {
    position: relative;
    
    &__edit-btn {
        margin-left: .5rem;
        opacity: 0;
        transition: opacity .2s ease;
    }
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