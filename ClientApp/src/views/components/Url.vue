<template>
    <span>
        <q-icon name="las la-link" :color="iconColor" size="1.4em" class="q-mr-xs"/>
        <a v-if="withLink" :href="url" class="link">{{ url }}</a>
        <span v-else>{{ url }}</span>
        <slot></slot>
    </span>
</template>

<script setup lang="ts">
import {withDefaults} from "vue";

defineProps<{
    iconColor?: string
    url: string
    withLink?: boolean
}>()
</script>