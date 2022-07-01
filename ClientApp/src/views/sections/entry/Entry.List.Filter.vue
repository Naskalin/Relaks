<template>
    <div class="q-mb-md">
        <div class="q-gutter-y-md">
            <div>
                <q-input
                    v-if="isShowSearch"
                    v-model="model.search"
                    debounce="250"
                    placeholder="Поиск..."
                    color="secondary"
                >
                    <template v-slot:prepend>
                        <q-icon name="las la-search"/>
                    </template>
                    <template v-slot:append>
                        <q-icon v-if="model.search" @click="model.search = ''" name="cancel" class="cursor-pointer"/>
                    </template>
                </q-input>
            </div>
            <div>
                <q-btn-toggle
                    spread
                    class="bg-my-grey"
                    v-model="model.entryType"
                    toggle-color="secondary"
                    :options="entryTypes"
                />
            </div>

            <div>
                <q-btn-toggle
                    class="bg-my-grey"
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
        </div>
    </div>

    <entry-card
        v-if="previewEntry"
        @card-dblclick="val => emit('preview-dblclick', val)"
        :entry="previewEntry"
        :with-edit="false"/>
</template>

<script setup lang="ts">
import {computed} from "vue";
import {entryMessages} from "../../../localize/messages";
import {Entry, EntryListRequest} from "../../../api/api_types";
import EntryCard from './Entry.Card.vue';

const props = defineProps<{
    modelValue: EntryListRequest,
    previewEntry: null | Entry
    isShowSearch?: boolean
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: EntryListRequest): void,
    (e: 'preview-dblclick', val: Entry): void
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