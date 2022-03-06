<template>
  <div class="bg-grey-9 q-pa-md text-center">
    <q-avatar size="150px">
      <img
          src="https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=580&q=80">
    </q-avatar>
    {{entry}}
    <!--    <h1 v-if="entry">{{entry.data.attributes.name}}</h1>-->
  </div>
</template>

<script lang="ts">
import {defineComponent, computed} from "vue";
import {useEntryProfileStore} from "../../store/EntryProfileStore";

export default defineComponent({
  setup() {
    const store = useEntryProfileStore();

    return {
      entry: computed(() => store.entry),
    }
  }
})
</script>