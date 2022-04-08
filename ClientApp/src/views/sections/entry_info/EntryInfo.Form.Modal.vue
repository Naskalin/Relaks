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
            <template v-if="!isCreate">
                <div v-if="model.deletedAt" class="q-gutter-x-md">
                    <q-btn @click="onDelete" flat label="Удалить" icon="las la-trash"
                           text-color="negative"/>
                    <q-btn @click="onRecover" flat label="Восстановить" icon="las la-redo-alt" color="positive"/>
                </div>
                <q-btn v-else @click="onArchive" flat label="В архив" icon="las la-archive" text-color="negative"/>
            </template>
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
import {deletedMessages} from "../../../localize/messages";
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
    (e: 'archive'): void
    (e: 'recover'): void
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
        message: 'Удаляем из архива, всё верно?',
        cancel: true,
        persistent: true
    }).onOk(() => {
        emit('delete');
    })
}

const onArchive = () => {
    $q.dialog({
        title: 'Архивация',
        message: deletedMessages.deletedReason + ' (не обязательно)',
        cancel: true,
        persistent: true,
        prompt: {
            model: '',
            type: 'textarea',
            maxlength: 250,
            counter: true,
        },
    }).onOk(data => {
        model.value.deletedReason = data;
        emit('archive');
    })
}

const onRecover = () => {
    model.value.deletedAt = null;
    model.value.deletedReason = '';
    emit('recover');
}
</script>