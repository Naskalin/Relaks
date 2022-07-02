<template>
    <entry-form-modal v-model:is-show="isShowCreateForm"
                      v-model="createStore.model"
                      :is-loading="createStore.isLoading"
                      :is-create="true"
                      btn-title="Добавить"
                      @submit="createEntry"
                      label="Добавление объединения"/>

    <entry-list-table @row-dblclick="rowDoubleClick">
        <template v-slot:top-right>
            <q-btn icon="las la-plus-circle"
                   @click="isShowCreateForm = true"
                   label="Добавить"
                   color="primary"
            />
        </template>
    </entry-list-table>
</template>

<script setup lang="ts">
import EntryFormModal from './Entry.Form.Modal.vue';
import {ref, onMounted} from 'vue';
import {useEntryCreateStore} from "../../../store/entry/entry.create.store";
import {useRouter, onBeforeRouteLeave} from 'vue-router'
import EntryListTable from './Entry.List.Table.vue';
import {Entry} from "../../../api/api_types";
import {useEntryListStore} from "../../../store/entry/entry.list.table.store";
import {useLayoutStore} from "../../layouts/layout_store";
import EntryCard from '../entry/Entry.Card.vue';

// creating
const router = useRouter();
const createStore = useEntryCreateStore();
const isShowCreateForm = ref(false);
const entryListStore = useEntryListStore();
const layoutStore = useLayoutStore();
const createEntry = async () => {
    const entry = await createStore.createEntry();
    await router.push({name: 'entry-profile', params: {entryId: entry.id}});
    isShowCreateForm.value = false;
    createStore.$reset();
}
const rowDoubleClick = (entry: Entry) => {
    router.push({name: 'entry-profile', params: {entryId: entry.id}});
}
onBeforeRouteLeave((to, from, next) => {
    entryListStore.$reset();
    layoutStore.search = '';
    next();
})
</script>