<template>
    <div class="inline-block">
        <q-icon name="las la-calendar" :color="iconColor" size="1.4em" class="q-mr-xs"/>
        <span>{{dateHelper.utcFormat(date, format)}}</span>
    </div>
</template>

<script setup lang="ts">
import {dateHelper} from "../../utils/date_helper";

defineProps<{
    date: string,
    iconColor?: string
    format?: string 
}>()
</script>