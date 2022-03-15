<template>
    <router-view></router-view>
</template>

<script lang="ts">
import {defineComponent} from "vue";
import {useQuasar} from "quasar";

export default defineComponent({
  setup() {
    // const quasar = useQuasar();
    // quasar.dark.set(true);
  }
})
</script>

<style lang="scss">
@import "./css/main";
</style>