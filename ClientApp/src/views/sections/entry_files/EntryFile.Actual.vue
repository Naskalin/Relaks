<template>
    <q-btn-toggle
        class="bg-grey-2"
        v-model="isDeleted"
        toggle-color="secondary"
        :options="[
                {label: 'Все', value: null},
                {label: 'Актуальные', value: false},
                {label: 'Архивные', value: true},
            ]"
    />
</template>

<script setup lang="ts">
import {useFileListTableStore} from "../../../store/entryFile/entryFile.list.table.store";
import {useEntryFileCategoriesStore} from "./entry_file_category/entry_file_categories_store";
import {ref, watch} from 'vue';

const isDeleted = ref(false);
const fileListStore = useFileListTableStore();
const categoryListStore = useEntryFileCategoriesStore();
watch(() => isDeleted.value, val => {
    fileListStore.listRequest.isDeleted = val;
    categoryListStore.listRequest.isDeleted = val;
})
</script>