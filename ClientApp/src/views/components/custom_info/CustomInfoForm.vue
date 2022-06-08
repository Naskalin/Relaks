<template>
    <div class="q-my-md">
        <q-btn label="Добавить группу" icon="las la-plus-circle" color="secondary" @click="addGroup"/>
        &nbsp;
        <q-btn @click="isMinimizeGroups = !isMinimizeGroups"
               :label="isMinimizeGroups ? 'Раскрыть группы' : 'Минимизировать группы'"
               :icon="isMinimizeGroups ? 'las la-eye' : 'las la-eye-slash'"
               outline
               color="positive"/>
    </div>
    
    <draggable
        v-model="model.groups"
        handle=".draggable-group-btn"
        :itemKey="new String()"
    >
        <template #item="{element, index}">
            <q-card class="q-my-lg">
                <q-card-section>
                    <custom-info-group 
                        v-model="model.groups[index]"
                        :is-minimize="isMinimizeGroups"
                        @delete="removeGroup(index)"
                    />
                </q-card-section>
            </q-card>
        </template>
    </draggable>
    
    <div class="q-my-lg flex justify-between">
        <q-btn label="Сохранить набор данных" @click="emit('save', model)" icon="las la-save" color="primary"/>
        <q-btn color="negative" @click="onDelete" label="Удалить набор данных" icon="las la-trash" outline/>
    </div>
</template>

<script setup lang="ts">
import CustomInfoGroup from './CustomInfoGroup.vue';
import draggable from 'vuedraggable';

import {CustomInfo} from "../../../api/api_types";
import {computed, onMounted, ref, reactive} from 'vue';
import {useQuasar} from "quasar";

const $q = useQuasar();
const props = defineProps<{
    modelValue: CustomInfo
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: CustomInfo): void,
    (e: 'save', val: CustomInfo): void
    (e: 'delete', val: CustomInfo): void
}>();

const model = computed({
    get: () => props.modelValue,
    set: (val: CustomInfo) => emit('update:modelValue', val),
});

onMounted(() => {
    if (!model.value.groups.length) addGroup();
})

const addGroup = () => {
    model.value.groups.unshift({
        title: '',
        items: [{key: '', value: ''}]
    })
}
const removeGroup = (index: number) => {
    model.value.groups.splice(index, 1);
}
const onDelete = () => {
    $q.dialog({
        title: 'Полное удаление!',
        message: 'Удаляем, всё верно?',
        class: 'bg-negative text-white',
        cancel: true,
        persistent: true
    }).onOk(() => {
        emit('delete', model.value);
    })
}
const isMinimizeGroups = ref(false);
</script>