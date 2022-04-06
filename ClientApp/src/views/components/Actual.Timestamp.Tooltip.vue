<!--<template>-->
<!--    <arrow-tooltip direction="right">-->
<!--        <slot></slot>-->
<!--        -->
<!--        <div>-->
<!--            <small class="q-mr-xs">{{ actualMessages.actualStartAt.name }}</small>-->
<!--            {{ dateHelper.utcFormat(entryText.actualStartAt) }}-->
<!--            <div v-if="entryText.actualStartAtReason">-->
<!--                <small class="q-mr-xs">{{ actualMessages.actualStartAtReason.name }}</small>-->
<!--                {{ entryText.actualStartAtReason }}-->
<!--            </div>-->
<!--        </div>-->

<!--        <q-separator/>-->

<!--        <div v-if="entryText.actualEndAt || entryText.actualEndAtReason">-->
<!--            <template v-if="entryText.actualEndAt">-->
<!--                <small class="q-mr-xs">{{ actualMessages.actualEndAt.name }}</small>-->
<!--                {{ dateHelper.utcFormat(entryText.actualEndAt) }}-->
<!--            </template>-->
<!--            <div v-if="entryText.actualEndAtReason">-->
<!--                <small class="q-mr-xs">{{ actualMessages.actualEndAtReason.name }}</small>-->
<!--                {{ entryText.actualEndAtReason }}-->
<!--            </div>-->
<!--            <q-separator/>-->
<!--        </div>-->
<!--        -->
<!--        <div class="row">-->
<!--            <div class="col text-left">-->
<!--                <small>создано</small>-->
<!--                <br>-->
<!--                <span class="text-grey-5">{{ dateHelper.utcFormat(entryText.createdAt) }}</span>-->
<!--            </div>-->
<!--            <div class="col text-right">-->
<!--                <small>обновлено</small>-->
<!--                <br>-->
<!--                <span class="text-grey-5">{{ dateHelper.utcFormat(entryText.updatedAt) }}</span>-->
<!--            </div>-->
<!--        </div>-->
<!--    </arrow-tooltip>-->
<!--</template>-->

<!--<script setup lang="ts">-->
<!--import {EntryInfo} from "../../api/api_types";-->
<!--// import {actualMessages} from "../../localize/messages";-->
<!--import {dateHelper} from "../../utils/date_helper";-->
<!--import ArrowTooltip from '../components/Arrow.Tooltip.vue';-->

<!--defineProps<{-->
<!--    entryInfo: EntryInfo-->
<!--}>()-->
<!--</script>-->

<!--<style lang="scss">-->
<!--.actual-tooltip {-->
<!--    position: relative;-->
<!--    &:before {-->
<!--        right: 100%;-->
<!--        top: 50%;-->
<!--        content: "";-->
<!--        height: 0;-->
<!--        width: 0;-->
<!--        position: absolute;-->
<!--        pointer-events: none;-->
<!--        border: 30px solid rgba(0, 0, 0, 0);-->
<!--        border-right-color: #000000;-->
<!--        margin-top: -30px;-->
<!--    }-->
<!--    min-width: 300px;-->
<!--    max-width: 500px;-->
<!--    background: $secondary;-->
<!--    font-size: .9rem;-->
<!--    .q-separator {-->
<!--        background: $grey-8;-->
<!--        margin: .5rem 0;-->
<!--    }-->
<!--    small {-->
<!--        text-transform: uppercase;-->
<!--        color: $grey-5;-->
<!--    }-->
<!--}-->
<!--</style>-->