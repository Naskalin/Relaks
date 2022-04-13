<template>
    <q-btn-toggle v-if="isCreate"
                  class="bg-grey-2"
                  v-model="model.entryType"
                  spread
                  toggle-color="primary"
                  :options="entryMessages.entryType.selectOptions"
    />
    <q-chip v-else color="grey-5">{{ entryMessages.entryType.names[model.entryType] }}</q-chip>

    <q-input type="text" color="secondary" v-model="model.name" required="required" :label="entryMessages.name[model.entryType]"/>

    <div>
        <p class="q-mb-sm text-secondary">
            {{ entryMessages.reputation }}
            <span class="text-white bg-grey-7 q-py-xs q-px-sm rounded-borders">{{ model.reputation }}</span>
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

    <div>
        <div class="row q-col-gutter-md">
            <div class="col">
                <date-field v-model="model.startAt" :label="entryMessages.startAt[model.entryType]"></date-field>
            </div>
            <div class="col">
                <date-field v-model="model.endAt" :label="entryMessages.endAt[model.entryType]"></date-field>
            </div>
        </div>
    </div>

<!--    <actual-fieldset v-model="model"></actual-fieldset>-->
</template>

<script setup lang="ts">
import {EntryCreateRequest, EntryUpdateRequest} from "../../../api/api_types";
import DateField from '../../fields/Date.Field.vue';
import {computed} from "vue";
import {entryMessages} from "../../../localize/messages";
// import ActualFieldset from '../../fieldsets/Actual.Fieldset.vue';

const props = defineProps<{
    modelValue: EntryCreateRequest | EntryUpdateRequest,
    isCreate: boolean,
}>()

const emit = defineEmits<{
    (e: 'update:modelValue', value: EntryCreateRequest | EntryUpdateRequest): void
}>()

const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
</script>