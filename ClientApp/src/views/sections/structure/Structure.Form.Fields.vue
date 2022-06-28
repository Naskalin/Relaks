<template>
    <q-input v-model="store.request.title" type="text" label="Название" counter maxlength="150" required/>
    <q-input v-model="store.request.description" type="textarea" label="Описание" counter maxlength="250" autogrow/>
    <date-field v-model="store.request.startAt" label="Дата создания" :with-time="false" required/>
    <structure-select-field :entry-id="entryId" v-model="store.request.parentId" label="Родительская группа"/>
</template>

<script setup lang="ts">
import DateField from '../../fields/Date.Field.vue';
import StructureSelectField from './fields/Structure.Select.Field.vue';
import {useStructureFormStore} from "./structure_form_store";
import {useRoute} from "vue-router";
import {onMounted, watch} from "vue";

const entryId = (useRoute()).params.entryId as string;
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