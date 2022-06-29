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
                @click="structureFormStore.isShowCreate = true"
            />
        </div>
    </div>
    
    <q-card class="q-mt-lg">
        <q-splitter
            v-model="splitterModel"
            :limits="[50, 70]"
        >
            <template v-slot:before>
                <q-card-section>
                    <tree :entry-id="entryId"/>
                </q-card-section>
            </template>

            <template v-slot:after>
                <q-card-section v-if="structureStore.list.length">
                    <div class="q-mb-lg row q-col-gutter-md items-center" v-if="structureStore.structureSelected">
                        <div class="col q-gutter-y-sm">
                            <div class="text-h6">{{structureStore.structureSelected.title}}</div>
                            <div v-if="structureStore.structureSelected.description" class="text-grey-9">
                                <q-icon class="q-mr-xs" name="las la-comment"/>
                                {{structureStore.structureSelected.description}}
                            </div>
                            <div>
                                <small class="label-caption text-grey-7 q-mr-sm">Дата начала</small>
                                <time>{{dateHelper.utcFormat(structureStore.structureSelected.startAt)}}</time>
                            </div>
                        </div>
                        <div class="col-auto q-gutter-y-sm">
                            <div>
                                <q-btn
                                    color="primary"
                                    outline
                                    icon="las la-edit"
                                    round
                                    @click="showEditModal"
                                    v-tooltip.left="'Изменить группу'"/>
                            </div>
                            <div>
                                <q-btn
                                    color="primary"
                                    icon="las la-sitemap"
                                    v-tooltip.left="'Добавить группу в эту группу'"
                                    @click="showCreateModalForSelectedStructure"
                                    round/>
                            </div>
                        </div>
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
import {useStructureConnectionsStore} from "./structure_connections_store";
import {useStructureItemsStore} from "./structure_items_store";
import {debounce} from "quasar";
import {entryMessages} from "../../../localize/messages";
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {useStructureFormStore} from "./structure_form_store";
import {dateHelper} from "../../../utils/date_helper";

const structureFormStore = useStructureFormStore();
const profileStore = useEntryProfileStore();
const entryId = (useRoute()).params.entryId as string;
const splitterModel = ref(70);
const structureStore = useStructureStore();
const connectionsStore = useStructureConnectionsStore();
const itemsStore = useStructureItemsStore();

watch(() => splitterModel.value, debounce(() => {
    connectionsStore.deleteConnections();
    connectionsStore.isShowLines = true; 
    connectionsStore.drawConnections(connectionsStore.connections);
    if (structureStore.structureSelectedId) {
        connectionsStore.drawActiveStructureConnections(structureStore.structureSelectedId, null);
    }
}, 150))

// const deleteConnections = () => {
//     Object.keys(connectionsStore.connectionLines).forEach(key => {
//         const line = connectionsStore.connectionLines[key];
//         const startEl = line.start as Element;
//         const endEl = line.end as Element;
//         startEl.remove();
//         endEl.remove();
//         line.remove();
//     })
// }
const showEditModal = () => {
    structureFormStore.$reset();
    structureFormStore.editId = structureStore.structureSelected!.id;
    structureFormStore.request = Object.assign({}, structureStore.structureSelected!);
    structureFormStore.isShowEdit = true
}
const showCreateModalForSelectedStructure = () => {
    structureFormStore.$reset();
    structureFormStore.request.parentId = structureStore.structureSelected!.id;
    structureFormStore.isShowCreate = true
}
onBeforeRouteLeave((to, from, next) => {
    connectionsStore.deleteConnections();
    connectionsStore.$reset();
    structureStore.$reset();
    itemsStore.$reset();
    next();
})
</script>