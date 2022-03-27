<template>
    <div class="inline-block">
        <q-icon name="las la-envelope" color="secondary" size="1.4em" class="q-mr-xs"/>
        <a :href="'mailto:' + entryText.val" class="link">{{entryText.val}}</a>
        <actual-tooltip :entry-text="entryText">
            <q-icon name="las la-envelope" color="grey-5" size="1.4em" class="q-mr-xs"/> {{entryText.val}}

            <q-separator></q-separator>

            <div v-if="entryText.about">
                {{entryText.about}}
                <q-separator></q-separator>
            </div>
        </actual-tooltip>
    </div>
</template>

<script setup lang="ts">
import {EntryText} from "../../../api/api_types";
import {onBeforeMount} from "vue";
import ActualTooltip from '../../components/Actual.Tooltip.vue';

const props = defineProps<{
    entryText: EntryText
}>()

onBeforeMount(() => {
    if (props.entryText.textType !== 'Email') {
        throw Error('EntryText.Email type should be Email, actually EntryText.textType:' + props.entryText.textType);
    }
})

</script>