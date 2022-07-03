<template>
    <q-infinite-scroll
        v-if="structureStore.list.length && structureStore.structureSelected"
        @load="onLoadAsync"
        :disable="itemsStore.isEnd"
        :offset="250"
    >
        <structure-item :structure="structureStore.structureSelected" class="q-mb-xl"/>
        
        <connections/>
        <items/>

        <template v-slot:loading>
            <div class="row justify-center q-my-md">
                <q-spinner-dots color="primary" size="40px" />
            </div>
        </template>
    </q-infinite-scroll>
</template>

<script setup lang="ts">
import Items from './Items.vue';
import Connections from './Connections.vue';
import StructureItem from './Structure.Item.vue';
import {useStructureStore} from "./structure_store";
import {watch} from "vue";
import {useStructureItemsStore} from "./structure_items_store";
import {emitter} from "../../event_bus";

const structureStore = useStructureStore();
const itemsStore = useStructureItemsStore();
const onLoadAsync = async (index: number, done: CallableFunction) => {
    await itemsStore.getItemsAsync();
    done();
}
watch(() => structureStore.structureSelectedId, async (structureId: any, oldVal: any) => {
    if (!structureId) return;
    emitter.emit('rightSidebarScrollTop');
    itemsStore.resetListRequest();
    await itemsStore.getItemsAsync();
}, {immediate: true})
</script>