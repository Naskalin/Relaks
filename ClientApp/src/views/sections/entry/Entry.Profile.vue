<template>
    <div class="row">
        <div class="col-3" style="min-width: 320px">
            <profile-card></profile-card>
        </div>
        <div class="col">
            <div class="q-ml-lg">
                <q-tabs inline-label align="left">
                    <q-route-tab v-for="(label, routeName) in entryMessages.profile.tabs"
                                 :key="routeName"
                                 :label="label"
                                 :to="{ name: routeName, params: {entryId: entryId} }"
                                 exact
                    />
                </q-tabs>

                <q-separator/>
                <h5 class="q-my-md">{{ entryMessages.profile.tabs[route.name] }}</h5>
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import ProfileCard from "./Entry.Profile.Card.vue";

import {onMounted} from "vue";
import {useRoute} from 'vue-router'
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {entryMessages} from "../../../localize/messages";

const profileStore = useEntryProfileStore();
const route = useRoute();
const entryId = route.params.entryId as string;

onMounted(async () => {
    await profileStore.getEntry(entryId);
})
</script>