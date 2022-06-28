<template>
    <q-tree
        :nodes="structureStore.tree"
        node-key="id"
        v-model:selected="structureId"
        :default-expand-all="true"
        no-selection-unset
    />
</template>

<script setup lang="ts">
import {StructureFormRequest, StructureTree, Structure} from "../../../../api/rerources/api_structure";
import {computed} from "vue";
import {useStructureStore} from "../structure_store";

const props = defineProps<{
    modelValue: any
}>()
const structureStore = useStructureStore();
const emit = defineEmits<{
    (e: 'update:modelValue', val: any): void,
}>()
const structureId = computed({
    get: () => props.modelValue,
    set: (val: any) => emit('update:modelValue', val)
});
</script>
