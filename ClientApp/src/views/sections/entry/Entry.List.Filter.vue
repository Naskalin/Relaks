<template>
    <div class="q-mb-md">
        <div class="q-gutter-y-md">
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