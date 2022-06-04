<template>
    <div class="phone-show">
        <span class="q-mr-sm fi" :class="'fi-' + phone.region.toLowerCase()"></span>
        <span>{{numberFormat}}</span>
    </div>
</template>

<script setup lang="ts">
import parsePhoneNumber, {PhoneNumber} from 'libphonenumber-js';
import {InfoPhone} from "../../api/api_types";
import {computed} from 'vue';

const props = defineProps<{
    phone: InfoPhone
}>()

const numberFormat = computed(() => {
    const phoneNumber = parsePhoneNumber(props.phone.number, props.phone.region.toUpperCase() as any)
    if (phoneNumber && phoneNumber.isValid()) {
        return phoneNumber.formatNational();
    }
    
    return '';
});
</script>

<style lang="scss">
.phone-show {
    .fi {
        margin-right: .5rem;
        font-size: 1rem;
        position: relative;
        top: -1px;
    }
}
</style>