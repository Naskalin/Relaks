<template>
    <div class="q-my-md q-gutter-x-md">
        <q-btn label="Добавить группу в начало" icon="las la-plus-circle" outline color="secondary" @click="unshiftGroup"/>
        <q-btn
            v-if="model.groups.length"
            @click="isMinimizeGroups = !isMinimizeGroups"
            :label="isMinimizeGroups ? 'Развернуть группы' : 'Свернуть группы'"
            :icon="isMinimizeGroups ? 'las la-eye-slash' : 'las la-eye'"
            outline
            color="positive"/>
        <slot name="custom-info-actions"/>
    </div>
    
    <draggable
        v-model="model.groups"
        handle=".draggable-group-btn"
        :itemKey="randomId()"
    >
        <template #item="{element, index}">
            <q-card class="q-my-lg">
                <q-card-section>
                    <custom-info-group 
                        v-model="model.groups[index]"
                        :is-minimize="isMinimizeGroups"
                        :groups-length="model.groups.length"
                        @delete="removeGroup(index)"
                    />
                </q-card-section>
            </q-card>
        </template>
    </draggable>

    <div class="q-my-md">
        <q-btn label="Добавить группу в конец" icon="las la-plus-circle" outline color="secondary" @click="pushGroup"/>
        &nbsp;
        <q-btn
            v-if="model.groups.length"
            @click="isMinimizeGroups = !isMinimizeGroups"
            :label="isMinimizeGroups ? 'Развернуть группы' : 'Свернуть группы'"
            :icon="isMinimizeGroups ? 'las la-eye-slash' : 'las la-eye'"
            outline
            color="positive"/>
    </div>
</template>

<script setup lang="ts">
import CustomInfoGroup from './CustomInfoGroup.vue';
import draggable from 'vuedraggable';

import {CustomInfo} from "../../../api/api_types";
import {computed, onMounted, ref, reactive} from 'vue';
import {randomId} from "../../../utils/file_helper";

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
    if (!model.value.groups.length) pushGroup();
})

const pushGroup = () => {
    model.value.groups.push({
        title: '',
        items: [{key: '', value: ''}]
    })
}
const unshiftGroup = () => {
    model.value.groups.unshift({
        title: '',
        items: [{key: '', value: ''}]
    })
}
const removeGroup = (index: number) => {
    model.value.groups.splice(index, 1);
}
const isMinimizeGroups = ref(false);
</script>