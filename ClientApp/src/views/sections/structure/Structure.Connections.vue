<template>
    <div v-if="store.structureConnections.length">
        <p v-for="connection in store.structureConnections">{{connection}}</p>
    </div>
</template>

<script setup lang="ts">
import {useStructureConnectionsStore} from "./structure_connections_store";

const store = useStructureConnectionsStore();
</script>