<template>
    <div v-if="store.structureConnections.length">
        <p class="text-center text-h6 q-mb-lg">Связи</p>
        <div class="q-gutter-y-md">
            <connection-item
                v-for="connection in store.structureConnections"
                :connection="connection"
                :key="connection.id"/>
        </div>
    </div>
</template>

<script setup lang="ts">
import {useStructureConnectionsStore} from "./structure_connections_store";
import {watch, onMounted} from 'vue';
import {useStructureStore} from "./structure_store";
import ConnectionItem from './Connection.Item.vue';

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

</script>