<template>
    <div class="phone-show">
        <span class="q-mr-sm fi" :class="'fi-' + phone.phoneRegion.toLowerCase()"></span>
        <span>{{ phone.phoneNumber }}</span>
        <slot></slot>
    </div>
</template>

<script setup lang="ts">
import {PhoneType} from "../../api/api_types";

const props = defineProps<{
    phone: PhoneType
}>()
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