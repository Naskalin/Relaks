<template>
    <q-item class="q-pa-sm" v-bind="$attrs">
        <slot name="start"></slot>
        <q-item-section side>
            <q-avatar size="50px" color="grey-5">
                <api-image v-if="entry.avatar" :file-id="entry.avatar" image-filter="square-thumbnail"/>
                <q-icon v-else name="las la-question-circle" color="grey-4" size="34px"/>
            </q-avatar>
        </q-item-section>
        <q-item-section>
            <q-item-label>{{entry.name}}</q-item-label>
            <slot name="body"></slot>
        </q-item-section>
        <slot name="end"></slot>
    </q-item>
</template>

<script setup lang="ts">
import ApiImage from './ApiImage.vue';
import {Entry} from "../../api/api_types";
const props = defineProps<{
    entry: Entry
}>()
</script>