<template>
  <q-card v-if="entry" class="bg-grey-9 q-pa-md text-center">
    <q-avatar size="150px" class="q-mb-md">
      <img
          src="https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=580&q=80">
    </q-avatar>
    
    <div class="text-h5 q-mb-sm">
      {{ entry.data.attributes.name }}
      <q-popup-edit buttons v-model="entry.data.attributes.name" class="bg-dark text-white" v-slot="scope">
        <q-input color="secondary" label="Ф.И.О." v-model="scope.value" autofocus @keyup.enter="scope.set">
          <template v-slot:append>
            <q-icon name="edit" />
          </template>
        </q-input>
<!--        <q-input filled-->
<!--                 clearable-->
<!--                 color="secondary"-->
<!--                 v-model="store.model.name"-->
<!--                 :label="store.model.entryType === 'Person' ? 'Ф.И.О.' : 'Название'"/>-->
      </q-popup-edit>
    </div>
    <p v-if="entry.data.attributes.description">{{entry.data.attributes.description}}</p>
  </q-card>
</template>

<script lang="ts">
import {defineComponent, computed} from "vue";
import {useEntryProfileStore} from "../../../../store/entry/EntryProfileStore";

export default defineComponent({
  setup() {
    const store = useEntryProfileStore();

    return {
      entry: computed(() => store.entry),
    }
  }
})
</script>