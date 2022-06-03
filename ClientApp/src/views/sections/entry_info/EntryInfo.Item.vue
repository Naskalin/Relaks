<template>
    <q-item :class="{'bg-pink-1': eInfo.deletedAt}" class="q-px-none">
        <q-item-section>
            <div class="row q-col-gutter-x-sm">
                <div class="col-auto">
                    <meta-tooltip :meta="eInfo">
                        <q-icon name="las la-info-circle" size="1.6rem" color="grey-7"/>
                    </meta-tooltip>
                </div>
                <div class="col">
                    <date v-if="eInfo.type === 'Date'" :date="eInfo.info.date"></date>
                    <url v-else-if="eInfo.type === 'Url'" :url="eInfo.info"></url>
                    <email v-else-if="eInfo.type === 'Email'" :email="eInfo.info"></email>
                    <phone v-else-if="eInfo.type === 'Phone'" :phone="eInfo.info"></phone>
                    <note v-else-if="eInfo.type === 'Note'" :note="eInfo.info"/>
                    <q-item-label v-if="eInfo.title" class="text-grey-9">{{ eInfo.title }}</q-item-label>
                    <q-item-label v-if="eInfo.deletedReason" class="text-negative">{{ eInfo.deletedReason }}</q-item-label>
                </div>
            </div>
        </q-item-section>
        <q-item-section side>
            <q-btn v-if="withEdit" icon="las la-edit" @click="emit('showEditForm', eInfo)" flat round color="primary"/>
        </q-item-section>
    </q-item>
</template>

<script setup lang="ts">
import MetaTooltip from '../../components/MetaTooltip.vue';
import Phone from '../../components/Phone.vue';
import Email from '../../components/Email.vue';
import Url from '../../components/Url.vue';
import Date from '../../components/Date.vue';
import Note from '../../components/Note.vue';
import {EntryInfo} from "../../../api/api_types";

const emit = defineEmits<{
    (e: 'showEditForm', eInfo: EntryInfo): void
}>()
defineProps<{
    eInfo: EntryInfo,
    withEdit?: boolean
}>()
</script>