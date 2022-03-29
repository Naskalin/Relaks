<template>

    
    <div class="row">
        <div class="col-1">
            <q-select
                v-model="model"
                use-input
                label="Регион"
                :options="countries"
                @filter="filterFn"
            >
                <template v-slot:option="scope">
                    <q-item v-bind="scope.itemProps">
                        <q-item-section avatar>
                            {{scope}}
<!--                            <q-icon :name="scope.opt.icon" />-->
                        </q-item-section>
                        <q-item-section>
                            <q-item-label>{{ scope.opt.native }}</q-item-label>
                            <q-item-label caption>{{ scope.opt.name }}{{ scope.opt.capital ? ' - ' + scope.opt.capital : '' }}</q-item-label>
                        </q-item-section>
                    </q-item>
                </template>
            </q-select>
        </div>
        <div class="col-2">
            <q-input v-model="model.number" label="Номер"/>
        </div>
    </div>

    <!--    <p>{{russia.emoji.toUpperCase()}}</p>-->
    <p><span class="q-mr-sm fi" :class="'fi-ru'"></span></p>

    <div v-for="(c, key) in countries">
        <p>
            <span class="q-mr-sm fi" :class="'fi-' + key.toLowerCase()"></span>
            +{{ c.phone }} {{ c.native }} ({{ c.name }}{{ c.capital ? ' - ' + c.capital : '' }})
        </p>
    </div>
</template>

<script setup lang="ts">
import {countries} from "countries-list";
import {computed, ref} from "vue";
import {phoneHelper, PhoneType} from "../../utils/phone_helper";

const props = defineProps<{
    modelValue: string
}>()

const emit = defineEmits<{
    (e: 'update:modelValue', val: string)
}>()

// const countryOptions = computed(() => {
//     // countries
//     for (const [region, value] of Object.entries(countries))
//     {
//         arr.push({value: key, label: value})
//     }
// })
const stringOptions = ['Google', 'Facebook', 'Twitter', 'Apple', 'Oracle'];
const options = ref(stringOptions);

const model = computed({
    get: (): PhoneType => {
        if (props.modelValue === '') {
            return {region: 'RU', number: ''};
        }
        
        return phoneHelper.toPhone(props.modelValue);
    },
    set: (val: PhoneType) => {
        return emit('update:modelValue', phoneHelper.toString(val));
    }
})

const filterFn = (val, update) => {
    if (val === '') {
        update(() => {
            options.value = stringOptions
        })
        return
    }

    update(() => {
        const needle = val.toLowerCase()
        options.value = stringOptions.filter(v => v.toLowerCase().indexOf(needle) > -1)
    })
}
</script>