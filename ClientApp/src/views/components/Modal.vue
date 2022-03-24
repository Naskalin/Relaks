<template>
  <q-dialog v-model="isShowModel" persistent>
    <q-card style="width: 700px; max-width: 80vw;">
      <q-card-section class="row items-center card-header">
        <div class="text-h6">{{title}}</div>
        <q-space/>
        <q-btn icon="close" flat round dense v-close-popup/>
      </q-card-section>

      <slot></slot>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import {computed} from "vue";

const props = defineProps<{
    isShow: boolean,
    title: string
}>()

const emit = defineEmits<{
    (e: 'update:isShow', value: boolean): void
    // (e: 'update:modelValue', value: boolean): void
}>();

const isShowModel = computed({
    get: () => props.isShow,
    set: (val) => emit('update:isShow', val)
})
</script>