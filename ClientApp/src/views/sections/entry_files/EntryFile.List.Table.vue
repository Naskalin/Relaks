<template>
    <q-table
        :columns="columns"
        title="Файлы"
        :loading="store.isLoading"
        row-key="id"
        :rows="store.files"
        :pagination="{rowsPerPage: 0}"
        :virtual-scroll-item-size="65"
        :virtual-scroll-sticky-size-start="65"
        virtual-scroll
        :rows-per-page-options="[0]"
        @virtual-scroll="getFiles"
    >
        <template v-slot:body-cell-path="props">
            <q-td :props="props">
                {{props.row.path}}
                <q-avatar>
                    <img src="https://cdn.quasar.dev/img/avatar.png">
                </q-avatar>
            </q-td>
        </template>
    </q-table>
</template>

<script setup lang="ts">
import {entryFileFieldNames as trans} from "../../../localize/messages";
import {useEntryFileListTableStore} from "../../../store/entryFile/entryFile.list.table.store";
import {dateHelper} from "../../../utils/date_helper";

const props = defineProps<{
    entryId: string
}>()

const store = useEntryFileListTableStore();
const columns = [
    {name: 'path', field: 'path', label: ''},
    {name: 'name', field: 'name', label: trans.name},
    {name: 'contentType', field: 'contentType', label: trans.contentType},
    {name: 'createdAt', field: 'createdAt', label: trans.createdAt, format: (val:string) => dateHelper.utcFormat(val)},
    {name: 'updatedAt', field: 'updatedAt', label: trans.updatedAt, format: (val:string) => dateHelper.utcFormat(val)},
    {name: 'deletedAt', field: 'deletedAt', label: trans.deletedAt, format: (val:string) => dateHelper.utcFormat(val)},
    {name: 'deletedReason', field: 'deletedReason', label: trans.deletedReason},
]
const getFiles = async ({to}: { to: number }) => {
    const rowsCount = store.files.length;
    if (
        to === -1
        || (to === rowsCount - 1 && !store.isEnd)
    ) {
        await store.getFiles(props.entryId);
    }
}
</script>