<template>
    <modal v-model:is-show="isShow" title="Изменение файла">
        <q-card-section>
            <q-form
                autocorrect="off"
                autocapitalize="off"
                autocomplete="off"
                spellcheck="false"
                id="file-edit-form-modal"
                @submit.prevent="emit('submit', model)"
            >
                <q-input
                    v-model="model.name"
                    counter
                    color="secondary"
                    :label="fileFieldNames.name"
                    type="text"
                />

                <category-field
                    v-model="model.category"
                    :options="filesMeta.categories" 
                    label="Категория"/>                
            </q-form>
        </q-card-section>

        <q-card-actions align="between" class="q-pa-md">
            <div class="q-gutter-x-md">
                <q-btn @click="onDelete" flat label="Удалить" icon="las la-trash" text-color="secondary"/>
                <q-btn v-if="model.deletedAt" @click="onRecover" flat label="Восстановить" icon="las la-redo-alt" color="positive"/>
                <q-btn v-else @click="onSoftDelete" flat label="В архив" icon="las la-archive" text-color="negative"/>
            </div>
            <div class="q-gutter-x-md">
                <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
                <q-btn label="Сохранить"
                       form="file-edit-form-modal"
                       type="submit"
                       color="primary"
                       icon="las la-save"
                       :disable="isLoading"
                       :loading="isLoading"/>
            </div>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import Modal from '../../components/Modal.vue';
import CategoryField from '../../fields/Category.Field.vue';
import {EntryFileMeta, FileModelUpdateRequest} from "../../../api/api_types";
import {deletedMessages, fileFieldNames} from "../../../localize/messages";
import {computed} from "vue";
import {useQuasar} from "quasar";

const props = defineProps<{
    modelValue: FileModelUpdateRequest,
    filesMeta: EntryFileMeta
    isShow: boolean
    isLoading: boolean
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', value: FileModelUpdateRequest): void
    (e: 'update:isShow', value: boolean): void
    (e: 'submit', value: FileModelUpdateRequest): void
    (e: 'recover'): void
    (e: 'softDelete'): void
    (e: 'delete'): void
}>()
const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
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