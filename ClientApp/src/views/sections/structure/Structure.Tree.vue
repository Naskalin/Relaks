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
        :default-expand-all="true"
        no-selection-unset
        v-model:expanded="structureStore.expandedIds"
    >
        <template v-slot:default-header="prop">
            <div>
                <div :id="prop.node.id" class="js-structure-connections structure-connection q-py-xs q-px-sm">
                    <div>{{ prop.node.data.title }}</div>
                    <div v-if="prop.node.data.description" class="text-grey-8">
                        <q-icon name="las la-comment q-mr-xs" size="1.1em"/>
                        <span style="font-size: .9rem">{{ prop.node.data.description }}</span>
                    </div>
                </div>
            </div>
        </template>
    </q-tree>
</template>

<script setup lang="ts">
import {useStructureStore} from "./structure_store";
import {useStructureConnectionsStore} from "./structure_connections_store";
import {onMounted, ref, watch} from "vue";
const treeEl = ref<any>(null);
const props = defineProps<{
    entryId: string
}>() 
const structureStore = useStructureStore();
const connectionStore = useStructureConnectionsStore();
let allExpandedCount = ref(0);
onMounted(async () => {
    await structureStore.getStructuresAsync(props.entryId);
    await connectionStore.getConnectionsAsync(props.entryId);
    connectionStore.drawConnections(connectionStore.connections);

    if (treeEl.value) {
        console.log(treeEl.value.getExpandedNodes().length);
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