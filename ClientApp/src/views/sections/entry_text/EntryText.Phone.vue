<template>
    <phone :phone="p">
        <actual-tooltip v-if="entryText" :entry-text="entryText">
            <phone :phone="p"></phone>
            <q-separator></q-separator>

            <div v-if="entryText.about">
                {{entryText.about}}
                <q-separator></q-separator>
            </div>
            
            <small class="q-mr-xs">Регион</small> {{p.region}}
            <q-separator></q-separator>
        </actual-tooltip>
    </phone>
</template>

<script setup lang="ts">
import {PhoneType, phoneHelper} from "../../../utils/phone_helper";
import {computed} from 'vue';
import {EntryText} from "../../../api/api_types";
import ActualTooltip from '../../components/Actual.Tooltip.vue';
import Phone from '../../components/Phone.vue';

const props = defineProps<{
    entryText: EntryText
}>()

const p = computed((): PhoneType => {
    if (props.entryText.textType !== 'Phone') {
        throw Error('EntryText.Phone type should be Phone, actually EntryText.textType:' + props.entryText.textType);
    }

    return phoneHelper.toPhone(props.entryText.val);
    
})
</script>