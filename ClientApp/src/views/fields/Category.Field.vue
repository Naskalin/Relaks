<template>
    <q-select
        v-model="model"
        :options="arrOptions"
        @filter="filterFn"
        use-input
        use-chips
        input-debounce="0"
        new-value-mode="add-unique"
        :label="props.label"
    />
</template>

<script setup lang="ts">
import {computed, ref} from 'vue';

const props = defineProps<{
    modelValue: string,
    label: string,
    options: string[]
}>();

const emit = defineEmits<{
    (e: 'update:modelValue', value: string): void,
}>();
const arrOptions = ref<string[]>(props.options);

const model = computed({
    get: () => props.modelValue,
    set: (val?: string) => {
        console.log('>> update val', val);
        emit('update:modelValue', val ?? '')
    }
})

const filterFn = (val: string, update: CallableFunction) => {
    update(() => {
        const needle = val.toLowerCase()
        arrOptions.value = props.options.filter(v => v.toLowerCase().indexOf(needle) > -1)
    })
}
// const createValue = (val: string, done: CallableFunction) => {
//     done(val);
// }
</script>