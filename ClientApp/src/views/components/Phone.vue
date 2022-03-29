<template>
    <div class="phone-show">
        <span class="q-mr-sm fi" :class="'fi-' + p.region.toLowerCase()"></span>
        <span>{{ p.number }}</span>
        <slot></slot>
    </div>
</template>

<script setup lang="ts">
import {phoneHelper, PhoneType} from "../../utils/phone_helper";
import {computed} from "vue";

const props = defineProps<{
    phone: PhoneType | string
}>()

const p = computed((): PhoneType => {
    if (typeof props.phone === "string") {
        return phoneHelper.toPhone(props.phone);
    }

    return props.phone;
})

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