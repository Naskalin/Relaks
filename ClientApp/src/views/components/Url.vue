<template>
    <span>
        <q-icon name="las la-link" :color="iconColor" size="1.4em" class="q-mr-xs"/>
        <a v-if="withLink" :href="url.url" class="link">{{ url }}</a>
        <span v-else>{{ url.url }}</span>
        <slot></slot>
    </span>
</template>

<script setup lang="ts">
import {InfoUrl} from "../../api/api_types";

defineProps<{
    iconColor?: string
    url: InfoUrl
    withLink?: boolean
}>()
</script>