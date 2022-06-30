<template>
    <q-banner v-if="model.deletedAt" class="bg-pink-1">
        {{deletedMessages.deletedAt}} <date :date="model.deletedAt"/>
        <q-input v-if="model.deletedAt" v-model="model.deletedReason" type="textarea" maxlength="250" counter autogrow :label="deletedMessages.deletedReason"/>
    </q-banner>
</template>

<script setup lang="ts">
import {SoftDeletableType} from "../../api/api_types";
import {computed} from "vue";
import {deletedMessages} from "../../localize/messages";
import Date from '../components/Date.vue';

const props = defineProps<{
    modelValue: SoftDeletableType
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: SoftDeletableType): void
}>()
const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
</script>