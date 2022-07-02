<template>
    <q-infinite-scroll
        v-if="structureStore.list.length && structureStore.structureSelected"
        ref="scrollEl"
        @load="onLoadAsync"
        :disable="itemsStore.isEnd"
        :offset="250"
    >
        <structure-item :structure="structureStore.structureSelected"/>
        <q-separator class="q-my-lg"/>
        <connections/>
        <items/>
    </q-infinite-scroll>
</template>

<script setup lang="ts">
import Items from './Items.vue';
import Connections from './Connections.vue';
import StructureItem from './Structure.Item.vue';
import {useStructureStore} from "./structure_store";
import {watch} from "vue";
import {useStructureItemsStore} from "./structure_items_store";

const structureStore = useStructureStore();
const itemsStore = useStructureItemsStore();
const onLoadAsync = async (index: number, done: CallableFunction) => {
    console.log('>> onScroll')
    await itemsStore.getItemsAsync();
    done();
}
watch(() => structureStore.structureSelectedId, async (structureId: any, oldVal: any) => {
    if (!structureId) return;

    // console.log(structureId);
    // connectionsStore.drawActiveStructureConnections(structureId, oldVal);
    console.log('>> watcher')
    itemsStore.resetListRequest();
    await itemsStore.getItemsAsync();
}, {immediate: true})
</script>