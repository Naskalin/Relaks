<template>
    <q-dialog 
        :model-value="isShow"
        @update:model-value="val => emit('update:isShow', val)" 
        full-height 
        full-width
    >
        <q-layout view="lHh Lpr lFf" container style="height: 500px" class="bg-grey-3">
            <q-header bordered class="bg-my-grey text-black">
                <q-toolbar>
                    <q-toolbar-title>Выберите объединение</q-toolbar-title>
                    <q-btn flat round dense icon="close" v-close-popup />
                </q-toolbar>
            </q-header>
            <q-drawer
                :width="380"
                show-if-above
                behavior="desktop"
                bordered
                class="body--light"
            >
                <q-scroll-area class="fit">
                    <div class="q-pa-md">
<!--                        <p v-for="n in 50" :key="n">-->
<!--                            Lorem ipsum dolor sit amet consectetur adipisicing elit. Fugit nihil praesentium molestias a adipisci, dolore vitae odit, quidem consequatur optio voluptates asperiores pariatur eos numquam rerum delectus commodi perferendis voluptate?-->
<!--                        </p>-->
<!--&lt;!&ndash;                        asd&ndash;&gt;-->
                        <entry-filter
                            :is-show-search="true"
                            v-model="entryListStore.listRequest"
                            :preview-entry="entryListStore.previewEntry"
                            @preview-dblclick="onSelectEntry"
                        />
                    </div>
                </q-scroll-area>
            </q-drawer>

            <q-page-container>
                <q-page class="q-pa-md body--light q-pb-xl">
                    <entry-list-table @row-dblclick="onSelectEntry"/>
<!--                    <p v-for="n in 50" :key="n">-->
<!--                        Lorem ipsum dolor sit amet consectetur adipisicing elit. Fugit nihil praesentium molestias a adipisci, dolore vitae odit, quidem consequatur optio voluptates asperiores pariatur eos numquam rerum delectus commodi perferendis voluptate?-->
<!--                    </p>-->
                </q-page>

                <q-page-sticky position="bottom-right">
                    <q-btn v-if="entryListStore.previewEntry" color="primary" icon="las la-check-circle" label="Выбрать объединение" @click="onSelectEntry(entryListStore.previewEntry)"/>
                </q-page-sticky>
            </q-page-container>
        </q-layout>
    </q-dialog>
    
<!--    <q-dialog-->
<!--        full-height-->
<!--        full-width-->
<!--        :model-value="isShow" -->
<!--        @update:model-value="val => emit('update:isShow', val)" -->
<!--        v-bind="props">-->
<!--        -->
<!--        <q-card>-->
<!--            <q-card-section class="row items-center">-->
<!--                <div class="text-h6">{{ title || 'Выбор объединения' }}</div>-->
<!--                <q-space/>-->
<!--                <q-btn icon="close" flat round dense v-close-popup/>-->
<!--            </q-card-section>-->
<!--            -->
<!--            <q-separator/>-->
<!--            -->
<!--            <q-card-section class="scroll q-pa-none" style="max-height: 80vh">-->
<!--                <q-layout view="lHh Lpr lFf">-->
<!--                    <entry-list-table @row-click="onRowClick"/>-->
<!--                </q-layout>-->
<!--            </q-card-section>-->
<!--            -->
<!--            <q-separator/>-->

<!--            <q-card-actions align="right" class="q-pa-md">-->
<!--                <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>-->
<!--                <q-btn v-close-popup label="Выбрать объединение" icon="las la-check-circle" color="secondary"/>-->
<!--            </q-card-actions>-->
<!--        </q-card>-->
<!--    </q-dialog>-->
</template>

<script setup lang="ts">
import {computed} from "vue";
import EntryListTable from '../sections/entry/Entry.List.Table.vue';
import {Entry} from "../../api/api_types";
import EntryFilter from '../sections/entry/Entry.List.Filter.vue';
import {useEntryListStore} from "../../store/entry/entry.list.table.store";

const entryListStore = useEntryListStore();
const props = defineProps<{
    isShow: boolean,
    entrySelectedId?: string
    title?: string
}>()
const emit = defineEmits<{
    (e: 'onSelectEntry', entry: Entry): void,
    (e: 'update:isShow', val: boolean): void,
}>()
const onSelectEntry = (val: Entry) => {
    emit('onSelectEntry', val);
    emit('update:isShow', false);
}
</script>