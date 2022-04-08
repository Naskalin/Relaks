<template>
    <q-card style="min-width: 600px">
        <template v-if="eNote.title">
            <q-card-section class="q-py-sm">
                <div class="text-h6">{{ eNote.title }}</div>
            </q-card-section>
            <q-separator/>
        </template>
        <q-card-section v-html="eNote.note"></q-card-section>
<!--        <template v-if="eNote.actualStartAtReason">-->
<!--            <q-separator/>-->
<!--            <q-card-section class="q-py-sm">-->
<!--                <span class="label-caption text-grey-8">{{ actualMessages.actualStartAtReason.name }}:</span>-->
<!--                {{ eNote.actualStartAtReason }}-->
<!--            </q-card-section>-->
<!--        </template>-->
<!--        <template v-if="eNote.actualEndAtReason">-->
<!--            <q-separator/>-->
<!--            <q-card-section class="q-py-sm">-->
<!--                <span class="label-caption text-grey-8">{{ actualMessages.actualEndAtReason.name }}:</span>-->
<!--                {{ eNote.actualEndAtReason }}-->
<!--            </q-card-section>-->
<!--        </template>-->

        <q-separator/>
        <q-card-section class="q-py-sm">
            <div class="row justify-between items-center">
<!--                <div class="col-auto q-gutter-x-md">-->
<!--                    <q-btn flat icon="las la-edit" text-color="primary" round @click="emit('showEditForm')"></q-btn>-->
<!--                    <span class="label-caption">{{ actualMessages.actualStartAt.name }}:</span>-->
<!--                    {{ dateHelper.utcFormat(eNote.actualStartAt) }}-->
<!--                    <template v-if="eNote.actualEndAt">-->
<!--                        <span class="label-caption">{{ actualMessages.actualEndAt.name }}:</span>-->
<!--                        {{ dateHelper.utcFormat(eNote.actualEndAt) }}-->
<!--                    </template>-->
<!--                </div>-->
                <div class="col-auto text-grey-8 q-gutter-x-md">
                    <span class="label-caption">{{ entryInfoMessages.createdAt }}:</span>
                    {{ dateHelper.utcFormat(eNote.createdAt) }}
                    <span class="label-caption">{{ entryInfoMessages.updatedAt }}:</span>
                    {{ dateHelper.utcFormat(eNote.updatedAt) }}
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
    eNote: EntryNote,
}>()
const emit = defineEmits<{
    (e: 'showEditForm'): void
}>();
</script>