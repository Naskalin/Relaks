<template>
    <div class="row q-gutter-x-lg items-center q-mb-md">
        <div class="col-auto">
            <h5 class="q-my-md">{{ entryMessages.profile.tabs[route.name] }}</h5>
        </div>
        <div class="col flex justify-center">
            <q-input v-model="listStore.listRequest.search" label="Поиск заметок" style="width: 500px"/>
        </div>
<!--        <div class="col-auto">-->
<!--            <q-btn label="Добавить" @click="showCreateForm" color="secondary" icon="las la-plus-circle"/>-->
<!--        </div>-->
    </div>

    <br>
    
    <div v-if="listStore.notes.length" class="q-gutter-y-lg">
        <entry-note-list-item v-for="eText in listStore.notes"
                              :e-text="eText"
                              :key="eText.id"
        ></entry-note-list-item>
<!--                              @showEditForm="showEditForm(eText)"-->
    </div>
    <div v-else>
        Ничего не найдено
    </div>
    
<!--    <entry-text-form-modal v-model:is-show="isShowCreate"-->
<!--                           v-model="createStore.model"-->
<!--                           :is-loading="createStore.isLoading"-->
<!--                           title="Добавление заметки"-->
<!--                           btn-title="Добавить"-->
<!--                           btn-icon="las la-plus-circle"-->
<!--                           @submit="onSubmitCreateForm"-->
<!--                           :is-create="true"-->
<!--    />-->

<!--    <entry-text-form-modal v-model:is-show="isShowEdit"-->
<!--                           v-model="editStore.model"-->
<!--                           :is-loading="editStore.isLoading"-->
<!--                           title="Изменение заметки"-->
<!--                           btn-title="Сохранить"-->
<!--                           btn-icon="las la-save"-->
<!--                           @submit="onSubmitEditForm"-->
<!--                           @delete="onDelete"-->
<!--                           :is-create="false"/>-->
</template>

<script setup lang="ts">
// import EntryTextFormModal from '../entry_text/EntryText.Form.Modal.vue';
import EntryNoteListItem from './EntryNote.List.Item.vue';

import {useRoute} from "vue-router";
import {entryMessages} from "../../../localize/messages";
// import {useEntryNoteListStore} from "../../../store/entry_notes/entryText.note.list.store";
import {useEntryNoteListStore} from "../../../store/entry_notes/entryNote.list.store";
import {computed, onMounted, ref, watch} from "vue";
// import {apiMappers} from "../../../api/api_mappers";
// import {useEntryTextEditStore} from "../../../store/entry_text/entryText.edit.store";
// import {EntryNote, EntryText} from "../../../api/api_types";
// import {apiEntryText} from "../../../api/rerources/api_entry_text";
// import {useEntryTextCreateStore} from "../../../store/entry_text/entryText.create.store";

const route = useRoute();
const listStore = useEntryNoteListStore();
const entryId = route.params.entryId as string;

// watch(() => listStore.listRequest.search, async (val) => {
//     await listStore.getNotes(entryId);
// })

onMounted(async () => {
    await listStore.getNotes(entryId);
})

// edit
// const editStore = useEntryTextEditStore();
// const isShowEdit = ref(false);
// const eInfoEditable = ref<EntryNote | null>(null);
// const showEditForm = (eText: EntryText) => {
//     editStore.model = apiMappers.toEntryNoteFormRequest(eText);
//     isShowEdit.value = true;
//     eInfoEditable.value = eText;
// }
// const onSubmitEditForm = async () => {
//     if (!eInfoEditable.value) throw Error('eTextEditable not found');
//     const eTextId = eInfoEditable.value.id;
//    
//     await editStore.update(entryId, eTextId);
//     const eTextUpdated = await apiEntryText.get(entryId, eTextId);
//     listStore.notes[listStore.notes.findIndex(x => x.id === eTextUpdated.id)] = eTextUpdated;
//    
//     isShowEdit.value = false;
// }

// delete
// const onDelete = async () => {
//     if (!eInfoEditable.value) throw Error('eTextEditable not found');
//
//     const eTextId = eInfoEditable.value.id;
//    
//     await apiEntryText.delete(entryId, eTextId);
//     listStore.notes.splice(listStore.notes.findIndex(x => x.id === eTextId), 1);
//     isShowEdit.value = false;
// }

// create
// const isShowCreate = ref(false);
// const createStore = useEntryTextCreateStore();
// const showCreateForm = () => {
//     createStore.$reset();
//     createStore.model.textType = 'Note';
//     createStore.model.actualStartAt = new Date().toISOString();
//     isShowCreate.value = true;
// }
//
// const onSubmitCreateForm = async () => {
//     const eText = await createStore.create(entryId);
//     listStore.notes.unshift(eText);
//     isShowCreate.value = false;
// }
</script>