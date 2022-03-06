<template>
  
  <div class="row">
    <div class="col-3">
      <entry-card></entry-card>
    </div>
    <div class="col">
      <div class="q-ml-lg bg-grey-9">
        tabs...
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import EntryCard from "./_EntryCard.vue";
import {defineComponent, onMounted, ref, computed} from "vue";
import {jsonApi} from "../../api";
import { useRoute } from 'vue-router'
import {useEntryProfileStore} from "../../store/EntryProfileStore";

export default defineComponent({
  components: {EntryCard},
  setup(props) {
    const store = useEntryProfileStore();
    
    const route = useRoute();
    onMounted(async () => {
      // entry.value = await jsonApi.get('/persons/' + route.params.id);
      await store.getEntry('/persons/' + route.params.id);
      // console.log(store.entry)
    })
    
    return {
    }
  }
})
</script>