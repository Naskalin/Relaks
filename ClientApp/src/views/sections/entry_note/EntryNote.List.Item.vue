<template>
    <q-card style="min-width: 600px">
        <template v-if="eText.title">
            <q-card-section class="q-py-sm">
                <div class="text-h6">{{ eText.title }}</div>
            </q-card-section>
            <q-separator/>
        </template>
        <q-card-section v-html="eText.note"></q-card-section>
<!--        <template v-if="eText.actualStartAtReason">-->
<!--            <q-separator/>-->
<!--            <q-card-section class="q-py-sm">-->
<!--                <span class="label-caption text-grey-8">{{ actualMessages.actualStartAtReason.name }}:</span>-->
<!--                {{ eText.actualStartAtReason }}-->
<!--            </q-card-section>-->
<!--        </template>-->
<!--        <template v-if="eText.actualEndAtReason">-->
<!--            <q-separator/>-->
<!--            <q-card-section class="q-py-sm">-->
<!--                <span class="label-caption text-grey-8">{{ actualMessages.actualEndAtReason.name }}:</span>-->
<!--                {{ eText.actualEndAtReason }}-->
<!--            </q-card-section>-->
<!--        </template>-->

        <q-separator/>
        <q-card-section class="q-py-sm">
            <div class="row justify-between items-center">
<!--                <div class="col-auto q-gutter-x-md">-->
<!--                    <q-btn flat icon="las la-edit" text-color="primary" round @click="emit('showEditForm')"></q-btn>-->
<!--                    <span class="label-caption">{{ actualMessages.actualStartAt.name }}:</span>-->
<!--                    {{ dateHelper.utcFormat(eText.actualStartAt) }}-->
<!--                    <template v-if="eText.actualEndAt">-->
<!--                        <span class="label-caption">{{ actualMessages.actualEndAt.name }}:</span>-->
<!--                        {{ dateHelper.utcFormat(eText.actualEndAt) }}-->
<!--                    </template>-->
<!--                </div>-->
                <div class="col-auto text-grey-8 q-gutter-x-md">
                    <span class="label-caption">{{ entryInfoMessages.createdAt }}:</span>
                    {{ dateHelper.utcFormat(eText.createdAt) }}
                    <span class="label-caption">{{ entryInfoMessages.updatedAt }}:</span>
                    {{ dateHelper.utcFormat(eText.updatedAt) }}
                </div>
            </div>
        </q-card-section>
    </q-card>
</template>

<script setup lang="ts">

import {entryInfoMessages} from "../../../localize/messages";
import {dateHelper} from "../../../utils/date_helper";
import {EntryNote} from "../../../api/api_types";
// import {EntryText} from "../../../api/api_types";

defineProps<{
    eText: EntryNote,
}>()
const emit = defineEmits<{
    (e: 'showEditForm'): void
}>();
</script>