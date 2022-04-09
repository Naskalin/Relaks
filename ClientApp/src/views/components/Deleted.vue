<template>
    <template v-if="deleted.deletedAt">
        <q-card-section class="bg-pink-1">
            <span class="label-caption text-grey-8">{{ deletedMessages.deletedAt }}</span>
            &nbsp;
            <date :date="deleted.deletedAt"></date>
            <div v-if="deleted.deletedReason">
                <span class="label-caption text-grey-8 q-mr-xs">{{ deletedMessages.deletedReason }}</span>
                {{ deleted.deletedReason }}
            </div>
        </q-card-section>
        <q-separator/>
    </template>
</template>

<script setup lang="ts">
import Date from './Date.vue';
import {SoftDeletableType} from "../../api/api_types";
import {deletedMessages} from "../../localize/messages";

defineProps<{
    deleted: SoftDeletableType
}>()
</script>