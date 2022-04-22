<template>
    <div class="row">
        <div class="col-3" style="min-width: 350px">
            <entry-card v-if="profileStore.entry"
                          :with-edit="true"
                          @update-entry="reloadEntry"
                          :entry="profileStore.entry"></entry-card>
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
                
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import EntryCard from "./Entry.Card.vue";

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