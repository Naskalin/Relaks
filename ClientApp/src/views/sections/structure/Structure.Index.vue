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
                <q-card-section>
                    <div class="row q-col-gutter-md items-center q-mb-lg justify-between">
                        <div class="col-auto">
                            <h5 class="q-ma-none">Группы</h5>
                        </div>
                        <div class="col-auto q-gutter-sm">
                            <q-btn icon="las la-object-group" label="группа" color="primary" v-tooltip="'Добавить группу'"/>
                            <q-btn icon="las la-arrows-alt-h" label="связь" color="primary" v-tooltip="'Связать две группы'"/>
                        </div>
                    </div>
                    <tree :entry-id="entryId"/>
                </q-card-section>
            </template>

            <template v-slot:after>
                <q-card-section>
                    <div class="row q-col-gutter-md items-center q-mb-lg justify-between">
                        <div class="col-auto">
                            <h5 class="q-ma-none">Состав</h5>
                        </div>
                        <div class="col-auto q-gutter-sm">
                            <q-btn icon="las la-users" label="объединение" color="primary" v-tooltip="'Добавить объединение'"/>
                        </div>
                    </div>
                    <div v-if="structureStore.structureSelected" class="text-grey-9 q-mb-md text-center" style="font-size: 1rem">
                        <b class="text-blue-8">{{structureStore.structureSelected.title}}</b>
                    </div>
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
import {ref} from 'vue';
import {useRoute, onBeforeRouteLeave, onBeforeRouteUpdate} from "vue-router";
import Tree from './Structure.Tree.vue';
import Items from './Structure.Items.vue';
import Connections from './Structure.Connections.vue';
import {useStructureConnectionsStore} from "./structure_connections_store";

const entryId = (useRoute()).params.entryId as string;
const splitterModel = ref(70);
const profileStore = useEntryProfileStore();
const structureStore = useStructureStore();
const connectionsStore = useStructureConnectionsStore();

onBeforeRouteLeave((to, from, next) => {
    Object.keys(connectionsStore.connectionLines).forEach(key => {
        const line = connectionsStore.connectionLines[key];
        const startEl = line.start as Element;
        const endEl = line.end as Element;
        startEl.remove();
        endEl.remove();
        line.remove();
    })
    connectionsStore.$reset();
    next();
})
</script>