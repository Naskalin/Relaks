<template>
<modal v-model="model.isShow" :title="title">
    <q-form autocorrect="off"
            autocapitalize="off"
            autocomplete="off"
            spellcheck="false"
            id="entry-form"
    >
<!--            @submit.prevent="addEntry"-->
        <entry-fields v-model="model.model"></entry-fields>
    </q-form>

<!--    <q-card-actions align="right" class="q-pa-md">-->
<!--        <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>-->
<!--        <q-btn label="Добавить" form="entry-form" type="submit" color="primary" icon="las la-plus-circle" :loading="store.isCreating"/>-->
<!--    </q-card-actions>-->
</modal>
</template>

<script setup lang="ts">
import Modal from '../../components/Modal.vue';
import {CreateEntryRequest, UpdateEntryRequest} from "../../../api/rerources/api_entry";
import EntryFields from './Entry.Fields.vue';
import {computed} from "vue";

declare type EntryFormModalValue = {
    model: CreateEntryRequest | UpdateEntryRequest,
    isShow: boolean
}
const props = defineProps<{
    title: string,
    modelValue: EntryFormModalValue
}>()

const emit = defineEmits<{
    (e: 'update:modelValue', value: EntryFormModalValue): void
}>();

const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
</script>