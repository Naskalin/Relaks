<template>
    <q-form @submit.prevent="val => emit('save', val)">
        <q-input v-model="formStore.model.title" filled label="Название набора данных" autogrow counter maxlength="250"/>
        <custom-info-fields v-model="formStore.model.info" v-bind="$attrs"/>
    </q-form>
</template>

<script setup lang="ts">
import CustomInfoFields from '../../../components/custom_info/CustomInfoFields.vue';
import {useEntryInfoCustomFormStore} from "./entry_info_custom_form_store";

const formStore = useEntryInfoCustomFormStore();
const emit = defineEmits<{
    (e: 'save', val: any): void
}>();
</script>