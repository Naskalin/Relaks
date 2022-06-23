<template>
    <div v-if="itemsStore.items.length" class="q-mt-lg">
        <p class="text-center text-h6 q-mb-md">Объединения</p>
        
        <div v-for="sItem in itemsStore.items" :key="sItem.id">
            <q-separator class="q-my-md"/>
            
            <q-item class="q-pa-none">
                <q-item-section side>
                    <q-avatar size="50px" color="grey-5">
                        <api-image v-if="sItem.entry.avatar" :file-id="sItem.entry.avatar" image-filter="square-thumbnail"/>
                        <q-icon v-else name="las la-question-circle" color="grey-4" size="34px"/>
                    </q-avatar>
                </q-item-section>
                <q-item-section>
                    <q-item-label>{{sItem.entry.name}}</q-item-label>
                    <div v-if="sItem.entry.description" style="font-size: .85rem" class="text-grey-8">{{sItem.entry.description}}</div>
                    <div v-if="sItem.comment" class="q-mt-sm">
                        <q-icon name="las la-comment"/> {{sItem.comment}}
                    </div>
                </q-item-section>
                <q-item-section side>
                    <q-btn color="primary" icon="las la-edit" round flat/>
                </q-item-section>
            </q-item>
        </div>
    </div>
</template>

<script setup lang="ts">
import {useStructureItemsStore} from "./structure_items_store";
import {useStructureStore} from "./structure_store";
import {watch} from "vue";
import ApiImage from '../../components/ApiImage.vue';
import {useStructureConnectionsStore} from "./structure_connections_store";

const itemsStore = useStructureItemsStore();
const structureStore = useStructureStore();
const connectionStore = useStructureConnectionsStore();
watch(() => structureStore.structureSelectedId, async (structureId: any, oldVal: any) => {
    if (!structureId) return;
    await itemsStore.getItemsAsync(structureId);
    connectionStore.drawActiveStructureConnections(structureId, oldVal);
}, {immediate: true})
</script>