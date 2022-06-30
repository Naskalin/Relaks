<template>
    <modal title="Добавление связи" v-model:is-show="store.isShowCreate">
        <q-form @submit.prevent="createAsync" id="structure-connection-new-form">
            <q-card-section class="q-gutter-y-sm">
                <form-fields/>
            </q-card-section>
        </q-form>
        <q-card-actions align="right" class="q-pa-md">
            <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
            <q-btn form="structure-connection-new-form" type="submit" label="Добавить новую" icon="las la-plus-circle" color="primary"/>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import Modal from '../../components/Modal.vue';
import FormFields from './Connections.Form.Fields.vue';
import {useStructureConnectionsFormStore} from "./structure_connections_form_store";
import {apiStructureConnection} from "../../../api/rerources/api_structure_connections";
import {useStructureConnectionsStore} from "./structure_connections_store";
import {useStructureStore} from "./structure_store";

const emit = defineEmits<{
    (e: 'onSave'): void
}>()
const store = useStructureConnectionsFormStore();
const connectionsStore = useStructureConnectionsStore();
const structureStore = useStructureStore();

const createAsync = async () => {
    const connection = await apiStructureConnection.create(store.request);
    emit('onSave');
    store.$reset();
    
    connectionsStore.connections.push(connection);
    connectionsStore.drawConnections([connection]);
    setTimeout(() => {
        connectionsStore.drawActiveStructureConnections(structureStore.structureSelectedId!);
        connectionsStore.activeConnectionId = connection.id;
        connectionsStore.drawActiveConnection();
    }, 200)
}
</script>