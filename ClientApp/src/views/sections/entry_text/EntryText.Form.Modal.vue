<template>
    <modal v-model:is-show="isShow" :title="title">
        <q-form autocorrect="off"
                autocapitalize="off"
                autocomplete="off"
                spellcheck="false"
                id="entry-text-form"
                @submit.prevent="emit('submit', model)"
        >
            <q-card-section class="q-gutter-y-md">
                <entry-text-fields v-model="model"></entry-text-fields>
            </q-card-section>
        </q-form>

        <q-card-actions align="right" class="q-pa-md">
            <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
            <q-btn :label="btnTitle"
                   form="entry-text-form"
                   type="submit"
                   color="primary"
                   :icon="btnIcon ?? 'las la-plus-circle'"
                   :disable="isLoading"
                   :loading="isLoading"/>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import EntryTextFields from './EntryText.Fields.vue';
import Modal from '../../components/Modal.vue';
import {CreateEntryTextRequest, UpdateEntryTextRequest} from "../../../api/api_types";
import {computed, onMounted} from "vue";

const props = defineProps<{
    modelValue: CreateEntryTextRequest | UpdateEntryTextRequest,
    isCreate: boolean,
    isShow: boolean,
    title: string
    btnTitle: string
    btnIcon?: string
    isLoading: boolean
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', value: CreateEntryTextRequest | UpdateEntryTextRequest): void
    (e: 'update:isShow', value: boolean): void
    (e: 'submit', value: CreateEntryTextRequest | UpdateEntryTextRequest): void
}>()
const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
onMounted(() => {
    if (props.isCreate) model.value.actualStartAt = new Date().toISOString();
})

</script>