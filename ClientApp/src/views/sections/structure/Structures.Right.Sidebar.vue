<template>
    Lorem ipsum dolor sit amet, consectetur adipisicing elit. A cupiditate delectus dignissimos doloribus dolorum hic illo laboriosam minima nobis nulla possimus praesentium quo similique, sint tenetur. Culpa nemo nisi soluta.
    <div v-if="structureStore.list.length && structureStore.structureSelected">
        <structure-item :structure="structureStore.structureSelected"/>
        <q-separator class="q-my-lg"/>
        <connections/>
        <items/>
    </div>
</template>

<script setup lang="ts">
import Items from './Items.vue';
import Connections from './Connections.vue';
import StructureItem from './Structure.Item.vue';

import {useStructureStore} from "./structure_store";

const structureStore = useStructureStore();
</script>