<template>
  <q-card v-for="n in (1, 10)">
    note .. {{n}}
  </q-card>
</template>

<script lang="ts">
import {defineComponent} from "vue";

export default defineComponent({
  setup() {
    return {
      
    }
  }
})
</script>