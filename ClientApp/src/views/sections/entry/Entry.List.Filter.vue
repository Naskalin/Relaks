<template>
    <div class="q-gutter-md">
        <p class="text-center text-h6">Фильтр</p>

        <div>
            <q-btn-toggle
                spread
                class="bg-grey-2"
                v-model="model.entryType"
                toggle-color="secondary"
                :options="entryTypes"
            />
        </div>

        <div>
            <q-btn-toggle
                class="bg-grey-2"
                spread
                v-model="model.isDeleted"
                toggle-color="secondary"
                :options="[
                {label: 'Все', value: null},
                {label: 'Актуальные', value: false},
                {label: 'Архивные', value: true},
            ]"
            />
        </div>
        
<!--        <div class="text-center">-->
<!--            <q-radio v-model="model.isDeleted" :val="null" label="Все объединения" />-->
<!--            <q-radio v-model="model.isDeleted" :val="false" label="Актуальные" />-->
<!--            <q-radio v-model="model.isDeleted" :val="true" label="Архив" />-->
<!--        </div>-->

        <q-input v-model="model.search" debounce="250" label="Поиск..." color="secondary">
            <template v-slot:append>
                <q-icon name="las la-search"/>
            </template>
        </q-input>
    </div>
</template>

<script setup lang="ts">
import {computed} from "vue";
import {entryMessages} from "../../../localize/messages";
import {EntryListRequest} from "../../../api/api_types";

const props = defineProps<{
    modelValue: EntryListRequest
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: EntryListRequest): void
}>()
const model = computed({
    get: (): EntryListRequest => props.modelValue,
    set: val => emit('update:modelValue', val)
})
const entryTypes = [
    {value: null, label: 'Все'},
    ...entryMessages.entryType.selectOptions,
]
</script>