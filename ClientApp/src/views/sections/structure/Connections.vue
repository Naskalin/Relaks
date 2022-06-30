<template>
    <new-modal/>
    <edit-modal/>
    
    <div class="row q-col-gutter-md items-center justify-between">
        <div class="col-auto">
            <h6 class="q-ma-none">Связи</h6>
        </div>
        <div class="col-auto q-gutter-sm">
            <q-btn
                @click="showNewModal"
                icon="las la-exchange-alt"
                round color="primary"
                v-tooltip.left="'Добавить связь'"/>
        </div>
    </div>
    <div class="q-gutter-y-md q-mt-sm" v-if="connectionsStore.structureConnections.length">
        <q-list v-if="structureStore.structureSelectedId" bordered class="rounded-borders">
            <connections-item
                v-for="connection in connectionsStore.structureConnections"
                :connection="connection"
                :key="connection.id"/>
        </q-list>
    </div>
</template>

<script setup lang="ts">
import {useStructureConnectionsStore} from "./structure_connections_store";
import {watch, onMounted} from 'vue';
import {useStructureStore} from "./structure_store";
import ConnectionsItem from './Connections.Item.vue';
import NewModal from './Connections.New.Modal.vue';
import EditModal from './Connections.Edit.Modal.vue';
import {useStructureConnectionsFormStore} from "./structure_connections_form_store";
import {useRoute} from "vue-router";
import {dateHelper} from "../../../utils/date_helper";

const structureStore = useStructureStore();
const connectionsStore = useStructureConnectionsStore();
const formStore = useStructureConnectionsFormStore();
const entryId = (useRoute()).params.entryId as string;

onMounted(() => {
    setTimeout(() => {
        // load first connection
        connectionsStore.drawActiveStructureConnections(structureStore.structureSelectedId, null);  
    }, 1000)
})
watch(() => connectionsStore.activeConnectionId, (val: string | null, oldVal: string | null) => {
    connectionsStore.drawActiveConnection(oldVal);
})
const showNewModal = () => {
    formStore.$reset();
    formStore.request.structureFirstId = structureStore.structureSelectedId!;
    formStore.request.startAt = dateHelper.startOfDay();
    formStore.isShowCreate = true;
}
</script>