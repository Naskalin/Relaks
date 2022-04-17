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
        <template v-slot:top>
<!--        <p>store.isDeleted false {{store.isDeleted === false}}</p>-->
            <q-btn-toggle
                class="bg-grey-2"
                v-model="store.listRequest.isDeleted"
                toggle-color="secondary"
                :options="[
                {label: 'Все', value: null},
                {label: 'Актуальные', value: false},
                {label: 'Архивные', value: true},
            ]"
            />
            <!--            <q-btn color="primary" :disable="loading" label="Add row" @click="addRow" />-->
            <!--            <q-btn class="q-ml-sm" color="primary" :disable="loading" label="Remove row" @click="removeRow" />-->
            <!--            <q-space />-->
            <!--            <q-input borderless dense debounce="300" color="primary" v-model="filter">-->
            <!--                <template v-slot:append>-->
            <!--                    <q-icon name="search" />-->
            <!--                </template>-->
            <!--            </q-input>-->
        </template>

        <template v-slot:header="p">
            <q-tr :props="p">
                <q-th v-if="withEdit" style="width: 70px"/>
                <q-th
                    v-for="col in p.cols"
                    :key="col.name"
                    :props="p"
                >{{ col.label }}
                </q-th>
            </q-tr>
        </template>

        <template v-slot:body="p">
            <q-tr :props="p"
                  :key="p.row.id"
                  :class="{'bg-pink-1': p.row.deletedAt}"
                  @click="emit('rowClick', p.row)"
                  @dblclick="emit('rowDblClick', p.row)">
                <q-td v-if="withEdit">
                    <q-btn size="sm" color="primary" round @click="emit('showEdit', p.row)" icon="las la-edit"/>
                </q-td>
                <q-td
                    v-for="col in p.cols"
                    :key="col.name"
                    :props="p"
                >
                    <file-in-table-cell v-if="col.name === 'path'" :file="p.row"/>
                    <template v-else>{{ col.value }}</template>
                </q-td>
            </q-tr>
        </template>
    </q-table>
</template>

<script setup lang="ts">
import FileInTableCell from '../../components/FileInTableCell.vue';
import {fileFieldNames as trans} from "../../../localize/messages";
import {dateHelper} from "../../../utils/date_helper";
import {FileModel, PaginateListRequest} from "../../../api/api_types";
import {computed, watch} from 'vue';
import {getFileExtension} from "../../../utils/file_helper";
import {FileListTableStoreState} from "../../../store/entryFile/entryFile.list.table.store";

const props = defineProps<{
    // files: FileModel[]
    // isEnd: boolean
    // isLoading: boolean,
    // listRequest: PaginateListRequest
    modelValue: FileListTableStoreState
    withEdit?: boolean
}>()

const emit = defineEmits<{
    (e: 'getFiles'): void,
    (e: 'showEdit', row: FileModel): void,
    (e: 'rowClick', row: FileModel): void,
    (e: 'rowDblClick', row: FileModel): void,
    (e: 'update:modelValue', val: FileListTableStoreState): void 
}>()

let columns = [
    {name: 'path', field: 'path', label: 'Файл', style: {width: '80px'}},
    {
        name: 'name',
        field: 'name',
        label: trans.name,
        format: (val: string, row: FileModel) => val + '.' + getFileExtension(row.path)
    },
    {name: 'contentType', field: 'contentType', label: trans.contentType, style: {width: '180px'}},
    {
        name: 'createdAt',
        field: 'createdAt',
        label: trans.createdAt,
        format: (val: string) => dateHelper.utcFormat(val),
        style: {width: '130px'}
    },
    {
        name: 'updatedAt',
        field: 'updatedAt',
        label: trans.updatedAt,
        format: (val: string) => dateHelper.utcFormat(val),
        style: {width: '130px'}
    },
    {
        name: 'deletedAt',
        field: 'deletedAt',
        label: trans.deletedAt,
        format: (val: string) => dateHelper.utcFormat(val),
        style: {width: '130px'}
    },
    {name: 'deletedReason', field: 'deletedReason', label: trans.deletedReason},
]
columns = columns.map(row => ({...row, align: 'left'}));

const store = computed({
    get: () => props.modelValue,
    set: val => emit('update:modelValue', val),
})
const filesCount = computed(() => store.value.files.length);
const getFiles = async ({to}: { to: number }) => {
    if (
        to === -1
        || (to === filesCount.value - 1 && !store.value.isEnd)
    ) {
        emit('getFiles');
    }
}

watch(() => store.value.listRequest.isDeleted, () => {
    store.value.isEnd = false;
    store.value.listRequest.page = 1;
    store.value.files = [];
    emit('getFiles')
});
</script>