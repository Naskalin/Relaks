<template>
    <modal v-model:is-show="isShow" :label="label">
        <q-form autocorrect="off"
                autocapitalize="off"
                autocomplete="off"
                spellcheck="false"
                id="entry-form"
                @submit.prevent="emit('submit', model)"
        >
            <q-card-section class="q-gutter-y-md">
                <entry-fields :is-create="isCreate" v-model="model"></entry-fields>
            </q-card-section>
        </q-form>

        <q-card-actions align="right" class="q-pa-md">
            <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
            <q-btn :label="btnTitle"
                   form="entry-form"
                   type="submit"
                   color="primary"
                   :icon="btnIcon ?? 'las la-plus-circle'"
                   :disable="isLoading"
                   :loading="isLoading"/>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import Modal from '../../components/Modal.vue';
import {EntryCreateRequest, EntryUpdateRequest} from "../../../api/api_types";
import EntryFields from './Entry.Fields.vue';
import {computed} from "vue";

const props = defineProps<{
    label: string
    btnTitle: string
    btnIcon?: string
    isLoading: boolean
    modelValue: EntryCreateRequest | EntryUpdateRequest,
    isShow: boolean,
    isCreate: boolean
}>()

const emit = defineEmits<{
    (e: 'update:modelValue', value: EntryCreateRequest | EntryUpdateRequest): void,
    (e: 'update:isShow', value: boolean): void
    (e: 'submit', value: EntryCreateRequest | EntryUpdateRequest): void
}>();
const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
</script>