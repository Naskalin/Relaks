<template>
    <q-card style="min-width: 600px">
        <deleted :deleted="eNote"/>
        
        <template v-if="eNote.title">
            <q-card-section class="q-py-sm">
                <div class="text-h6">{{ eNote.title }}</div>
            </q-card-section>
            <q-separator/>
        </template>
        <q-card-section v-html="eNote.note"></q-card-section>

        <q-separator/>
        <q-card-section class="q-py-sm">
            <div class="row justify-between items-center">
                <div class="col-auto q-gutter-x-md">
                    <q-btn flat icon="las la-edit" text-color="primary" round @click="emit('showEditForm')"></q-btn>
                </div>
                <div class="col-auto text-grey-8 q-gutter-x-md">
                    <span class="label-caption">{{ entryInfoMessages.createdAt }}:</span>
                    <date :date="eNote.createdAt"/>
                    <span class="label-caption">{{ entryInfoMessages.updatedAt }}:</span>
                    <date :date="eNote.updatedAt"/>
                </div>
            </div>
        </q-card-section>
    </q-card>
</template>

<script setup lang="ts">
import Date from '../../components/Date.vue';
import Deleted from '../../components/Deleted.vue';
import {entryInfoMessages} from "../../../localize/messages";
import {EntryNote} from "../../../api/api_types";

defineProps<{
    eNote: EntryNote,
}>()
const emit = defineEmits<{
    (e: 'showEditForm'): void
}>();
</script>