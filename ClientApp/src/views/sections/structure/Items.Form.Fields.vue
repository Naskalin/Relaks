<template>
    <soft-delete-fields v-model="formStore.request"/>
    <entry-select-field
        :entry-selected-id="formStore.request.entryId"
        @onSelectEntry="val => formStore.request.entryId = val.id"
        :is-show-default="!formStore.request.entryId"
    />
    <date-field v-model="formStore.request.startAt" label="Существует с *" :with-time="false" required/>
    <q-input v-model="formStore.request.description" type="textarea" autogrow label="Описание" counter maxlength="250"/>
</template>

<script setup lang="ts">
import EntrySelectField from '../../fields/EntrySelect.Field.vue'
import SoftDeleteFields from '../../fields/SoftDeleteFields.vue';
import {useStructureItemFormStore} from "./structure_items_form_store";
import DateField from '../../fields/Date.Field.vue';

const formStore = useStructureItemFormStore();
</script>