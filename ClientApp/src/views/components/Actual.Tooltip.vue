<template>
    <q-tooltip class="actual-tooltip" anchor="center right" self="center left" :offset="[5, 10]" size="2rem">
        <slot></slot>
        
        <div>
            <small class="q-mr-xs">{{ actualMessages.actualStartAt.name }}</small>
            {{ dateHelper.utcFormat(entryText.actualStartAt) }}
            <div v-if="entryText.actualStartAtReason">
                <small class="q-mr-xs">{{ actualMessages.actualStartAtReason.name }}</small>
                {{ entryText.actualStartAtReason }}
            </div>
        </div>

        <q-separator/>

        <div v-if="entryText.actualEndAt || entryText.actualEndAtReason">
            <template v-if="entryText.actualEndAt">
                <small class="q-mr-xs">{{ actualMessages.actualEndAt.name }}</small>
                {{ dateHelper.utcFormat(entryText.actualEndAt) }}
            </template>
            <div v-if="entryText.actualEndAtReason">
                <small class="q-mr-xs">{{ actualMessages.actualEndAtReason.name }}</small>
                {{ entryText.actualEndAtReason }}
            </div>
            <q-separator/>
        </div>
        
        <div class="row">
            <div class="col text-left">
                <small>создано</small>
                <br>
                <span class="text-grey-5">{{ dateHelper.utcFormat(entryText.createdAt) }}</span>
            </div>
            <div class="col text-right">
                <small>обновлено</small>
                <br>
                <span class="text-grey-5">{{ dateHelper.utcFormat(entryText.updatedAt) }}</span>
            </div>
        </div>
    </q-tooltip>
</template>

<script setup lang="ts">
import {EntryText} from "../../api/api_types";
import {actualMessages} from "../../localize/messages";
import {dateHelper} from "../../utils/date_helper";

defineProps<{
    entryText: EntryText
}>()
</script>

<style lang="scss">
.actual-tooltip {
    max-width: 400px;
    background: $secondary;
    font-size: .9rem;
    .q-separator {
        background: $grey-8;
        margin: .5rem 0;
    }
    small {
        text-transform: uppercase;
        color: $grey-5;
    }
}
</style>