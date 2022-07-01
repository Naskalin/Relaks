<template>
    <with-sidebar>
        <template v-slot:sidebar>
            <entry-card
                v-if="profileStore.entry"
                :with-edit="true"
                @update-entry="reloadEntry"
                :entry="profileStore.entry"
            />
        </template>

        <q-tabs v-if="profileStore.entry" inline-label align="left">
            <q-route-tab
                v-for="(label, routeName) in entryMessages.profile.tabs(profileStore.entry.entryType)"
                :key="routeName"
                :label="label"
                :to="{ name: routeName, params: {entryId: entryId} }"
            />
        </q-tabs>
        <q-separator/>

        <router-view></router-view>
    </with-sidebar>
</template>

<script setup lang="ts">
import EntryCard from "./Entry.Card.vue";
import WithSidebar from '../../layouts/_WithSidebar.vue';

import {onMounted} from "vue";
import {useRoute} from 'vue-router'
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {entryMessages} from "../../../localize/messages";

const profileStore = useEntryProfileStore();

const route = useRoute();
const entryId = route.params.entryId as string;

const reloadEntry = async (entryId: string) => {
    // reload entry if updated
    await profileStore.getEntry(entryId)   
}
onMounted(async () => {
    // initialize entry
    await profileStore.getEntry(entryId);
})

</script>