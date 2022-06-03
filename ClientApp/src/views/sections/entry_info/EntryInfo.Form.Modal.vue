<template>
    <modal v-model:is-show="isShow" :title="title">
        <q-card-section class="q-gutter-y-md" :class="{'max-height-scrollable': entryInfoType === 'Note'}">
            <q-form autocorrect="off"
                    autocapitalize="off"
                    autocomplete="off"
                    spellcheck="false"
                    id="entry-text-form"
                    @submit.prevent="emit('submit', model)"
            >
                <entry-info-fields v-model="model" :entry-info-type="entryInfoType"></entry-info-fields>
            </q-form>
        </q-card-section>

        <q-card-actions align="between" class="q-pa-md">
            <div v-if="!isCreate" class="q-gutter-x-md">
                <q-btn @click="onDelete" flat label="Удалить" icon="las la-trash" text-color="secondary"/>
                <q-btn v-if="model.deletedAt" @click="onRecover" flat label="Восстановить" icon="las la-redo-alt" color="positive"/>
                <q-btn v-else @click="onSoftDelete" flat label="В архив" icon="las la-archive" text-color="negative"/>
            </div>
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
import {EntryInfoFormRequest, EntryInfoType} from "../../../api/api_types";
import {computed} from "vue";
import {useQuasar} from "quasar";

const props = defineProps<{
    modelValue: EntryInfoFormRequest,
    entryInfoType: EntryInfoType,
    isCreate: boolean,
    isShow: boolean,
    title: string
    btnTitle: string
    btnIcon?: string
    isLoading: boolean
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', value: EntryInfoFormRequest): void
    (e: 'update:isShow', value: boolean): void
    (e: 'delete'): void
    (e: 'softDelete'): void
    (e: 'recover'): void
    (e: 'submit', value: any): void
}>()

const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})

// delete
const $q = useQuasar();
const onDelete = () => {
    $q.dialog({
        title: 'Полное удаление!',
        message: 'Удаляем, всё верно?',
        class: 'bg-negative text-white',
        cancel: true,
        persistent: true
    }).onOk(() => {
        emit('delete');
    })
}

const onSoftDelete = () => {
    $q.dialog({
        title: 'Архивация',
        message: deletedMessages.deletedReason + ' (не обязательно)',
        cancel: true,
        persistent: true,
        prompt: {
            model: '',
            type: 'textarea',
            attrs: {rows: 2},
            maxlength: 250,
            counter: true,
        },
    }).onOk(data => {
        model.value.deletedAt = new Date().toISOString();
        model.value.deletedReason = data;
        emit('softDelete');
    })
}

const onRecover = () => {
    model.value.deletedAt = null;
    model.value.deletedReason = '';
    emit('recover');
}
</script>

<style lang="scss">
    .max-height-scrollable {
        max-height: 80vh;
        overflow-y: scroll;
    }
</style>