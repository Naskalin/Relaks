<template>
    <modal :title="title" v-model:is-show="isShowModel" full-width>
        <q-card-section>
            <file-list-table
                @getFiles="listStore.getFiles(entryId)"
                @rowClick="file => emit('fileSelect', file)"
                v-model="listStore"
            />
        </q-card-section>
    </modal>
</template>

<script setup lang="ts">
import Modal from '../components/Modal.vue';
import FileListTable from '../sections/files/File.List.Table.vue';
import {useFileListTableStore} from "../../store/entryFile/entryFile.list.table.store";
import {useEntryFileMetaListStore} from "../../store/entryFile/entryFileMeta.list.store";
import {FileModel} from "../../api/api_types";
import {computed, watch, onMounted} from 'vue';

const listStore = useFileListTableStore();
const filesMetaStore = useEntryFileMetaListStore();
const props = defineProps<{
    entryId: string,
    isShow: boolean
    title: string
}>()
onMounted(async () => {
    await filesMetaStore.getMeta(props.entryId);
});
const emit = defineEmits<{
    (e: 'update:isShow', value: boolean): void,
    (e: 'fileSelect', file: FileModel): void,
}>()
const isShowModel = computed({
    get: () => props.isShow,
    set: (val: boolean) => emit('update:isShow', val),
})
watch(() => props.entryId, () => {
    listStore.$reset();
    filesMetaStore.$reset();
}, {immediate: true})
</script>