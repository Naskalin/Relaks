<template>
    <modal title="Изменение связи" v-model:is-show="store.isShowEdit">
        <q-form @submit.prevent="editAsync" id="structure-connection-edit-form">
            <q-card-section class="q-gutter-y-md">
                <form-fields/>
            </q-card-section>
        </q-form>
        <q-card-actions align="right" class="q-pa-md">
            <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
            <q-btn form="structure-connection-edit-form" type="submit" label="Сохранить изменения" icon="las la-save" color="primary"/>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import FormFields from './Connections.Form.Fields.vue';
import Modal from '../../components/Modal.vue';
import {useStructureConnectionsFormStore} from "./structure_connections_form_store";
import {apiStructureConnection} from "../../../api/rerources/api_structure_connections";
import {useStructureStore} from "./structure_store";
import {useStructureConnectionsStore} from "./structure_connections_store";

const store = useStructureConnectionsFormStore();
const structureStore = useStructureStore();
const connectionsStore = useStructureConnectionsStore();

const emit = defineEmits<{
    (e: 'onSave'): void
}>()
const editAsync = async () => {
    const connectionId = store.editId!;
    await apiStructureConnection.update(connectionId, store.request);
    emit('onSave');
    store.$reset();
    
    const line = connectionsStore.connectionLines[connectionId] || null;
    if (!line) throw Error('LeaderLine для перерисовки не найден');
    connectionsStore.deleteLine(line);
    const connection = await apiStructureConnection.get(connectionId);
    const indexOf = connectionsStore.connections.findIndex(x => x.id === connectionId);
    if (indexOf > -1) {
        connectionsStore.connections[indexOf] = connection;
    }
    
    connectionsStore.drawConnections([connection]);
    setTimeout(() => {
        connectionsStore.drawActiveStructureConnections(structureStore.structureSelectedId!);
        connectionsStore.activeConnectionId = connectionId;
        connectionsStore.drawActiveConnection();
    }, 200)
}
</script>