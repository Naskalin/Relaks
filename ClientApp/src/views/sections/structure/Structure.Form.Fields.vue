<template>
    <soft-delete-fields v-model="store.request"/>
    <q-input v-model="store.request.title" type="text" label="Название" counter maxlength="250" required/>
    <q-input v-model="store.request.description" type="textarea" label="Описание" counter maxlength="500" autogrow/>
    <date-field v-model="store.request.startAt" label="Существует с" :with-time="false" required/>
    <structure-select-field v-model="store.request.parentId" label="Родительская группа"/>
</template>

<script setup lang="ts">
import DateField from '../../fields/Date.Field.vue';
import StructureSelectField from './fields/Structure.Select.Field.vue';
import {useStructureFormStore} from "./structure_form_store";
import {onMounted, watch} from "vue";
import SoftDeleteFields from '../../fields/SoftDeleteFields.vue';
import {dateHelper} from "../../../utils/date_helper";

const store = useStructureFormStore();
onMounted(() => {
    if (!store.request.startAt) store.request.startAt = dateHelper.startOfDayISO();
})
watch(() => [store.isShowEdit, store.isShowCreate], (val: any) => {
    if (!val) {
        store.$reset();
    }
})
</script>