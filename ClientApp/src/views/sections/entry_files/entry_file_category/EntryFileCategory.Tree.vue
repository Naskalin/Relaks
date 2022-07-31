<template>
    
    {{categoryStore.selectedCategory}}
    
    <q-tree
        ref="treeEl"
        :nodes="categoryStore.tree"
        :duration="0"
        node-key="id"
        label-key="data/title"
        selected-color="primary"
        default-expand-all
        no-selection-unset
        v-model:selected="categoryStore.selectedId"
    >
        <template v-slot:default-header="prop">
            {{prop.node.data.title}}
        </template>
    </q-tree>
</template>

<script setup lang="ts">
import {useEntryFileCategoriesStore} from "./entry_file_categories_store";
import {onMounted} from "vue";

const props = defineProps<{
    entryId: string
}>();

const categoryStore = useEntryFileCategoriesStore();
onMounted(async () => {
    categoryStore.listRequest.entryId = props.entryId;
    await categoryStore.getItems();
});
</script>