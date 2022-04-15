<template>
    <div class="flex q-gutter-x-md">
        <q-select
            :model-value="model.phoneRegion"
            @update:model-value="onSelectCountry"
            use-input
            input-debounce="0"
            label="Регион телефона"
            :options="selectOptions"
            @filter="filterFn"
            style="width: 150px"
            behavior="dialog"
        >
            <template v-slot:no-option>
                <q-item>
                    <q-item-section class="text-grey">
                        Ничего не найдено
                    </q-item-section>
                </q-item>
            </template>
            <template v-slot:selected>
                <div v-if="model.phoneRegion">
                    <span class="q-mr-sm fi" :class="'fi-' + model.phoneRegion.toLowerCase()"></span>
                    <span class="text-grey-8">{{model.phoneRegion}}</span>
                </div>
                <q-badge v-else>*none*</q-badge>
            </template>
        </q-select>

        <q-input style="width: 200px" v-model="model.phoneNumber" label="Номер телефона"/>
    </div>
</template>

<script setup lang="ts">
import {countries} from "countries-list";
import {ref, computed, onMounted} from "vue";
import {appDefaults} from "../../app_defaults";
import {PhoneType} from "../../api/api_types";

type SelectItem = {
    value: string,
    label: string
}
const props = defineProps<{
    modelValue: PhoneType
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: PhoneType): void
}>()

const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
onMounted(() => {
    if (model.value.phoneRegion === '') model.value.phoneRegion = appDefaults.phoneRegion;
})

const onSelectCountry = (val: SelectItem) => {
    model.value.phoneRegion = val.value;
}

const allCountries: SelectItem[] = [];
for (const [region, value] of Object.entries(countries)) {
    const desc = [];
    if (value.name) desc.push(value.name);
    if (value.capital) desc.push(value.capital);

    allCountries.push({
        value: region,
        label: '+' + value.phone + ' - ' + value.native + ' (' + desc.join(' - ') + ')',
    })
}
const selectOptions = ref(allCountries);
const filterFn = (val: string, update: any) => {
    if (val === '') {
        update(() => {
            selectOptions.value = allCountries;
        })
        return
    }
    update(() => {
        const needle = val.toLowerCase()
        selectOptions.value = allCountries.filter((v: SelectItem) => {
            return v.label.toLowerCase().indexOf(needle) > -1 || v.value.toLowerCase().indexOf(needle) > -1;
        })
    })
}
</script>