<template>
    <modal title="Изменить группу" v-model:is-show="store.isShowEdit">
        <q-form @submit.prevent="editAsync" id="structure-edit-form">
            <q-card-section class="q-gutter-y-md">
                <structure-form-fields/>
            </q-card-section>
        </q-form>
        <q-card-actions align="right" class="q-pa-md">
            <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
            <q-btn form="structure-edit-form" type="submit" label="Сохранить изменения" icon="las la-save"
                   color="primary"/>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import Modal from '../../components/Modal.vue';
import {useRoute} from "vue-router";
import {useStructureFormStore} from "./structure_form_store";
import {apiStructure} from "../../../api/rerources/api_structure";
import StructureFormFields from './Structure.Form.Fields.vue';

const route = useRoute();
const entryId = route.params.entryId as string;
const emit = defineEmits<{
    (e: 'onSave'): void
}>()

const store = useStructureFormStore();
const editAsync = async () => {
    await apiStructure.update(entryId, store.editId!, store.request);
    emit('onSave');
    store.$reset();
}
</script>