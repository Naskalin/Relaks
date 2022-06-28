<template>
<!--    <structure-form-fields   />-->
    new {{entryId}}
    
</template>

<script setup lang="ts">
import StructureFormFields from  './Structure.Form.Fields.vue';
import {useRoute} from "vue-router";
import {onMounted} from "vue";
import {useStructureStore} from "./structure_store";

const structureStore = useStructureStore();
onMounted(() => {
    structureStore.$reset()
})
const entryId = (useRoute()).params.entryId as string;
const props = defineProps<{
    
}>()
</script>