<template>
  <div class="row">
    <div class="col-3">
      <profile-card></profile-card>
    </div>
    <div class="col">
      <div class="q-ml-lg bg-grey-9">
        <profile-tabs></profile-tabs>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import ProfileCard from "./_profile/ProfileCard.vue";
import {defineComponent, onMounted, ref, computed} from "vue";
import {useRoute} from 'vue-router'
import {useEntryProfileStore} from "../../store/entry/EntryProfileStore";
import ProfileTabs from "./_profile/ProfileTabs.vue";

export default defineComponent({
  components: {ProfileCard, EntryCard: ProfileCard, ProfileTabs},
  setup() {
    const store = useEntryProfileStore();
    const route = useRoute();
    onMounted(async () => {
      await store.getEntry(route.params.id as string);
    })
  }
})
</script>