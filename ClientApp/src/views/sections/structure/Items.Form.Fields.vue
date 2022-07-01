<template>
    <soft-delete-fields v-model="formStore.request"/>
    <q-input v-model="formStore.request.description" type="textarea" autogrow label="Описание" counter maxlength="250"/>
    <date-field v-model="formStore.request.startAt" label="Существует с" :with-time="false" required/>
</template>

<script setup lang="ts">
import SoftDeleteFields from '../../fields/SoftDeleteFields.vue';
import {useStructureItemFormStore} from "./structure_items_form_store";
import DateField from '../../fields/Date.Field.vue';

const formStore = useStructureItemFormStore();
</script>