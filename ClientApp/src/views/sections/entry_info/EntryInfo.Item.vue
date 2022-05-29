<template>
    <q-item :class="{'bg-pink-1': eInfo.deletedAt}" class="q-px-none">
        <q-item-section>
            <div class="row q-col-gutter-x-sm">
                <div class="col-auto">
                    <meta-tooltip :meta="eInfo">
                        <q-icon name="las la-info-circle" size="1.6rem"/>
                    </meta-tooltip>
                </div>
                <div class="col">
                    <date v-if="eInfo.discriminator === 'Date'" :date="eInfo.date"></date>
                    <url v-else-if="eInfo.discriminator === 'Url'" :url="eInfo.url"></url>
                    <email v-else-if="eInfo.discriminator === 'Email'" :email="eInfo.email"></email>
                    <phone v-else-if="eInfo.discriminator === 'Phone'" :phone="eInfo"></phone>
                    
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

const emit = defineEmits<{
    (e: 'showEditForm', eInfo: any): void
}>()
defineProps<{
    eInfo: any,
    withEdit?: boolean
}>()
</script>