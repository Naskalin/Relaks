<template>
    <q-card v-bind="$attrs">
        <q-card-section class="bg-blue-grey-3">
            <div class="row justify-between items-center q-col-gutter-md">
                <div class="col">
                    <div class="text-h6">{{ headerTitle }}</div>
                </div>
                <div class="col-auto">
                    <q-btn
                        @click="emit('clickChangeBtn')"
                        round
                        icon="las la-edit"
                        color="primary"
                        outline
                        v-tooltip="'Изменить'"/>
                </div>
            </div>
        </q-card-section>
        <q-separator/>
        <q-card-section>
            <div class="groups q-gutter-y-lg">
                <div v-for="group in customInfo.groups">
                    <div v-if="group.title" class="q-mb-md">
                        <q-icon name="las la-object-ungroup" color="grey" class="q-mr-xs"/>
                        <b style="font-size: 1rem">{{ group.title }}</b>
                    </div>
                    <q-markup-table flat bordered separator="cell" class="custom-table">
                        <tbody>
                        <template v-for="item in group.items">
                            <tr v-if="isShowRow(item)" class="q-tr--no-hover">
                                <td v-if="isShowKey(item)">
                                    <q-icon name="las la-key" color="grey" class="q-mr-xs"/>
                                    <span class="">{{ item.key }}</span>
                                </td>
                                <td :colspan="isShowKey(item) ? 1 : 2">
                                    <q-icon name="las la-comment" color="grey" class="q-mr-xs"/>
                                    {{ item.value }}
                                </td>
                            </tr>
                        </template>
                        </tbody>
                    </q-markup-table>
                </div>
            </div>
        </q-card-section>
        <q-card-actions align="between" class="q-px-md">
            <div class="q-gutter-x-xs">
                <timestamps :timestamps="timestamps" stroke/>
            </div>
            <slot name="card-actions"></slot>
        </q-card-actions>
    </q-card>
</template>

<script setup lang="ts">
import {CustomInfo, CustomInfoItem, TimestampTypes} from "../../../api/api_types";
import Timestamps from '../Timestamps.vue';

const props = defineProps<{
    customInfo: CustomInfo,
    timestamps: TimestampTypes
    headerTitle: string
    hideRowsWithoutValue?: boolean
}>()
const emit = defineEmits<{
    (e: 'clickChangeBtn'): void
}>()
const isShowKey = (item: CustomInfoItem) => {
    return Boolean(item.key);
}
const isShowRow = (item: CustomInfoItem) => {
    const isHasValue = Boolean(item.value);
    if (isHasValue) return true;
    
    // isHasValue is false
    if (props.hideRowsWithoutValue) return false;
    
    return isShowKey(item);
}
</script>

<style lang="scss" scoped>
.custom-table {
    td {
        white-space: normal;
        text-align: left;
        font-size: 15px !important;
    }
}
</style>