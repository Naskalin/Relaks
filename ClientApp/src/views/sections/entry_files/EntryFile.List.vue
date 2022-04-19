<template>
    <div class="row justify-between items-center q-mb-md">
        <h5 class="q-my-md">{{ entryMessages.profile.tabs['entry-file-list'] }}</h5>
        <div style="width: 400px">
            <entry-file-create @onUploaded="onUploaded" :entry-id="entryId"/>
        </div>
    </div>

    <br>

    <file-list-table
        @getFiles="listStore.getFiles(entryId)"
        with-edit
        with-download
        with-explorer
        v-model="listStore"
        @showEdit="onShowEdit"
    />
<!--    v-model:is-end="listStore.isEnd"-->
<!--    v-model:list-request="listStore.listRequest"-->
<!--        :files="listStore.files"-->
<!--        :is-loading="listStore.isLoading"-->

    <file-edit-modal
        v-if="editStore.file"
        v-model="editStore.file"
        v-model:is-show="isShowEdit"
        :is-loading="editStore.isLoading"
        @submit="saveEditForm"
        @recover="onRecover"
        @delete="onDelete"
        @softDelete="onSoftDelete"
    />
    <a :href="testLinkHref" download="test.jpg" ref="testLink"></a>
</template>

<script setup lang="ts">
import FileListTable from '../files/File.List.Table.vue';
import FileEditModal from '../files/File.Edit.Modal.vue';
import EntryFileCreate from './EntryFile.Create.vue';

import {entryMessages} from "../../../localize/messages";
import {useRoute} from "vue-router";
import {useFileListTableStore} from "../../../store/entryFile/entryFile.list.table.store";
import {useEntryFileEditStore} from "../../../store/entryFile/entryFile.edit.store";
import {ref, computed, onMounted} from 'vue';
import {EntryFile} from "../../../api/api_types";
import {apiMappers} from "../../../api/api_mappers";
import {apiEntryFile} from "../../../api/rerources/api_entry_file";

const route = useRoute();
const entryId = computed(() => route.params.entryId as string)
const listStore = useFileListTableStore();
const editStore = useEntryFileEditStore();
const isShowEdit = ref(false);

// initialize
onMounted(() => listStore.$reset());

// create
const onUploaded = async () => {
    listStore.$reset();
    await listStore.getFiles(entryId.value);
}

// edit
const onShowEdit = (value: any) => {
    editStore.file = {...value as EntryFile};
    isShowEdit.value = true
}

const saveEditForm = async () => {
    if (!editStore.file) throw Error('saveEditForm. EditStore file model not found');
    const req = apiMappers.toFileModelUpdateRequest(editStore.file);
    await editStore.update(req);

    const indexOf = listStore.files.findIndex(x => x.id === editStore.file!.id);
    if (indexOf > -1) listStore.files[indexOf] = editStore.file;
    
    isShowEdit.value = false;
    editStore.$reset();
}
const onSoftDelete = async () => {
    await saveEditForm();
}

const onRecover = async () => {
    await saveEditForm();
}

const onDelete = async () => {
    if (!editStore.file) throw Error('onDelete. EditStore file model not found');
    await apiEntryFile.delete(entryId.value, editStore.file.id);

    const indexOf = listStore.files.findIndex(x => x.id === editStore.file!.id);
    if (indexOf > -1) listStore.files.splice(indexOf, 1);

    isShowEdit.value = false;
    editStore.$reset();
}

// open file
// https://gist.github.com/javilobo8/097c30a233786be52070986d8cdb1743
const testLinkHref = ref('');
const testLink = ref(null);
</script>