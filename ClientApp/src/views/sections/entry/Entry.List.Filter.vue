<template>
    <div class="q-gutter-md">
        <p class="text-center text-h6">Фильтр</p>

        <q-btn-toggle
            class="bg-grey-2"
            v-model="model.entryType"
            spread
            toggle-color="primary"
            :options="entryMessages.entryType.selectOptions"
        />
        
        <div class="text-center">
            <q-radio v-model="model.isDeleted" :val="null" label="Все объединения" />
            <q-radio v-model="model.isDeleted" :val="false" label="Актуальные" />
            <q-radio v-model="model.isDeleted" :val="true" label="Архив" />
        </div>

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

// const debounceSearch = debounce((val) => {
//     props.modelValue.search = val;
//     emit('update:modelValue', props.modelValue);
// }, 200)
</script>