<template>
    <q-tabs v-if="profileStore.entry" inline-label align="left">
        <q-route-tab
            v-for="(label, routeName) in entryMessages.profile.tabs(profileStore.entry.entryType)"
            :key="routeName"
            :label="label"
            :to="{ name: routeName, params: {entryId: entryId} }"
        />
    </q-tabs>
    <q-separator/>
    
    <router-view/>
</template>

<script setup lang="ts">
import {onMounted} from "vue";
import {useRoute} from 'vue-router'
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {entryMessages} from "../../../localize/messages";

const profileStore = useEntryProfileStore();

const route = useRoute();
const entryId = route.params.entryId as string;

onMounted(async () => {
    // initialize entry
    await profileStore.getEntry(entryId);
})

</script>