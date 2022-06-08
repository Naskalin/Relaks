<template>
    <span>
        <q-icon name="las la-envelope" :color="iconColor" size="1.4em" class="q-mr-xs"/>
        <a v-if="withLink" :href="'mailto:' + email.email" class="link">{{ email.email }}</a>
        <span v-else>{{ email.email }}</span>
        <slot></slot>
    </span>
</template>

<script setup lang="ts">
import {EmailInfo} from "../../api/api_types";

defineProps<{
    iconColor?: string
    email: EmailInfo
    withLink?: boolean
}>()
</script>