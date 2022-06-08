<!--<template>-->
<!--    note list-->
<!--&lt;!&ndash;    <div class="row justify-between items-center q-mb-md">&ndash;&gt;-->
<!--&lt;!&ndash;        <h5 class="q-my-md">{{ entryMessages.profile.tabs['entry-note-list'] }}</h5>&ndash;&gt;-->
<!--&lt;!&ndash;        <q-btn label="Добавить" @click="showCreateForm" color="primary" icon="las la-plus-circle"/>&ndash;&gt;-->
<!--&lt;!&ndash;    </div>&ndash;&gt;-->

<!--&lt;!&ndash;    <div class="q-gutter-y-md">&ndash;&gt;-->
<!--&lt;!&ndash;        <q-btn-toggle&ndash;&gt;-->
<!--&lt;!&ndash;            class="bg-grey-2"&ndash;&gt;-->
<!--&lt;!&ndash;            v-model="listStore.listRequest.isDeleted"&ndash;&gt;-->
<!--&lt;!&ndash;            toggle-color="secondary"&ndash;&gt;-->
<!--&lt;!&ndash;            :options="[&ndash;&gt;-->
<!--&lt;!&ndash;                {label: 'Все', value: null},&ndash;&gt;-->
<!--&lt;!&ndash;                {label: 'Актуальные', value: false},&ndash;&gt;-->
<!--&lt;!&ndash;                {label: 'Архивные', value: true},&ndash;&gt;-->
<!--&lt;!&ndash;            ]"&ndash;&gt;-->
<!--&lt;!&ndash;        />&ndash;&gt;-->
<!--&lt;!&ndash;        <q-input v-model="listStore.listRequest.search" debounce="250" label="Поиск заметок" style="width: 500px"/>&ndash;&gt;-->
<!--&lt;!&ndash;    </div>&ndash;&gt;-->
<!--&lt;!&ndash;    <br>&ndash;&gt;-->

<!--&lt;!&ndash;    <div v-if="listStore.notes.length" class="q-gutter-y-lg">&ndash;&gt;-->
<!--&lt;!&ndash;        <entry-note-list-item v-for="eNote in listStore.notes"&ndash;&gt;-->
<!--&lt;!&ndash;                              :e-note="eNote"&ndash;&gt;-->
<!--&lt;!&ndash;                              :key="eNote.id"&ndash;&gt;-->
<!--&lt;!&ndash;                              @showEditForm="showEditForm(eNote)"&ndash;&gt;-->
<!--&lt;!&ndash;        ></entry-note-list-item>&ndash;&gt;-->
<!--&lt;!&ndash;    </div>&ndash;&gt;-->
<!--&lt;!&ndash;    <q-banner v-else rounded class="bg-grey-3">&ndash;&gt;-->
<!--&lt;!&ndash;        К сожалению, ничего не найдено.&ndash;&gt;-->
<!--&lt;!&ndash;    </q-banner>&ndash;&gt;-->
<!--&lt;!&ndash;    &ndash;&gt;-->
<!--&lt;!&ndash;    <entry-info-form-modal&ndash;&gt;-->
<!--&lt;!&ndash;        v-model:is-show="isShowCreate"&ndash;&gt;-->
<!--&lt;!&ndash;        v-model="createStore.Note"&ndash;&gt;-->
<!--&lt;!&ndash;        :is-loading="createStore.isLoading"&ndash;&gt;-->
<!--&lt;!&ndash;        entry-info-type="Note"&ndash;&gt;-->
<!--&lt;!&ndash;        title="Добавление заметки"&ndash;&gt;-->
<!--&lt;!&ndash;        btn-title="Добавить"&ndash;&gt;-->
<!--&lt;!&ndash;        btn-icon="las la-plus-circle"&ndash;&gt;-->
<!--&lt;!&ndash;        @submit="onSubmitCreateForm"&ndash;&gt;-->
<!--&lt;!&ndash;        :is-create="true"&ndash;&gt;-->
<!--&lt;!&ndash;    />&ndash;&gt;-->

<!--&lt;!&ndash;    <entry-info-form-modal&ndash;&gt;-->
<!--&lt;!&ndash;        v-if="editStore.Note.model"&ndash;&gt;-->
<!--&lt;!&ndash;        v-model="editStore.Note.model"&ndash;&gt;-->
<!--&lt;!&ndash;        v-model:is-show="isShowEdit"&ndash;&gt;-->
<!--&lt;!&ndash;        entry-info-type="Note"&ndash;&gt;-->
<!--&lt;!&ndash;        :is-loading="editStore.isLoading"&ndash;&gt;-->
<!--&lt;!&ndash;        :is-create="false"&ndash;&gt;-->
<!--&lt;!&ndash;        title="Изменение заметки"&ndash;&gt;-->
<!--&lt;!&ndash;        btn-title="Сохранить"&ndash;&gt;-->
<!--&lt;!&ndash;        btn-icon="las la-save"&ndash;&gt;-->
<!--&lt;!&ndash;        @submit="onSubmitEditForm"&ndash;&gt;-->
<!--&lt;!&ndash;        @delete="onDelete"&ndash;&gt;-->
<!--&lt;!&ndash;        @softDelete="onSubmitEditForm"&ndash;&gt;-->
<!--&lt;!&ndash;        @recover="onSubmitEditForm"&ndash;&gt;-->
<!--&lt;!&ndash;    />&ndash;&gt;-->
<!--</template>-->

<!--<script setup lang="ts">-->
<!--// import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';-->
<!--// import EntryNoteListItem from './EntryNote.List.Item.vue';-->
<!--//-->
<!--// import {useRoute} from "vue-router";-->
<!--// import {entryMessages} from "../../../localize/messages";-->
<!--// import {useEntryNoteListStore} from "../../../store/entry_notes/entryNote.list.store";-->
<!--// import {onMounted, ref, watch} from "vue";-->
<!--// import {useEntryInfoCreateStore} from "../../../store/entry_info/entryInfo.create.store";-->
<!--// import {useEntryInfoEditStore} from "../../../store/entry_info/entryInfo.edit.store";-->
<!--// import {InfoNote} from "../../../api/api_types";-->
<!--// import {apiMappers} from "../../../api/api_mappers";-->
<!--// import {apiEntryNote} from "../../../api/rerources/api_entry_info";-->
<!--//-->
<!--// const route = useRoute();-->
<!--// const listStore = useEntryNoteListStore();-->
<!--// const entryId = route.params.entryId as string;-->
<!--//-->
<!--// watch([() => listStore.listRequest.search, () => listStore.listRequest.isDeleted], async () => {-->
<!--//     await listStore.getNotes(entryId);-->
<!--// })-->
<!--//-->
<!--// onMounted(async () => {-->
<!--//     await listStore.getNotes(entryId);-->
<!--// })-->
<!--//-->
<!--// // edit-->
<!--// const editStore = useEntryInfoEditStore();-->
<!--// const isShowEdit = ref(false);-->
<!--// const entryNoteEditable = ref<InfoNote | null>(null);-->
<!--// const showEditForm = (entryNote: InfoNote) => {-->
<!--//     editStore.Note.model = apiMappers.toEntryInfoFormRequest.Note(entryNote);-->
<!--//     entryNoteEditable.value = entryNote;-->
<!--//    -->
<!--//     isShowEdit.value = true;-->
<!--// }-->
<!--// const onSubmitEditForm = async () => {-->
<!--//     if (!entryNoteEditable.value) throw Error('onSubmitEditForm entryNoteEditable not found');-->
<!--//     const entryNoteId = entryNoteEditable.value.id;-->
<!--//     await editStore.update(entryId, entryNoteId, 'NOTE');-->
<!--//     const eNoteUpdated = await apiEntryNote.get(entryId, entryNoteId);-->
<!--//     listStore.notes[listStore.notes.findIndex(x => x.id === eNoteUpdated.id)] = eNoteUpdated;-->
<!--//    -->
<!--//     isShowEdit.value = false;-->
<!--// }-->
<!--//-->
<!--// // delete-->
<!--// const onDelete = async () => {-->
<!--//     if (!entryNoteEditable.value) throw Error('onDelete entryNoteEditable not found');-->
<!--//     const entryNoteId = entryNoteEditable.value.id;-->
<!--//     await apiEntryNote.delete(entryId, entryNoteId);-->
<!--//     listStore.notes.splice(listStore.notes.findIndex(x => x.id === entryNoteId), 1);-->
<!--//    -->
<!--//     isShowEdit.value = false;-->
<!--// }-->
<!--//-->
<!--// // create-->
<!--// const isShowCreate = ref(false);-->
<!--// const createStore = useEntryInfoCreateStore();-->
<!--// const showCreateForm = () => {-->
<!--//     createStore.$reset();-->
<!--//     isShowCreate.value = true;-->
<!--// }-->
<!--// const onSubmitCreateForm = async () => {-->
<!--//     const eNote = await createStore.create(entryId, 'NOTE');-->
<!--//     listStore.notes.unshift(eNote);-->
<!--//     isShowCreate.value = false;-->
<!--// }-->
<!--</script>-->