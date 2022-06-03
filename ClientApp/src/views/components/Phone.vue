<template>
    <div class="phone-show">
        <span class="q-mr-sm fi" :class="'fi-' + phone.region.toLowerCase()"></span>
        <span>{{ phone.number }}</span>
    </div>
</template>

<script setup lang="ts">
import {InfoPhone} from "../../api/api_types";

const props = defineProps<{
    phone: InfoPhone
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