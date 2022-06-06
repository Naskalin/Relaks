<template>
        <div class="flex column q-col-gutter-y-md q-mb-lg">
            <div>
                <div>
                    <q-icon name="las la-folder-open la-lg la-fw"/>
                    <span class="label-caption q-mx-sm">Категории</span>
                    &nbsp;
                    <q-btn
                        v-if="withEdit"
                        @click="createStore.isCreateCategory = !createStore.isCreateCategory; createStore.newCategory = ''"
                        label="Новая категория"
                        icon="las la-plus-circle"
                        size="sm"
                        color="secondary"/>
                    <div v-if="createStore.isCreateCategory" class="q-my-md q-pa-md" style="border: 1px solid #aaa">
                        <q-input
                            autofocus
                            v-model="createStore.newCategory"
                            label="Название категории"
                            hint="Выберите файлы для загрузки в новую категорию"
                            counter
                            maxlength="200"
                            required="required"
                            type="text" style="max-width: 500px"/>
                    </div>
                </div>
                <div>
                    <q-option-group
                        :options="categoryOptions"
                        type="radio"
                        v-model="listStore.listRequest.category"
                    />
                </div>
            </div>

            <div>
                <q-icon name="las la-tags la-lg la-fw"/>
                <span class="label-caption q-mx-sm">Метки</span>
                <div>
                    <q-checkbox
                        v-if="metaStore.meta && metaStore.meta.tags.length"
                        v-for="tag in metaStore.meta.tags"
                        v-model="listStore.listRequest.tags"
                        :val="tag"
                        :label="tag"
                    />
                </div>
            </div>
        </div>
    
    <file-list-table :list-store="props.listStore" :with-edit="props.withEdit" v-bind="$attrs"/>
</template>

<script setup lang="ts">
import FileListTable from '../files/File.List.Table.vue';

import {useEntryFileMetaListStore} from "../../../store/entryFile/entryFileMeta.list.store";
import {onMounted, computed, watch} from 'vue';
import {selectHelper} from "../../../utils/select_helper";
import {FileListTableStoreState} from "../../../store/entryFile/entryFile.list.table.store";
import {useEntryFileCreateStore} from "../../../store/entryFile/entryFile.create.store";

const props = defineProps<{
    entryId: string,
    listStore: FileListTableStoreState,
    withEdit?: boolean
}>();
const emit = defineEmits<{
    (e: 'update:listStore', val: FileListTableStoreState): void
}>()

const metaStore = useEntryFileMetaListStore();
const createStore = useEntryFileCreateStore();
const listStore = computed({
    get: () => props.listStore,
    set: val => emit('update:listStore', val),
})

// При изменении категории для поиска файлов изменяем категорию для загрузки файлов
watch(
    () => listStore.value.listRequest.category,
    (val: string | null) => {
        if(val) {
            createStore.category = val;
        }
    }
)
onMounted(async () => {
    metaStore.$reset();
    await metaStore.getMeta(props.entryId);
})

const categoryOptions = computed(() => {
    const options: any = [
        {label: 'Все', value: '', color: 'secondary'},
        {label: 'Без категории', value: null, color: 'secondary'}
    ];
    if (metaStore.meta) {
        options.push(...selectHelper.arrayToSelectOptions(metaStore.meta.categories))
    }
    return options;
});

// const tagsOptions = computed(() => selectHelper.arrayToSelectOptions(props.filesMeta.tags));
</script>