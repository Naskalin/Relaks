<template>
    <q-item class="q-pa-sm" 
            v-bind="$attrs"
            :class="{
                'bg-pink-1': entry.deletedAt,
                'bg-deep-purple-2': withPreview && store.previewEntry && store.previewEntry.id === entry.id
            }"
    >
        <slot name="start"></slot>
        <q-item-section side @click="onClickAvatar">
            <q-avatar size="50px" color="grey-5">
                <api-image v-if="entry.avatar" :file-id="entry.avatar" image-filter="square-thumbnail"/>
                <q-icon v-else name="las la-question-circle" color="grey-4" size="34px"/>
                <q-menu
                    v-if="withPreview"
                    style="width: 390px; height: 500px"
                    self="center right"
                    ref="menuPreviewRef"
                    :offset="[20, 0]"
                >
                    <q-scroll-area class="fit">
                        <q-banner class="text-center bg-grey-5 q-pa-sm" dense>
                            <div style="font-size: .85rem">
                                <q-icon name="las la-info-circle"/>
                                Двойной клик на аватар откроет это объединение.
                            </div>
                        </q-banner>
                        <entry-card
                            flat
                            square
                            :entry="entry"
                            :with-edit="false"
                            @card-dblclick="val => $router.push({name: 'entry-profile', params: {entryId: val.id}})"
                        />
                    </q-scroll-area>
                </q-menu>
            </q-avatar>
        </q-item-section>
        <q-item-section>
            <q-item-label>{{entry.name}}</q-item-label>
            <slot name="body"></slot>
        </q-item-section>
        <slot name="end"></slot>
    </q-item>
</template>

<script setup lang="ts">
import ApiImage from '../ApiImage.vue';
import EntryCard from '../../sections/entry/Entry.Card.vue';
import {Entry} from "../../../api/api_types";
import {useEntryItemStore} from "./entry_item_store";

const props = defineProps<{
    entry: Entry,
    withPreview?: boolean
}>()
const emit = defineEmits<{
    (e: 'clickAvatar', val: Entry): void
}>()

const store = useEntryItemStore();
const onClickAvatar = () => {
    if (props.withPreview) store.previewEntry = props.entry;
    emit('clickAvatar', props.entry);
}
</script>