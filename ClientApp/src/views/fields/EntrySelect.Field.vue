<template>
    <div class="row items-center q-col-gutter-lg">
        <div class="col-auto">
            <span class="text-grey-7">Объединение <sup>*</sup></span>
        </div>
        <div class="col">
            <q-list v-if="entrySelected" bordered>
                <entry-item
                    :entry="entrySelected"
                    @click="isShow = true"
                    clickable
                />
            </q-list>
            <q-btn
                v-else
                label="Выбрать"
                color="secondary"
                size="sm"
                outline
                icon="las la-hand-pointer"
                :disable="isShow"
                @click="isShow = true"
            />
        </div>
    </div>
    
    <q-dialog 
        v-model="isShow" 
        full-height 
        full-width
    >
        <q-layout view="lHh Lpr lFf" container style="height: 500px" class="bg-grey-3">
            <q-header bordered class="bg-my-grey text-black" elevated>
                <q-toolbar>
                    <q-toolbar-title>{{label || 'Выберите объединение'}}</q-toolbar-title>
                    <q-btn flat round dense icon="close" v-close-popup />
                </q-toolbar>
            </q-header>

            <q-footer class="bg-my-grey text-black" elevated>
                <q-card flat>
                    <div class="row items-center">
                        <div class="col">
                            <div class="q-px-md text-grey-8">
                                <q-icon name="las la-hand-pointer"/>
                                Вместо кнопки выбора можно <b>дважды кликнуть</b> на строку или на аватар в карточке объединения.
                            </div>
                        </div>
                        <div class="col-auto">
                            <q-card-actions class="q-py-sm q-px-md" align="right">
                                <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
                                <q-btn
                                    :disable="!entryListStore.previewEntry"
                                    color="primary"
                                    icon="las la-check-circle"
                                    label="Выбрать объединение"
                                    @click="onSelectEntry(entryListStore.previewEntry!)"/>
                            </q-card-actions>
                        </div>
                    </div>
  
                </q-card>
            </q-footer>
            
            <q-drawer
                :width="380"
                show-if-above
                behavior="desktop"
                bordered
                class="bg-my-light"
            >
                <q-scroll-area class="fit">
                    <div class="q-pa-md">
                        <entry-filter
                            :is-show-search="true"
                            :preview-entry="entryListStore.previewEntry"
                            v-model="entryListStore.listRequest"
                            @preview-dblclick="onSelectEntry"
                        />
                    </div>
                </q-scroll-area>
            </q-drawer>

            <q-page-container>
                <q-page class="q-pa-md bg-my-light q-pb-xl">
                    <entry-list-table @row-dblclick="onSelectEntry" :entry-selected-id="entrySelectedId"/>
                </q-page>
            </q-page-container>
        </q-layout>
    </q-dialog>
</template>

<script setup lang="ts">
import {ref, onMounted, watch} from "vue";
import EntryListTable from '../sections/entry/Entry.List.Table.vue';
import {Entry} from "../../api/api_types";
import EntryFilter from '../sections/entry/Entry.List.Filter.vue';
import {useEntryListStore} from "../../store/entry/entry.list.table.store";
import {apiEntry} from "../../api/rerources/api_entry";
import EntryItem from '../components/entry_item/Entry.Item.vue';

const entrySelected = ref<null | Entry>(null);
const isShow = ref(false);
const entryListStore = useEntryListStore();
const props = defineProps<{
    entrySelectedId?: string | null
    label?: string,
    isShowDefault?: boolean
}>()
const emit = defineEmits<{
    (e: 'onSelectEntry', entry: Entry): void,
}>()
watch(() => isShow.value, val => {
    if (!val) entryListStore.$reset();
})
const onSelectEntry = (val: Entry) => {
    emit('onSelectEntry', val);
    entrySelected.value = val;
    isShow.value = false;
}
onMounted(async () => {
    if (props.isShowDefault) isShow.value = true;
    if (props.entrySelectedId && props.entrySelectedId !== '') {
        entrySelected.value = await apiEntry.get(props.entrySelectedId);
    }
})
</script>