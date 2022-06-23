<template>
    <q-card
        @click="setActiveConnection"
        :class="connectionsStore.activeConnectionId === connection.id ? 'bg-indigo-9 text-white' : 'bg-blue-grey-2'"
    >
        <q-card-section class="q-gutter-y-sm">
            <div v-if="connection.title" class="text-weight-bold">{{ connection.title }}</div>
            <div v-if="connection.description">{{ connection.description }}</div>
            <div class="text-italic">{{ relationText }}</div>
        </q-card-section>
    </q-card>
</template>

<script setup lang="ts">
import {StructureConnection} from "../../../api/rerources/api_structure_connections";
import {useStructureConnectionsStore} from "./structure_connections_store";
import {computed} from 'vue';
import {useStructureStore} from "./structure_store";

const props = defineProps<{
    connection: StructureConnection,
}>()
const connectionsStore = useStructureConnectionsStore();
const structureStore = useStructureStore();
const setActiveConnection = () => {
    if (connectionsStore.activeConnectionId === props.connection.id) {
        connectionsStore.activeConnectionId = null;
        return;
    }
    connectionsStore.activeConnectionId = props.connection.id;
}
const relationText = computed(() => {
    if (structureStore.structureSelected && Object.keys(structureStore.structuresById).length) {
        let str = [structureStore.structureSelected.title];
        let isFirst = false;
        let secondStructureId = props.connection.structureSecondId;
        if(props.connection.structureFirstId !== structureStore.structureSelected.id) {
            secondStructureId = props.connection.structureFirstId;
            isFirst = true; 
        }
        
        switch (props.connection.direction) {
            case "Normal":
                str.push(isFirst ? '<<-' : '->>');
                break;
            case "Reverse":
                str.push(isFirst ? '->>' : '<<-');
                break;
            case "Bidirectional":
                str.push('<<->>');
                break;
        }

        const secondStructure = structureStore.structuresById[secondStructureId] || null;
        if(secondStructure) {
            str.push(secondStructure.title);
        }
        
        return str.join(' ');
    }
})
</script>