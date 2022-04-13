<template>
    <entry-form-modal v-model:is-show="isShowCreateForm"
                      v-model="createStore.model"
                      :is-loading="createStore.isLoading"
                      :is-create="true"
                      btn-title="Добавить"
                      @submit="createEntry"
                      title="Добавление объединения"/>
    
    <entry-list-table @row-dblclick="rowDoubleClick" @card-dblclick="rowDoubleClick">
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
import {ref} from 'vue';
import {useEntryCreateStore} from "../../../store/entry/entry.create.store";
import {useRouter} from 'vue-router'
import EntryListTable from './Entry.List.Table.vue';
import {Entry} from "../../../api/api_types";

// creating
const router = useRouter();
const createStore = useEntryCreateStore();
const isShowCreateForm = ref(false);
const createEntry = async () => {
    const entry = await createStore.createEntry();
    await router.push({name: 'entry-profile', params: {entryId: entry.id}});
    isShowCreateForm.value = false;
    createStore.$reset();
}

// list
const rowDoubleClick = async (entry: Entry) => {
    await router.push({name: 'entry-profile', params: {entryId: entry.id}});
}
</script>