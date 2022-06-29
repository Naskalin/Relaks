<template>
    <q-input v-model="store.request.title" type="text" label="Название" counter maxlength="250" required/>
    <q-input v-model="store.request.description" type="textarea" label="Описание" counter maxlength="500" autogrow/>
    <date-field v-model="store.request.startAt" label="Дата начала" :with-time="false" required/>
    <structure-select-field v-model="store.request.parentId" label="Родительская группа"/>
</template>

<script setup lang="ts">
import DateField from '../../fields/Date.Field.vue';
import StructureSelectField from './fields/Structure.Select.Field.vue';
import {useStructureFormStore} from "./structure_form_store";
import {onMounted, watch} from "vue";

const store = useStructureFormStore();
onMounted(() => {
    if (!store.request.startAt) store.request.startAt = (new Date()).toISOString();
})
watch(() => [store.isShowEdit, store.isShowCreate], (val: any) => {
    if (!val) {
        store.$reset();
    }
})
</script>