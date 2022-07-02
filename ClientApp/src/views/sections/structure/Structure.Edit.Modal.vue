<template>
    <modal label="Изменить группу" v-model:is-show="store.isShowEdit">
        <q-form @submit.prevent="editAsync" id="structure-edit-form">
            <q-card-section class="q-gutter-y-sm">
                <structure-form-fields/>
            </q-card-section>
        </q-form>
        <q-card-actions align="between" class="q-pa-md">
            <div class="q-gutter-x-md">
                <soft-delete-actions
                    @delete="onDelete"
                    @softDelete="editAsync"
                    @recover="editAsync"
                    v-model="store.request"/>
            </div>
            <div class="q-gutter-x-md">
                <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
                <q-btn form="structure-edit-form" type="submit" label="Сохранить изменения" icon="las la-save"
                       color="primary"/>
            </div>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import Modal from '../../components/Modal.vue';
import {useRoute} from "vue-router";
import {useStructureFormStore} from "./structure_form_store";
import {apiStructure} from "../../../api/rerources/api_structure";
import StructureFormFields from './Structure.Form.Fields.vue';
import SoftDeleteActions from '../../components/SoftDeleteActions.vue';

const route = useRoute();
const entryId = route.params.entryId as string;
const emit = defineEmits<{
    (e: 'onUpdate'): void
}>()

const store = useStructureFormStore();
const editAsync = async () => {
    await apiStructure.update(entryId, store.editId!, store.request);
    emit('onUpdate');
    store.$reset();
}
const onDelete = async () => {
    await apiStructure.delete(entryId, store.editId!);
    emit('onUpdate');
    store.$reset();
}
</script>