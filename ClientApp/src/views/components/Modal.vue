<template>
    <q-dialog v-model="isShowModel" v-bind="props">
        <q-card style="width: 700px; max-width: 80vw;" :class="{'column full-height': props.fullHeight}">
            <q-card-section class="row items-center">
                <div class="text-h6">{{ title }}</div>
                <q-space/>
                <q-btn icon="close" flat round dense v-close-popup/>
            </q-card-section>
            <slot></slot>
        </q-card>
    </q-dialog>
</template>

<script setup lang="ts">
import {computed, withDefaults} from "vue";

const props = withDefaults(defineProps<{
    fullHeight?: boolean,
    persistent?: boolean
    isShow: boolean,
    title: string
}>(), {
    persistent: false
})

const emit = defineEmits<{
    (e: 'update:isShow', value: boolean): void
    // (e: 'update:modelValue', value: boolean): void
}>();

const isShowModel = computed({
    get: () => props.isShow,
    set: (val) => emit('update:isShow', val)
})
</script>