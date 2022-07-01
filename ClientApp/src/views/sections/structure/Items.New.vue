<template>
    <modal title="Добавление объединения" v-model:is-show="formStore.isShowCreate">
        <q-form @submit.prevent="createAsync" id="structure-items-new-form">
            <q-card-section class="q-gutter-y-sm">
                <form-fields/>
            </q-card-section>
        </q-form>
        <q-card-actions align="right" class="q-pa-md">
            <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
            <q-btn form="structure-items-new-form" type="submit" label="Сохранить изменения" icon="las la-save" color="primary"/>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import FormFields from './Items.Form.Fields.vue';
import Modal from '../../components/Modal.vue';
import {useStructureItemFormStore} from "./structure_items_form_store";

const formStore = useStructureItemFormStore();
const createAsync = async () => {
    
}
</script>