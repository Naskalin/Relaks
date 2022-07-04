<template>
    <template v-if="stroke === true">
        <span class="label-caption text-grey-8">создано</span>
        <span class="text-grey-7">{{ dateHelper.utcFormat(timestamps.createdAt) }}</span>
        <span class="q-mx-sm"></span>
        <span class="label-caption text-grey-8">обновлено</span>
        <span class="text-grey-7">{{ dateHelper.utcFormat(timestamps.updatedAt) }}</span>
    </template>
    <div v-else class="row">
        <div class="col text-left">
            <span class="label-caption text-grey-8">создано</span>
            <br>
            <span class="text-grey-7">{{ dateHelper.utcFormat(timestamps.createdAt) }}</span>
        </div>
        <div class="col text-right">
            <span class="label-caption text-grey-8">обновлено</span>
            <br>
            <span class="text-grey-7">{{ dateHelper.utcFormat(timestamps.updatedAt) }}</span>
        </div>
    </div>
</template>

<script setup lang="ts">
import {TimestampTypes} from "../../api/api_types";
import {dateHelper} from "../../utils/date_helper";

const props = defineProps<{
    timestamps: TimestampTypes,
    stroke?: boolean
}>()
</script>