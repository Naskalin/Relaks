<template>
    <div class="row">
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
    timestamps: TimestampTypes
}>()
</script>