<template>
    <div>
        <div class="row q-col-gutter-md">
            <div class="col-4">
                <date-field v-model="model.actualStartAt" :required="true" :label="actualMessages.actualStartAt.name"/>
            </div>
            <div class="col-8">
                <q-input
                    v-model="model.actualStartAtReason"
                    counter
                    autogrow
                    color="secondary"
                    :label="actualMessages.actualStartAtReason.name"
                    type="textarea"
                />
            </div>
        </div>

        <div class="row q-col-gutter-md">
            <div class="col-4">
                <date-field v-model="model.actualEndAt" :label="actualMessages.actualEndAt.name"/>
            </div>
            <div class="col-8">
                <q-input
                    v-model="model.actualEndAtReason"
                    counter
                    autogrow
                    color="secondary"
                    :label="actualMessages.actualEndAtReason.name"
                    type="textarea"
                />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import {ActualTypes} from "../../api/api_types";
import DateField from '../fields/Date.Field.vue';
import {actualMessages} from "../../localize/messages";
import {computed} from "vue";

const emit = defineEmits<{
    (e: 'update:modelValue', value: ActualTypes): void
}>();

const props = defineProps<{
    modelValue: ActualTypes
}>()

const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})

</script>