<template>
    <modal title="Добавление группы" v-model:is-show="store.isShowCreate">
        <q-form @submit.prevent="createAsync" id="structure-new-form">
            <q-card-section class="q-gutter-y-sm">
                <structure-form-fields/>
            </q-card-section>
        </q-form>
        <q-card-actions align="right" class="q-pa-md">
            <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
            <q-btn form="structure-new-form" type="submit" label="Добавить новую" icon="las la-plus-circle" color="primary"/>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import StructureFormFields from './Structure.Form.Fields.vue';
import {useRoute} from "vue-router";
import {useStructureFormStore} from "./structure_form_store";
import Modal from '../../components/Modal.vue';
import {apiStructure} from "../../../api/rerources/api_structure";

const emit = defineEmits<{
    (e: 'onSave', structureId: string): void
}>()
const store = useStructureFormStore();
const entryId = (useRoute()).params.entryId as string;
const createAsync = async () => {
    const structure = await apiStructure.create(entryId, store.request);
    emit('onSave', structure.id);
    store.$reset();
}
</script>