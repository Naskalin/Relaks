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
                                <q-popup-edit :model-value="category" @update:model-value="val => onCategoryPopupEdit(category, val)" auto-save v-slot="scope">
                                    <q-input v-model="scope.value" dense autofocus counter @keyup.enter="scope.set" />
                                </q-popup-edit>
                            </q-btn>
                        </div>
                    </template>
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
import {FileListTableStoreState} from "../../../store/entryFile/entryFile.list.table.store";
import {useEntryFileCreateStore} from "../../../store/entryFile/entryFile.create.store";
import {apiEntryFile} from "../../../api/rerources/api_entry_file";

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

const onCategoryPopupEdit = async (val: string, newVal: string) => {
    if(!props.withEdit) return;
    
    await apiEntryFile.updateMeta(props.entryId, {
        value: val,
        newValue: newVal,
        field: 'Category'
    })
    
    await metaStore.getMeta(props.entryId);
    listStore.value.listRequest.category = newVal;
}

// const tagsOptions = computed(() => selectHelper.arrayToSelectOptions(props.filesMeta.tags));
</script>