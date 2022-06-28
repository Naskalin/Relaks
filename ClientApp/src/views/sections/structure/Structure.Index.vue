<template>
    <h4 v-if="profileStore.entry" class="q-my-lg">
        {{ entryMessages.profile.tabs(profileStore.entry.entryType)["entry-structures"] || '?'}}
    </h4>
    
    <q-card>
        <q-splitter
            v-model="splitterModel"
            :limits="[50, 70]"
        >
            <template v-slot:before>
                <router-view/>
                
                <q-card-section>
                    <div class="row q-col-gutter-md items-center q-mb-lg justify-between">
                        <div class="col-auto">
                            <h6 class="q-ma-none">Группы</h6>
                        </div>
                        <div class="col-auto q-gutter-sm">
                            <q-btn 
                                @click="$router.push({name: 'entry-structures-create', params: {entryId: entryId}})" 
                                icon="las la-sitemap" 
                                label="Добавить группу" 
                                color="primary"
                            />
                        </div>
                    </div>
                    <tree :entry-id="entryId"/>
                </q-card-section>
            </template>

            <template v-slot:after>
                <q-card-section v-if="structureStore.list.length">
                    <div class="text-grey-9 q-mb-md text-center text-h6 text-grey-9"
                         v-if="structureStore.structureSelected">{{structureStore.structureSelected.title}}</div>
                    <connections/>
                    <items/>
                </q-card-section>
            </template>
        </q-splitter>
    </q-card>
</template>

<script setup lang="ts">
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {useStructureStore} from "./structure_store";
import {entryMessages} from "../../../localize/messages";
import {ref, watch} from 'vue';
import {useRoute, onBeforeRouteLeave} from "vue-router";
import Tree from './Structure.Tree.vue';
import Items from './Items.vue';
import Connections from './Connections.vue';
import {useStructureConnectionsStore} from "./structure_connections_store";
import {useStructureItemsStore} from "./structure_items_store";
import {debounce} from "quasar";

const entryId = (useRoute()).params.entryId as string;
const splitterModel = ref(70);
const profileStore = useEntryProfileStore();
const structureStore = useStructureStore();
const connectionsStore = useStructureConnectionsStore();
const itemsStore = useStructureItemsStore();

watch(() => splitterModel.value, debounce(() => {
    deleteConnections();
    connectionsStore.isShowLines = true; 
    connectionsStore.drawConnections(connectionsStore.connections);
    if (structureStore.structureSelectedId) {
        connectionsStore.drawActiveStructureConnections(structureStore.structureSelectedId, null);
    }
}, 150))

const deleteConnections = () => {
    Object.keys(connectionsStore.connectionLines).forEach(key => {
        const line = connectionsStore.connectionLines[key];
        const startEl = line.start as Element;
        const endEl = line.end as Element;
        startEl.remove();
        endEl.remove();
        line.remove();
    })
}
onBeforeRouteLeave((to, from, next) => {
    deleteConnections();
    connectionsStore.$reset();
    structureStore.$reset();
    itemsStore.$reset();
    next();
})
</script>