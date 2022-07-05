<template>
    <q-form @submit.prevent="val => emit('save', val)">
        <q-input v-model="formStore.model.title" filled label="Название набора данных" autogrow counter maxlength="250"/>
        <custom-info-fields v-model="formStore.model.info" v-bind="$attrs"/>

        <div class="q-my-lg flex justify-between">
            <q-btn label="Сохранить набор данных" type="submit" @click="emit('save', formStore.model)" icon="las la-save" color="primary"/>
            <q-btn
                v-if="withDeleteBtn"
                color="negative" @click="onDelete" label="Удалить набор данных" icon="las la-trash"/>
        </div>
    </q-form>
</template>

<script setup lang="ts">
import CustomInfoFields from '../../../components/custom_info/CustomInfoFields.vue';
import {useEntryInfoCustomFormStore} from "./entry_info_custom_form_store";
import {useQuasar} from "quasar";

const $q = useQuasar();
const formStore = useEntryInfoCustomFormStore();
const props = defineProps<{
    withDeleteBtn?: boolean
}>()
const emit = defineEmits<{
    (e: 'save', val: any): void,
    (e: 'delete', val: any): void,
}>();
const onDelete = () => {
    if (!props.withDeleteBtn) return;
    $q.dialog({
        title: 'Полное удаление!',
        message: 'Удаляем, всё верно?',
        class: 'bg-negative text-white',
        cancel: true,
        persistent: true
    }).onOk(() => {
        emit('delete', formStore.model);
    })
}
</script>