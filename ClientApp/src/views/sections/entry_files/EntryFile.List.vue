<template>
    <h4 class="q-my-lg">Файлы</h4>
    
    <entry-file-full-comp
        :entry-id="entryId"
        class="list-table"
        with-edit
        with-download
        with-explorer
        @showEdit="onShowEdit"
        @clickAvatar="explorer"
    >
        <template v-slot:right-column>
            <div style="width: 500px">
                <div class="q-mb-sm">
                    <div class="text-h6 q-mb-md">Загрузить файлы</div>
                    <entry-file-create @onUploaded="onUploaded" :entry-id="entryId"/>
                </div>
<!--                <q-icon name="las la-info-circle la-fw" /> Файлы загрузятся в выбранную категорию.-->
            </div>
        </template>
    </entry-file-full-comp>

    <file-edit-modal
        v-if="editStore.file && fileMetaStore.meta"
        v-model="editStore.file"
        v-model:is-show="isShowEdit"
        :files-meta="fileMetaStore.meta"
        :is-loading="editStore.isLoading"
        @submit="saveEditForm"
        @recover="onRecover"
        @delete="onDelete"
        @softDelete="onSoftDelete"
    />
</template>

<script setup lang="ts">
import FileEditModal from '../files/File.Edit.Modal.vue';
import EntryFileCreate from './EntryFile.Create.vue';
import EntryFileFullComp from './EntryFile.Full.Comp.vue';

import {useRoute} from "vue-router";
import {useFileListTableStore} from "../../../store/entryFile/entryFile.list.table.store";
import {useEntryFileEditStore} from "../../../store/entryFile/entryFile.edit.store";
import {ref, computed, onMounted} from 'vue';
import {EntryFile, FileModel} from "../../../api/api_types";
import {apiMappers} from "../../../api/api_mappers";
import {apiEntryFile} from "../../../api/rerources/api_entry_file";
import {apiFiles} from "../../../api/rerources/api_files";
import {useEntryFileMetaListStore} from "../../../store/entryFile/entryFileMeta.list.store";
import {useEntryFileCreateStore} from "../../../store/entryFile/entryFile.create.store";

const route = useRoute();
const entryId = computed(() => route.params.entryId as string)
const listStore = useFileListTableStore();
const editStore = useEntryFileEditStore();
const fileMetaStore = useEntryFileMetaListStore();
const isShowEdit = ref(false);

// initialize
onMounted(async () => {
    listStore.$reset();
    editStore.$reset();
});

const explorer = async (file: FileModel) => {
    await apiFiles.explorer(file.id);
}

// create
const filesCreateStore = useEntryFileCreateStore();
const onUploaded = async () => {
    let tempCategory = listStore.listRequest.category;
    if(filesCreateStore.isCreateCategory && filesCreateStore.newCategory) {
        tempCategory = filesCreateStore.newCategory;
    }
    listStore.$reset();
    filesCreateStore.$reset();
    listStore.listRequest.category = tempCategory;
    await listStore.getFiles(entryId.value);
    await fileMetaStore.getMeta(entryId.value);
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
    await fileMetaStore.getMeta(entryId.value);
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
    await fileMetaStore.getMeta(entryId.value);
}
</script>