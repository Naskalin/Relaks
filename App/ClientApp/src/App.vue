<template>
    <router-view></router-view>
</template>

<script lang="ts">
import {defineComponent} from "vue";
import {useQuasar} from "quasar";

export default defineComponent({
  setup() {
    const quasar = useQuasar();
    quasar.dark.set(true);
  }
})
</script>
<style>
h1, h2, h3, h4, h5, h6 {
  margin-top: 0;
}
</style>