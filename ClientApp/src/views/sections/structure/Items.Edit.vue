<template>
    <modal label="Изменение объединения" v-model:is-show="formStore.isShowEdit">
        <q-form @submit.prevent="editAsync" id="structure-items-edit-form">
            <q-card-section class="q-gutter-y-sm">
                <form-fields/>
            </q-card-section>
        </q-form>
        <q-card-actions align="between" class="q-pa-md">
            <div class="q-gutter-x-md">
                <soft-delete-actions
                    @delete="onDelete"
                    @softDelete="editAsync"
                    @recover="editAsync"
                    v-model="formStore.request"/>
            </div>
            <div class="q-gutter-x-md">
                <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
                <q-btn form="structure-items-edit-form" type="submit" label="Сохранить изменения" icon="las la-save" color="primary"/>
            </div>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import FormFields from './Items.Form.Fields.vue';
import Modal from '../../components/Modal.vue';
import SoftDeleteActions from '../../components/SoftDeleteActions.vue';
import {useStructureItemFormStore} from "./structure_items_form_store";
import {apiStructureItems} from "../../../api/rerources/api_structure_items";
import {useStructureItemsStore} from "./structure_items_store";

const formStore = useStructureItemFormStore();
const listStore = useStructureItemsStore();
const editAsync = async () => {
    const structureItemId = formStore.editId!;
    await apiStructureItems.update(structureItemId, formStore.request);
    const indexOf = listStore.items.findIndex(x => x.id === structureItemId);
    if (indexOf > -1) {
        listStore.items[indexOf] = await apiStructureItems.get(structureItemId);
    }
    formStore.$reset();
}
const onDelete = async () => {
    const structureItemId = formStore.editId!;
    await apiStructureItems.delete(structureItemId);
    const indexOf = listStore.items.findIndex(x => x.id === structureItemId);
    if (indexOf > -1) {
        listStore.items.splice(indexOf, 1)
    }
    
    formStore.$reset();
}
</script>