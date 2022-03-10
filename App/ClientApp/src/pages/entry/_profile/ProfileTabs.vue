<template>

  <q-tabs
      v-model="store.tab"
      inline-label
      align="left"
      class="bg-grey-10"
  >
    <q-tab v-for="(_, key) in tabs"
           :key="key"
           :name="key"
           :label="tabsTrans[key.toLowerCase()]"
           :to="{ query: { tab: key } }"
           exact
           replace
    />
  </q-tabs>

  <q-separator />

  <q-tab-panels v-if="store.tab" v-model="store.tab" animated class="bg-grey-9">
    <q-tab-panel v-for="(_, key) in tabs" :key="key" :name="key">
      <div class="text-h5 q-mb-md">{{ tabsTrans[key.toLowerCase()] }}</div>
      <component :is="tabs[store.tab]"></component>
    </q-tab-panel>
  </q-tab-panels>
</template>

<script lang="ts">
import {defineComponent, watch} from "vue";
import {useEntryProfileStore} from "../../../store/entry/EntryProfileStore";
import {useRouter} from "vue-router";
import {entryProfileTabsTrans} from "../../../localize/default";
import Notes from "./_tabs/Notes.vue";
import Dates from "./_tabs/Dates.vue";
import Files from "./_tabs/Files.vue";
const tabs = {
  Notes,
  Dates,
  Files,
}

export default defineComponent({
  components: {
    ...tabs
  },
  setup() {
    const store = useEntryProfileStore();
    const router = useRouter();
    store.tab = router.currentRoute.value.query.tab as string || 'Notes';
    watch(() => store.tab, (val) => {
      const r = router.currentRoute.value;
      router.push({path: r.path, query: {tab: val}})
    }, {immediate: true})
    return {
      store,
      tabs,
      tabsTrans: entryProfileTabsTrans,
    }
  }
})
</script>