<template>
    <span>
        <q-icon name="las la-envelope" :color="iconColor" size="1.4em" class="q-mr-xs"/>
        <a v-if="withLink" :href="'mailto:' + email" class="link">{{ email }}</a>
        <span v-else>{{ email }}</span>
        <slot></slot>
    </span>
</template>

<script setup lang="ts">
import {withDefaults} from "vue";

withDefaults(defineProps<{
    iconColor?: string
    email: string
    withLink?: boolean
}>(), {
    iconColor: 'secondary'
})
</script>