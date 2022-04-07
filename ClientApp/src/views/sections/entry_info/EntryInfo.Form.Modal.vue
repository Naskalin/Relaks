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
                <entry-info-fields v-model="model" :entry-info-type="entryInfoType"></entry-info-fields>
            </q-card-section>
        </q-form>

        <q-card-actions align="between" class="q-pa-md">
            <q-btn v-if="!isCreate" @click="onDelete" flat label="Удалить" icon="las la-trash" text-color="negative"/>
            <div class="q-gutter-x-md" :class="{'q-ml-auto': isCreate}">
                <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
                <q-btn :label="btnTitle"
                       form="entry-text-form"
                       type="submit"
                       color="primary"
                       :icon="btnIcon ?? 'las la-plus-circle'"
                       :disable="isLoading"
                       :loading="isLoading"/>
            </div>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import EntryInfoFields from './EntryInfo.Fields.vue';
import Modal from '../../components/Modal.vue';
import {
    EntryNoteFormRequest,
    EntryEmailFormRequest,
    EntryUrlFormRequest,
    EntryDateFormRequest,
    EntryPhoneFormRequest,
    EntryInfoType
} from "../../../api/api_types";
import {computed} from "vue";
import {useQuasar} from "quasar";

const props = defineProps<{
    modelValue: EntryNoteFormRequest | EntryEmailFormRequest | EntryUrlFormRequest | EntryDateFormRequest | EntryPhoneFormRequest,
    entryInfoType: EntryInfoType,
    isCreate: boolean,
    isShow: boolean,
    title: string
    btnTitle: string
    btnIcon?: string
    isLoading: boolean
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', value: EntryNoteFormRequest | EntryEmailFormRequest | EntryUrlFormRequest | EntryDateFormRequest | EntryPhoneFormRequest): void
    (e: 'update:isShow', value: boolean): void
    (e: 'delete'): void
    (e: 'submit', value: EntryNoteFormRequest | EntryEmailFormRequest | EntryUrlFormRequest | EntryDateFormRequest | EntryPhoneFormRequest): void
}>()
const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})

// delete
const $q = useQuasar();
const onDelete = () => {
    $q.dialog({
        title: 'Подтверждение удаления',
        message: 'Удаляем, всё верно?',
        cancel: true,
        persistent: true
    }).onOk(() => {
        emit('delete');
    })
}
</script>