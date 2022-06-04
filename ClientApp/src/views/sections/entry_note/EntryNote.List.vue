<template>
    note list
<!--    <div class="row justify-between items-center q-mb-md">-->
<!--        <h5 class="q-my-md">{{ entryMessages.profile.tabs['entry-note-list'] }}</h5>-->
<!--        <q-btn label="Добавить" @click="showCreateForm" color="primary" icon="las la-plus-circle"/>-->
<!--    </div>-->

<!--    <div class="q-gutter-y-md">-->
<!--        <q-btn-toggle-->
<!--            class="bg-grey-2"-->
<!--            v-model="listStore.listRequest.isDeleted"-->
<!--            toggle-color="secondary"-->
<!--            :options="[-->
<!--                {label: 'Все', value: null},-->
<!--                {label: 'Актуальные', value: false},-->
<!--                {label: 'Архивные', value: true},-->
<!--            ]"-->
<!--        />-->
<!--        <q-input v-model="listStore.listRequest.search" debounce="250" label="Поиск заметок" style="width: 500px"/>-->
<!--    </div>-->
<!--    <br>-->

<!--    <div v-if="listStore.notes.length" class="q-gutter-y-lg">-->
<!--        <entry-note-list-item v-for="eNote in listStore.notes"-->
<!--                              :e-note="eNote"-->
<!--                              :key="eNote.id"-->
<!--                              @showEditForm="showEditForm(eNote)"-->
<!--        ></entry-note-list-item>-->
<!--    </div>-->
<!--    <q-banner v-else rounded class="bg-grey-3">-->
<!--        К сожалению, ничего не найдено.-->
<!--    </q-banner>-->
<!--    -->
<!--    <entry-info-form-modal-->
<!--        v-model:is-show="isShowCreate"-->
<!--        v-model="createStore.Note"-->
<!--        :is-loading="createStore.isLoading"-->
<!--        entry-info-type="Note"-->
<!--        title="Добавление заметки"-->
<!--        btn-title="Добавить"-->
<!--        btn-icon="las la-plus-circle"-->
<!--        @submit="onSubmitCreateForm"-->
<!--        :is-create="true"-->
<!--    />-->

<!--    <entry-info-form-modal-->
<!--        v-if="editStore.Note.model"-->
<!--        v-model="editStore.Note.model"-->
<!--        v-model:is-show="isShowEdit"-->
<!--        entry-info-type="Note"-->
<!--        :is-loading="editStore.isLoading"-->
<!--        :is-create="false"-->
<!--        title="Изменение заметки"-->
<!--        btn-title="Сохранить"-->
<!--        btn-icon="las la-save"-->
<!--        @submit="onSubmitEditForm"-->
<!--        @delete="onDelete"-->
<!--        @softDelete="onSubmitEditForm"-->
<!--        @recover="onSubmitEditForm"-->
<!--    />-->
</template>

<script setup lang="ts">
// import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';
// import EntryNoteListItem from './EntryNote.List.Item.vue';
//
// import {useRoute} from "vue-router";
// import {entryMessages} from "../../../localize/messages";
// import {useEntryNoteListStore} from "../../../store/entry_notes/entryNote.list.store";
// import {onMounted, ref, watch} from "vue";
// import {useEntryInfoCreateStore} from "../../../store/entry_info/entryInfo.create.store";
// import {useEntryInfoEditStore} from "../../../store/entry_info/entryInfo.edit.store";
// import {InfoNote} from "../../../api/api_types";
// import {apiMappers} from "../../../api/api_mappers";
// import {apiEntryNote} from "../../../api/rerources/api_entry_info";
//
// const route = useRoute();
// const listStore = useEntryNoteListStore();
// const entryId = route.params.entryId as string;
//
// watch([() => listStore.listRequest.search, () => listStore.listRequest.isDeleted], async () => {
//     await listStore.getNotes(entryId);
// })
//
// onMounted(async () => {
//     await listStore.getNotes(entryId);
// })
//
// // edit
// const editStore = useEntryInfoEditStore();
// const isShowEdit = ref(false);
// const entryNoteEditable = ref<InfoNote | null>(null);
// const showEditForm = (entryNote: InfoNote) => {
//     editStore.Note.model = apiMappers.toEntryInfoFormRequest.Note(entryNote);
//     entryNoteEditable.value = entryNote;
//    
//     isShowEdit.value = true;
// }
// const onSubmitEditForm = async () => {
//     if (!entryNoteEditable.value) throw Error('onSubmitEditForm entryNoteEditable not found');
//     const entryNoteId = entryNoteEditable.value.id;
//     await editStore.update(entryId, entryNoteId, 'Note');
//     const eNoteUpdated = await apiEntryNote.get(entryId, entryNoteId);
//     listStore.notes[listStore.notes.findIndex(x => x.id === eNoteUpdated.id)] = eNoteUpdated;
//    
//     isShowEdit.value = false;
// }
//
// // delete
// const onDelete = async () => {
//     if (!entryNoteEditable.value) throw Error('onDelete entryNoteEditable not found');
//     const entryNoteId = entryNoteEditable.value.id;
//     await apiEntryNote.delete(entryId, entryNoteId);
//     listStore.notes.splice(listStore.notes.findIndex(x => x.id === entryNoteId), 1);
//    
//     isShowEdit.value = false;
// }
//
// // create
// const isShowCreate = ref(false);
// const createStore = useEntryInfoCreateStore();
// const showCreateForm = () => {
//     createStore.$reset();
//     isShowCreate.value = true;
// }
// const onSubmitCreateForm = async () => {
//     const eNote = await createStore.create(entryId, 'Note');
//     listStore.notes.unshift(eNote);
//     isShowCreate.value = false;
// }
</script>