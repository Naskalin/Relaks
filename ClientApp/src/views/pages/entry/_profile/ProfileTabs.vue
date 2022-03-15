<template>
  <q-tabs inline-label align="left">
    <q-route-tab v-for="(label, key) in tabs"
                 :key="key"
                 :label="label"
                 :to="{ name: 'entry-' + key, params: {id: entryId} }"
                 exact
    />
  </q-tabs>
  <q-separator/>

  <br>
  
  <router-view></router-view>
</template>

<script setup lang="ts">
import {useEntryProfileStore} from "../../../../store/entry/EntryProfileStore";
import {useRoute} from "vue-router";
import {entryProfileTabs as tabs} from "../../../../localize/messages";

const store = useEntryProfileStore();
const route = useRoute();
const entryId = route.params.id;
</script>