<template>
    <entry-card
        v-if="profileStore.entry"
        :with-edit="true"
        @update-entry="reloadEntry"
        :entry="profileStore.entry"
    />
</template>

<script setup lang="ts">
import EntryCard from "./Entry.Card.vue";
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";

const profileStore = useEntryProfileStore();
const reloadEntry = async (entryId: string) => {
    // reload entry if updated
    await profileStore.getEntry(entryId)
}
</script>