<template>
    <div class="row items-center justify-between q-my-lg">
        <div class="col-auto">
            <h4 v-if="profileStore.entry" class="q-my-none">
                {{ entryMessages.profile.tabs(profileStore.entry.entryType)["entry-structures"] || '?'}}
            </h4>
        </div>
        <div class="col-auto">
            <q-btn
                icon="las la-sitemap"
                label="Добавить группу"
                color="primary"
                @click="showNewModal"
            />
        </div>
    </div>
    
    <q-card>
        <q-card-section>
            <tree :entry-id="entryId"/>
        </q-card-section>
    </q-card>
</template>

<script setup lang="ts">
import {useStructureStore} from "./structure_store";
import {ref, watch, onMounted} from 'vue';
import {useRoute, onBeforeRouteLeave} from "vue-router";
import Tree from './Structure.Tree.vue';
import {useStructureConnectionsStore} from "./structure_connections_store";
import {useStructureItemsStore} from "./structure_items_store";
import {debounce} from "quasar";
import {entryMessages} from "../../../localize/messages";
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {useStructureFormStore} from "./structure_form_store";
import {useLayoutStore} from "../../layouts/layout_store";

const structureFormStore = useStructureFormStore();
const profileStore = useEntryProfileStore();
const layoutStore = useLayoutStore();
const entryId = (useRoute()).params.entryId as string;
const splitterModel = ref(70);
const structureStore = useStructureStore();
const connectionsStore = useStructureConnectionsStore();
const itemsStore = useStructureItemsStore();

const showNewModal = () => {
    structureFormStore.$reset();
    structureFormStore.isShowCreate = true
}
watch(() => splitterModel.value, debounce(() => {
    connectionsStore.deleteConnections();
    connectionsStore.isShowLines = true; 
    connectionsStore.drawConnections(connectionsStore.connections);
    if (structureStore.structureSelectedId) {
        connectionsStore.drawActiveStructureConnections(structureStore.structureSelectedId, null);
    }
}, 150))
onMounted(() => {
    layoutStore.isRightSidebar = true;
})
onBeforeRouteLeave((to, from, next) => {
    connectionsStore.deleteConnections();
    connectionsStore.$reset();
    structureStore.$reset();
    itemsStore.$reset();
    layoutStore.isRightSidebar = false;
    next();
})
</script>