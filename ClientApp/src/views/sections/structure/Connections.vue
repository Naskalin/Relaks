<template>
    <div class="row q-col-gutter-md items-center justify-between">
        <div class="col-auto">
            <h6 class="q-ma-none">Связи</h6>
        </div>
        <div class="col-auto q-gutter-sm">
            <q-btn icon="las la-arrows-alt-h" round color="primary" v-tooltip.left="'Добавить связь'"/>
        </div>
    </div>
    <div class="q-gutter-y-md q-mt-sm" v-if="store.structureConnections.length">
        <q-list v-if="structureStore.structureSelectedId" bordered class="rounded-borders">
            <connections-item
                v-for="connection in store.structureConnections"
                :connection="connection"
                :key="connection.id"/>
        </q-list>
    </div>
</template>

<script setup lang="ts">
import {useStructureConnectionsStore} from "./structure_connections_store";
import {watch, onMounted} from 'vue';
import {useStructureStore} from "./structure_store";
import ConnectionsItem from './Connections.Item.vue';

const structureStore = useStructureStore();
const store = useStructureConnectionsStore();

onMounted(() => {
    setTimeout(() => {
        // load first connection
        store.drawActiveStructureConnections(structureStore.structureSelectedId, null);  
    }, 1000)
})
watch(() => store.activeConnectionId, (val: string | null, oldVal: string | null) => {
    store.drawActiveConnection(oldVal);
})
// watch(() => structureStore.expandedIds, (expandedIds: string[]) => {
//     // structureStore.list.filter(x => x.parentId)
//     console.log('expandedIds', expandedIds.length, 'list', structureStore.list.length);
// })
</script>