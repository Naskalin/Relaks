<template>
    <modal label="Изменение объединения" v-model:is-show="formStore.isShowEdit">
        <q-form @submit.prevent="editAsync" id="structure-items-edit-form">
            <q-card-section class="q-gutter-y-sm">
                <form-fields/>
            </q-card-section>
        </q-form>
        <q-card-actions align="right" class="q-pa-md">
            <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
            <q-btn form="structure-items-edit-form" type="submit" label="Сохранить изменения" icon="las la-save" color="primary"/>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import FormFields from './Items.Form.Fields.vue';
import Modal from '../../components/Modal.vue';
import {useStructureItemFormStore} from "./structure_items_form_store";
import {apiStructureItems} from "../../../api/rerources/api_structure_items";
import {useStructureItemsStore} from "./structure_items_store";

const formStore = useStructureItemFormStore();
const listStore = useStructureItemsStore();
const editAsync = async () => {
    const structureItemId = formStore.editId;
    await apiStructureItems.update(structureItemId, formStore.request);
    const indexOf = listStore.items.findIndex(x => x.id === structureItemId);
    if (indexOf > -1) {
        listStore.items[indexOf] = await apiStructureItems.get(structureItemId);
    }
    
    formStore.$reset();
}
</script>