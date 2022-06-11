<template>
    <q-select
        v-model="model"
        :options="arrOptions"
        @filter="filterFn"
        use-input
        use-chips
        :multiple="props.multiple"
        input-debounce="0"
        new-value-mode="add-unique"
        :label="props.label"
    />
</template>

<script setup lang="ts">
import {computed, ref} from 'vue';

const props = defineProps<{
    multiple: boolean,
    modelValue: any,
    label: string,
    options: string[]
}>();

const emit = defineEmits<{
    (e: 'update:modelValue', value: any): void,
}>();
const arrOptions = ref<string[]>(props.options);

const model = computed({
    get: () => props.modelValue,
    set: (val?: any) => {
        let emitVal;
        
        if (!props.multiple) {
            emitVal = val ?? '';
        } else {
            emitVal = val ?? [];
        }
        
        emit('update:modelValue', emitVal)
    }
})

const filterFn = (val: string, update: CallableFunction) => {
    update(() => {
        const needle = val.toLowerCase()
        arrOptions.value = props.options.filter(v => v.toLowerCase().indexOf(needle) > -1)
    })
}
</script>