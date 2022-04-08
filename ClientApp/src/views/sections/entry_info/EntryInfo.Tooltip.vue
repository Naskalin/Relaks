<template>
    <meta-tooltip :meta="eInfo">
        <date v-if="eInfo.discriminator === 'Date'" :date="eInfo.date"></date>
        <url v-else-if="eInfo.discriminator === 'Url'" :url="eInfo.url"></url>
        <email v-else-if="eInfo.discriminator === 'Email'" :email="eInfo.email"></email>
        <phone v-else-if="eInfo.discriminator === 'Phone'" :phone="eInfo"></phone>
        <q-separator/>
        
        <template v-if="eInfo.title">
            {{eInfo.title}}
            <q-separator/>
        </template>
    </meta-tooltip>
</template>

<script setup lang="ts">
import MetaTooltip from '../../components/MetaTooltip.vue';
import Phone from '../../components/Phone.vue';
import Email from '../../components/Email.vue';
import Url from '../../components/Url.vue';
import Date from '../../components/Date.vue';

defineProps<{
    eInfo: any
}>()
</script>