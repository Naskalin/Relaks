<template>
    {{formStore.request}}
    <new-structure-item/>
</template>

<script setup lang="ts">
import NewStructureItem from '../structure/Items.New.vue';
import {onMounted, computed, ref} from "vue";
import {useStructureFormStore} from "../structure/structure_form_store";
const formStore = useStructureFormStore()
</script>
