<template>
    <q-input v-model="model.title" type="text" counter maxlength="150"/>
    <q-input v-model="model.description" type="textarea" maxlength="250"/>
    <date-field v-model="model.startAt" label="Дата создания" :with-time="false" required/>
<!--    <structure-select-field v-model="model.parentId"/>-->
</template>

<script setup lang="ts">
import DateField from '../../fields/Date.Field.vue';
// import StructureSelectField from './fields/Structure.Select.Field.vue';
import {StructureFormRequest} from "../../../api/rerources/api_structure";

import {computed} from 'vue';

const props = defineProps<{
    modelValue: StructureFormRequest,
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: StructureFormRequest): void,
}>()
const model = computed({
    get: () => props.modelValue,
    set: (val: StructureFormRequest) => emit('update:modelValue', val)
});
</script>