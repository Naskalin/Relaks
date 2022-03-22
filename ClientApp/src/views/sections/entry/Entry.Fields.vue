<template>
    <q-btn-toggle
        v-if="!model?.id"
        class="bg-grey-2"
        v-model="model.entryType"
        spread
        toggle-color="primary"
        :options="entryMessages.entryType.selectOptions"
    />

    <q-input color="secondary" v-model="model.name" required="required" :label="entryMessages.name[model.entryType]"/>

    <div>
        <p class="q-mb-sm text-secondary">
            Рейтинг
            <span class="text-white bg-grey-8 q-pa-xs rounded-borders">{{ model.reputation }}</span>
        </p>
        <q-rating :max="10"
                  v-model="model.reputation"
                  color="secondary"
                  icon="lar la-star"
                  icon-selected="las la-star"
                  size="2em">
        </q-rating>
    </div>

    <q-input
        v-model="model.description"
        counter
        autogrow
        color="secondary"
        label="Краткое описание"
        :hint="'Пример: ' + entryMessages.entryType.descriptionHelpers[model.entryType]"
        type="textarea"
    />
    
    <div class="row">
        <div class="col">
            <date-field v-model="model.startAt" :label="entryMessages.startAt[model.entryType]"></date-field>
        </div>
        <div class="col">
            <date-field v-model="model.endAt" :label="entryMessages.endAt[model.entryType]"></date-field>
        </div>
    </div>

    <actual-fieldset v-model="model"></actual-fieldset>
</template>

<script setup lang="ts">
import {CreateEntryRequest, UpdateEntryRequest} from "../../../api/rerources/api_entry";
import DateField from '../../fields/Date.Field.vue';
import {computed} from "vue";
import {entryMessages} from "../../../localize/messages";
import ActualFieldset from '../../fieldsets/Actual.Fieldset.vue';

const props = defineProps<{
    modelValue: CreateEntryRequest | UpdateEntryRequest
}>()

const emit = defineEmits<{
    (e: 'update:modelValue', value: CreateEntryRequest | UpdateEntryRequest): void
}>()

const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
</script>