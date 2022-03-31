<template>
    <div class="flex q-gutter-x-md">
        <q-select
            :model-value="country"
            @update:model-value="onSelectCountry"
            use-input
            input-debounce="0"
            label="Регион"
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
                <div v-if="country">
                    <span class="q-mr-sm fi" :class="'fi-' + country.toLowerCase()"></span>
<!--                    <span class="text-grey-8">+{{ countries[country].phone }} | {{country}}</span>-->
                    <span class="text-grey-8">{{country}}</span>
                </div>
                <q-badge v-else>*none*</q-badge>
            </template>
        </q-select>

        <q-input style="width: 200px" :model-value="number" @update:model-value="onUpdateNumber" label="Номер"/>
    </div>
</template>

<script setup lang="ts">
import {countries} from "countries-list";
import {ref, watch} from "vue";
import {phoneHelper} from "../../utils/phone_helper";
import {appDefaults} from "../../app_defaults";

type SelectItem = {
    value: string,
    label: string
}
const props = defineProps<{
    modelValue: string
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', val: string): void
}>()

const country = ref(appDefaults.phoneRegion);
const number = ref('');
watch(() => props.modelValue, (val) => {
    if (val === ('')) {
        return;
    }

    const phone = phoneHelper.toPhone(val);
    country.value = phone.region.toUpperCase();
    number.value = phone.number;
}, {immediate: true})

const onSelectCountry = (val: SelectItem) => {
    country.value = val.value;
    emitModel();
}
const onUpdateNumber = (val: string) => {
    number.value = val;
    emitModel();
}
const emitModel = () => {
    let emitStr = phoneHelper.toString({
        region: country.value,
        number: number.value
    });
    
    if (number.value === '') {
        emitStr = '';
    }
    
    emit('update:modelValue', emitStr);
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