<template>
    <q-table
        :columns="columns"
        title="Файлы"
        :loading="listStore.isLoading"
        row-key="id"
        :rows="listStore.files"
        :pagination="{rowsPerPage: 0}"
        :virtual-scroll-item-size="65"
        :virtual-scroll-sticky-size-start="65"
        virtual-scroll
        :rows-per-page-options="[0]"
        @virtual-scroll="onVirtualScroll"
    >
        <template v-slot:top>
            <div>
                <h6 class="q-mb-md">
                    <q-icon name="las la-folder-open la-lg la-fw"/>
                    {{categoryName}}
                </h6>

                <div>
                    <small class="text-uppercase q-mr-md">Актуальность</small>
                    <q-btn-toggle
                        class="bg-grey-2"
                        v-model="listStore.listRequest.isDeleted"
                        toggle-color="secondary"
                        :options="[
                {label: 'Все', value: null},
                {label: 'Актуальные', value: false},
                {label: 'Архивные', value: true},
            ]"
                    />
                </div>
            </div>
        </template>
        <template v-slot:header="p">
            <q-tr :props="p">
                <q-th v-if="props.withEdit" style="width: 70px"/>
                <q-th
                    v-for="col in p.cols"
                    :key="col.name"
                    :props="p"
                >{{ col.label }}
                </q-th>
                <q-th v-if="props.withDownload" style="width: 70px"/>
            </q-tr>
        </template>

        <template v-slot:body="p">
            <q-tr :props="p"
                  :key="p.row.id"
                  :class="{'bg-pink-1': p.row.deletedAt}"
                  @click="emit('rowClick', p.row)">
                <q-td v-if="props.withEdit" class="q-gutter-x-sm">
                    <q-btn v-if="withEdit" size="sm" color="primary" round @click="emit('showEdit', p.row)">
                        <q-icon name="las la-edit" size="1.2rem"/>
                        <q-tooltip>Изменить</q-tooltip>
                    </q-btn>
                </q-td>
                <q-td
                    v-for="col in p.cols"
                    :key="col.name"
                    :props="p"
                >
                    <file-in-table-cell title="Открыть" @click="emit('clickAvatar', p.row)" v-if="col.name === 'path'"
                                        :file="p.row"/>
                    <template v-else>{{ col.value }}</template>
                </q-td>
                <q-td v-if="withDownload">
                    <q-btn size="sm" outline color="secondary" round @click="download(p.row.id)">
                        <q-icon name="las la-file-export" size="1.2rem"/>
                        <q-tooltip>Экспортировать</q-tooltip>
                    </q-btn>
                </q-td>
            </q-tr>
        </template>
    </q-table>
</template>

<script setup lang="ts">
import FileInTableCell from '../../components/FileInTableCell.vue';
import {fileFieldNames as trans} from "../../../localize/messages";
import {dateHelper} from "../../../utils/date_helper";
import {FileModel} from "../../../api/api_types";
import {computed, watch} from 'vue';
import {getFileExtension} from "../../../utils/file_helper";
import {FileListTableStoreState} from "../../../store/entryFile/entryFile.list.table.store";
import {apiFiles} from "../../../api/rerources/api_files";
import {debounce} from "quasar";

const props = defineProps<{
    listStore: FileListTableStoreState
    withEdit?: boolean
    withDownload?: boolean
    withExplorer?: boolean
}>()

const emit = defineEmits<{
    (e: 'getFiles'): void,
    (e: 'showEdit', row: FileModel): void,
    (e: 'rowClick', row: FileModel): void,
    (e: 'clickAvatar', row: FileModel): void,
    (e: 'update:listStore', val: FileListTableStoreState): void
}>()

let columns = [
    {name: 'path', field: 'path', label: 'Файл', style: 'width: 80px'},
    {
        name: 'name',
        field: 'name',
        label: trans.name,
        format: (val: string, row: FileModel) => val + '.' + getFileExtension(row.path)
    },
    {name: 'category', field: 'category', label: 'Категория'},
    {name: 'tags', field: 'tags', label: 'Метки', format: (val: string[]) => val.join(', ')},
    {name: 'contentType', field: 'contentType', label: trans.contentType, style: 'width: 180px'},
    {
        name: 'createdAt',
        field: 'createdAt',
        label: trans.createdAt,
        format: (val: string) => dateHelper.utcFormat(val),
        style: 'width: 130px'
    },
    {
        name: 'updatedAt',
        field: 'updatedAt',
        label: trans.updatedAt,
        format: (val: string) => dateHelper.utcFormat(val),
        style: 'width: 130px'
    },
    {
        name: 'deletedAt',
        field: 'deletedAt',
        label: trans.deletedAt,
        format: (val: string) => dateHelper.utcFormat(val),
        style: 'width: 130px'
    },
    {name: 'deletedReason', field: 'deletedReason', label: trans.deletedReason},
]
columns = columns.map(row => ({...row, align: 'left'}));

const categoryName = computed(() => {
    const c = listStore.value.listRequest.category;
    if (c === null) return 'Без категории';
    if (c === '') return 'Все';
    return c;
});
const listStore = computed({
    get: () => props.listStore,
    set: val => emit('update:listStore', val),
})
const filesCount = computed(() => listStore.value.files.length);
const onVirtualScroll = async ({to}: { to: number }) => {
    if (
        to === -1
        || (to === filesCount.value - 1 && !listStore.value.isEnd)
    ) {
        getFilesDebounce();
    }
}

watch(() => [
    listStore.value.listRequest.isDeleted,
    listStore.value.listRequest.category,
    listStore.value.listRequest.tags,
], () => {
    listStore.value.isEnd = false;
    listStore.value.listRequest.page = 1;
    listStore.value.files = [];

    getFilesDebounce();
});

const download = async (fileId: string) => {
    if (!props.withDownload) return;

    const resp = await apiFiles.download({fileId});
    const fileName = resp.headers['content-disposition'].split('filename=')[1].split(';')[0];
    const url = URL.createObjectURL(resp.data);
    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', fileName);
    document.body.appendChild(link);
    link.click();
    link.remove();
}

const getFilesDebounce = debounce(() => {
    // Тротлинг для GetFiles, при первом обращении при обновлении страницы происходит путаница.
    emit('getFiles')
}, 50);
</script>