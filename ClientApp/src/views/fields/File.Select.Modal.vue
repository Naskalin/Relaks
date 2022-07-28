<template>
    <modal :label="props.label" v-model:is-show="isShowModel" full-width>
        <q-card-section>
            <entry-file-full-comp
                :entry-id="entryId"
                @rowClick="file => emit('fileSelect', file)"
            />
        </q-card-section>
    </modal>
</template>

<script setup lang="ts">
import Modal from '../components/Modal.vue';
import EntryFileFullComp from '../sections/entry_files/EntryFile.Full.Comp.vue';
import {useFileListTableStore} from "../../store/entryFile/entryFile.list.table.store";
import {useEntryFileMetaListStore} from "../../store/entryFile/entryFileMeta.list.store";
import {FileModel} from "../../api/api_types";
import {computed, watch, onMounted} from 'vue';

const listStore = useFileListTableStore();
const filesMetaStore = useEntryFileMetaListStore();
const props = defineProps<{
    entryId: string,
    isShow: boolean
    label: string
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