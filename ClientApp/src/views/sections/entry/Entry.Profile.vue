<template>
    <q-tabs v-if="profileStore.entry" inline-label align="left">
        <q-route-tab
            v-for="(label, routeName) in entryMessages.profile.tabs(profileStore.entry.entryType)"
            :key="routeName"
            :label="label"
            :to="{ name: routeName, params: {entryId: profileStore.entry.id} }"
            :disable="layoutStore.isBlockLeaving"
            v-tooltip="layoutStore.isBlockLeavingMessage"
        />
    </q-tabs>
    <q-separator/>
    
    <router-view/>
</template>

<script setup lang="ts">
import {watch} from "vue";
import {useRoute} from 'vue-router'
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {entryMessages} from "../../../localize/messages";
import {useLayoutStore} from "../../layouts/layout_store";

const profileStore = useEntryProfileStore();
const layoutStore = useLayoutStore();
const route = useRoute();
watch(() => route.params.entryId, async (val: any) => {
    if (val && val !== '' && typeof val === 'string') await profileStore.getEntry(val);
}, {immediate: true})
</script>