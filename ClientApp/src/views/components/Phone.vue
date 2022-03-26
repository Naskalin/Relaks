<template>
    <div class="phone-show q-gutter-x-sm">
        <span class="fi" :class="'fi-' + p.region.toLowerCase()"></span>
        <span>{{ p.number }}</span>
        <actual-tooltip v-if="entryText" :entry-text="entryText">
            <div class="phone-show q-gutter-x-sm">
                <span class="fi" :class="'fi-' + p.region.toLowerCase()"></span>
                <span>{{ p.number }}</span>
            </div>
            
            <q-separator></q-separator>
            
            <div v-if="entryText.about">
                {{entryText.about}}
                <q-separator></q-separator>
            </div>
        </actual-tooltip>
    </div>
</template>

<script setup lang="ts">
import {PhoneType, phoneHelper} from "../../utils/phone_helper";
import {computed, ref} from 'vue';
import {EntryText} from "../../api/api_types";
import ActualTooltip from '../components/Actual.Tooltip.vue';

const props = defineProps<{
    phone?: string | PhoneType
    entryText?: EntryText
}>()

const p = computed((): PhoneType => {
    if (props.entryText) {
        return phoneHelper.toPhone(props.entryText.val);
    }

    if (props.phone) {
        if (typeof props.phone === "string") {
            return phoneHelper.toPhone(props.phone);
        }

        return props.phone;
    }

    throw Error('Phone computed find error');
})
</script>

<style lang="scss">
.phone-show {
    display: inline-flex;
    justify-items: center;
    .fi {
        font-size: 1rem;
        position: relative;
        top: -1px;
    }
}
</style>