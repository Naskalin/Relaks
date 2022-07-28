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
                        color="primary"/>
                    <q-card v-if="createStore.isCreateCategory" class="q-my-md">
                        <q-card-section>
                            <q-icon name="las la-info-circle" class="q-mx-xs" size="1.2rem"/> Введите название категории и выберите файлы для загрузки
                            <q-input
                                autofocus
                                v-model="createStore.newCategory"
                                label="Название категории"
                                counter
                                maxlength="200"
                                required="required"
                                type="text" style="max-width: 500px"/>
                        </q-card-section>
                    </q-card>
                </div>
                <div>
                    <div><q-radio v-model="listStore.listRequest.category" :val="''" label="Все" color="secondary"/></div>
                    <div><q-radio v-model="listStore.listRequest.category" :val="null" label="Без категории" color="secondary"/></div>
                    <template v-if="metaStore.meta">
                        <div v-for="category in metaStore.meta.categories">
                            <q-radio v-model="listStore.listRequest.category" :val="category" :label="category"/>
                            <q-btn icon="las la-edit" size="sm" color="secondary" outline class="q-mx-md">
                                <q-popup-edit :model-value="category" @update:model-value="val => onPopupEdit(category, val, 'Category')" auto-save v-slot="scope">
                                    <q-input v-model="scope.value" style="min-width: 350px" dense autofocus counter maxlength="200" @keyup.enter="scope.set"/>
                                </q-popup-edit>
                            </q-btn>
                        </div>
                    </template>
                </div>
            </div>

            <div>
                <q-icon name="las la-tags la-lg la-fw"/>
                <span class="label-caption q-mx-sm">Метки</span>
                <template v-if="metaStore.meta">
                    <div v-for="tag in metaStore.meta.tags">
                        <q-checkbox
                            v-model="listStore.listRequest.tags"
                            :val="tag"
                            :label="tag"
                        />
                        <q-btn icon="las la-edit" size="sm" color="secondary" outline class="q-mx-md">
                            <q-popup-edit :model-value="tag" @update:model-value="val => onPopupEdit(tag, val, 'Tag')" auto-save v-slot="scope">
                                <q-input v-model="scope.value" style="min-width: 350px" dense autofocus counter maxlength="200" @keyup.enter="scope.set" />
                            </q-popup-edit>
                        </q-btn>
                    </div>
                </template>
            </div>
        </div>

    <file-list-table
        @getFiles="listStore.getFiles(entryId)"
        @rowClick="file => emit('rowClick', file)"
        :list-store="listStore"
        :with-edit="props.withEdit"
        v-bind="$attrs"
    />
</template>

<script setup lang="ts">
import FileListTable from '../files/File.List.Table.vue';

import {useEntryFileMetaListStore} from "../../../store/entryFile/entryFileMeta.list.store";
import {onMounted, watch} from 'vue';
import {FileListTableStoreState} from "../../../store/entryFile/entryFile.list.table.store";
import {useEntryFileCreateStore} from "../../../store/entryFile/entryFile.create.store";
import {apiEntryFile} from "../../../api/rerources/api_entry_file";
import {useFileListTableStore} from "../../../store/entryFile/entryFile.list.table.store";
import {FileModel} from "../../../api/api_types";

const props = defineProps<{
    entryId: string,
    withEdit?: boolean
}>();
const emit = defineEmits<{
    (e: 'update:listStore', val: FileListTableStoreState): void
    (e: 'rowClick', val: FileModel): void
}>()

const listStore = useFileListTableStore();
const metaStore = useEntryFileMetaListStore();
const createStore = useEntryFileCreateStore();

// При изменении категории для поиска файлов изменяем категорию для загрузки файлов
watch(
    () => listStore.listRequest.category,
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

const onPopupEdit = async (val: string, newVal: string, field: 'Tag' | 'Category') => {
    if(!props.withEdit) return;
    
    await apiEntryFile.updateMeta(props.entryId, {
        value: val,
        newValue: newVal,
        field: field
    })
    
    await metaStore.getMeta(props.entryId);
    if (field === 'Category') {
        listStore.listRequest.category = newVal   
    } else {
        listStore.$reset();
        await listStore.getFiles(props.entryId);
    }
}
</script>