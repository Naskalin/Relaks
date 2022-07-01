<template>
    <q-dialog
        full-height
        full-width
        :model-value="isShow" 
        @update:model-value="val => emit('update:isShow', val)" 
        v-bind="props">
        <q-card>
            <q-card-section class="row items-center">
                <div class="text-h6">{{ title || 'Выбор объединения' }}</div>
                <q-space/>
                <q-btn icon="close" flat round dense v-close-popup/>
            </q-card-section>
            
            <q-separator/>
            
            <q-card-section class="scroll" style="max-height: 80vh">
<!--                <p v-for="p in (1, 50)">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Atque corporis cumque earum harum illo, labore perferendis sint. A aperiam assumenda culpa ipsa iusto laboriosam, possimus sint tempore temporibus, vero voluptatum?</p>-->
                <entry-list-table @row-click="onRowClick"/>
            </q-card-section>
            
            <q-separator/>

            <q-card-actions align="right" class="q-pa-md">
                <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
                <q-btn v-close-popup label="Выбрать объединение" icon="las la-check-circle" color="secondary"/>
            </q-card-actions>
        </q-card>
    </q-dialog>
</template>

<script setup lang="ts">
import {computed} from "vue";
import EntryListTable from '../sections/entry/Entry.List.Table.vue';
import {Entry} from "../../api/api_types";

const props = defineProps<{
    isShow: boolean,
    entrySelectedId?: string
    title?: string
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: any): void,
    (e: 'update:isShow', val: boolean): void,
}>()
const onRowClick = (val: Entry) => {
    console.log(val);
}
// const model = computed({
//     get: () => props.modelValue,
//     set: (val: any) => emit('update:modelValue', val)
// })
</script>