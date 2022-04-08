<template>
    <arrow-tooltip :direction="direction">
        <slot></slot>
        <template v-if="meta.deletedAt">
            <div class="q-pa-sm text-pink-4">
                <small class="q-mr-sm">{{ deletedMessages.deletedAt }}</small>
                <date :date="meta.deletedAt"></date>
                <div v-if="meta.deletedReason">
                    <small class="q-mr-sm">{{ deletedMessages.deletedReason }}</small>
                    {{ meta.deletedReason }}
                </div>
            </div>
            <q-separator/>
        </template>

        <div class="row">
            <div class="col text-left">
                <small>{{timeStampMessages.createdAt}}</small>
                <br>
                <date :date="meta.createdAt"></date>
            </div>
            <div class="col text-right">
                <small>{{timeStampMessages.updatedAt}}</small>
                <br>
                <date :date="meta.updatedAt"></date>
            </div>
        </div>
    </arrow-tooltip>
</template>

<script setup lang="ts">
import {TimestampTypes, SoftDeletableType} from "../../api/api_types";
import {withDefaults} from "vue";

import {timeStampMessages, deletedMessages} from "../../localize/messages";
import ArrowTooltip from '../components/Arrow.Tooltip.vue';
import Date from '../components/Date.vue';

withDefaults(defineProps<{
    direction?: 'left' | 'right' | 'top' | 'bottom',
    meta: TimestampTypes & SoftDeletableType
}>(), {
    direction: 'right'
})
</script>