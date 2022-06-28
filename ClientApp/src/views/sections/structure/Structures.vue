<template>
    <h4 v-if="profileStore.entry" class="q-mt-lg q-mb-none">
        {{ entryMessages.profile.tabs(profileStore.entry.entryType)["entry-structures"] || '?'}}
    </h4>
    
    <structure-new-modal @onSave="resetTree"/>
    <structure-edit-modal @onSave="resetTree"/>
    
    <q-card class="q-mt-lg">
        <q-splitter
            v-model="splitterModel"
            :limits="[50, 70]"
        >
            <template v-slot:before>
                <q-card-section>
                    <div class="row q-col-gutter-md items-center q-mb-lg justify-between">
                        <div class="col-auto">
                            <h6 class="q-ma-none">Группы</h6>
                        </div>
                        <div class="col-auto q-gutter-sm">
                            <q-btn 
                                icon="las la-sitemap" 
                                label="Добавить группу" 
                                color="primary"
                                @click="structureFormStore.isShowCreate = true"
                            />
                        </div>
                    </div>
                    <tree :entry-id="entryId"/>
                </q-card-section>
            </template>

            <template v-slot:after>
                <q-card-section v-if="structureStore.list.length">
                    <div class="q-mb-md" v-if="structureStore.structureSelected">
                        <div class="text-center text-h6">{{structureStore.structureSelected.title}}</div>
                        <div v-if="structureStore.structureSelected.description" class="q-mt-sm text-grey-9">{{structureStore.structureSelected.description}}</div>
                    </div>
                    <connections/>
                    <items/>
                </q-card-section>
            </template>
        </q-splitter>
    </q-card>
</template>

<script setup lang="ts">
import {useStructureStore} from "./structure_store";
import {ref, watch} from 'vue';
import {useRoute, onBeforeRouteLeave} from "vue-router";
import Tree from './Structure.Tree.vue';
import Items from './Items.vue';
import Connections from './Connections.vue';
import StructureNewModal from './Structure.New.Modal.vue';
import StructureEditModal from './Structure.Edit.Modal.vue';
import {useStructureConnectionsStore} from "./structure_connections_store";
import {useStructureItemsStore} from "./structure_items_store";
import {debounce} from "quasar";
import {entryMessages} from "../../../localize/messages";
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {useStructureFormStore} from "./structure_form_store";

const structureFormStore = useStructureFormStore();
const profileStore = useEntryProfileStore();
const entryId = (useRoute()).params.entryId as string;
const splitterModel = ref(70);
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

const resetTree = async () => {
    deleteConnections();
    await structureStore.getStructuresAsync(entryId);
    connectionsStore.isShowLines = true;
    connectionsStore.drawConnections(connectionsStore.connections);
    if (structureStore.structureSelectedId) {
        connectionsStore.drawActiveStructureConnections(structureStore.structureSelectedId, null);
    }
}
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