<template>
    <div class="sidebar-header bg-secondary fixed-top text-white">
        <q-toolbar class="bg-secondary">
            <div class="col">
                <q-input
                    v-model="model.search" debounce="250"
                    placeholder="Поиск..."
                    color="secondary"
                    label-color="grey-2"
                    class="text-white"
                    :autofocus="autofocus"
                    dark
                    borderless
                    dense
                >
                    <template v-slot:prepend>
                        <q-icon name="las la-search text-grey-2"/>
                    </template>
                    <template v-slot:append>
                        <q-icon v-if="model.search" @click="model.search = ''" name="cancel" class="cursor-pointer text-grey-2"/>
                    </template>
                </q-input>
            </div>
        </q-toolbar>
    </div>
</template>

<script setup lang="ts">
import {EntryListRequest} from "../../../api/api_types";
import {computed} from "vue";

const props = defineProps<{
    modelValue: EntryListRequest,
    autofocus?: boolean
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: EntryListRequest): void
}>()
const model = computed({
    get: (): EntryListRequest => props.modelValue,
    set: val => emit('update:modelValue', val)
})
</script>

<style lang="scss">
.sidebar-header {
    border-bottom: 1px solid $dark;
}
</style>