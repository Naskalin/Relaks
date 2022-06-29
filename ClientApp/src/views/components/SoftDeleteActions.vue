<template>
    <q-btn @click="onDelete" flat label="Удалить" icon="las la-trash" text-color="secondary"/>
    <q-btn v-if="model.deletedAt" @click="onRecover" flat label="Восстановить" icon="las la-redo-alt" color="positive"/>
    <q-btn v-else @click="onSoftDelete" flat label="В архив" icon="las la-archive" text-color="negative"/>
</template>

<script setup lang="ts">
    import {SoftDeletableType} from "../../api/api_types";
    import {computed} from 'vue';
    import {useQuasar} from "quasar";
    import {deletedMessages} from "../../localize/messages";

    const props = defineProps<{
        modelValue: SoftDeletableType
    }>()
    const emit = defineEmits<{
        (e: 'update:modelValue', val: SoftDeletableType): void
        (e: 'delete'): void
        (e: 'softDelete'): void
        (e: 'recover'): void
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