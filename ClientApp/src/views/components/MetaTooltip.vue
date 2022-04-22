<template>
    <VTooltip v-bind="props">
        <slot></slot>

        <template #popper>
            <slot name="body"></slot>
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

            <div class="row q-col-gutter-x-md">
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
        </template>
    </VTooltip>
</template>

<script setup lang="ts">
import {TimestampTypes, SoftDeletableType} from "../../api/api_types";

import {timeStampMessages, deletedMessages} from "../../localize/messages";
import Date from '../components/Date.vue';

const props = defineProps<{
    meta: TimestampTypes & SoftDeletableType
}>()
</script>