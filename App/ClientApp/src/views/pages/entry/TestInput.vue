<template>
  <q-input v-model="value" @update:model-value="$emit('input', $event)"></q-input>
</template>

<script setup lang="ts">
const props = defineProps<{
  value?: string
}>();
</script>