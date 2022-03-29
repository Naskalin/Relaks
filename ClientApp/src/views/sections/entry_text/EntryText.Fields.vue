<template>
    <q-input v-if="model.textType === 'Email'" type="email" :label="entryTextMessages.val.names.Email" v-model="model.val"/>
    <q-input v-else-if="model.textType === 'Url'" type="url" :label="entryTextMessages.val.names.Url" v-model="model.val"/>
    <phone-field v-else-if="model.textType === 'Phone'" v-model="model.val"/>
    <q-editor v-else-if="model.textType === 'Note'" v-model="model.val"/>
    <q-banner v-else class="bg-warning text-white">
        <q-icon icon="las la-exclamation-triangle la-fw"/>
        Что то пошло не так, EntryText.textType не определён.
    </q-banner>
    
    <q-input
        v-model="model.title"
        counter
        autogrow
        color="secondary"
        label="Название"
        type="textarea"
    />

    <actual-fieldset v-model="model"></actual-fieldset>
</template>

<script setup lang="ts">
import {CreateEntryTextRequest, UpdateEntryTextRequest} from "../../../api/api_types";
import {computed} from "vue";
import ActualFieldset from '../../fieldsets/Actual.Fieldset.vue';
import PhoneField from '../../fields/Phone.Field.vue';
import {entryTextMessages} from "../../../localize/messages";

const props = defineProps<{
    modelValue: CreateEntryTextRequest | UpdateEntryTextRequest,
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', value: CreateEntryTextRequest | UpdateEntryTextRequest): void
}>()
const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
</script>