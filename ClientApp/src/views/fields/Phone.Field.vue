<template>
    <div class="flex q-gutter-x-md">
        <q-select
            :model-value="model.region"
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
                <div v-if="model.region">
                    <span class="q-mr-sm fi" :class="'fi-' + model.region.toLowerCase()"></span>
                    <span class="text-grey-8">{{model.region}}</span>
                </div>
                <q-badge v-else>*none*</q-badge>
            </template>
        </q-select>

        <q-input style="width: 200px" v-model="model.number" label="Номер телефона"/>
    </div>
</template>

<script setup lang="ts">
import {countries} from "countries-list";
import {ref, computed, onMounted} from "vue";
import {appDefaults} from "../../app_defaults";
import {PhoneInfo} from "../../api/api_types";

type SelectItem = {
    value: string,
    label: string
}
const props = defineProps<{
    modelValue: PhoneInfo
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: PhoneInfo): void
}>()

const model = computed({
    get: () => props.modelValue,
    set: (val) => emit('update:modelValue', val)
})
onMounted(() => {
    if (model.value.region === '') model.value.region = appDefaults.phoneRegion;
})

const onSelectCountry = (val: SelectItem) => {
    model.value.region = val.value;
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