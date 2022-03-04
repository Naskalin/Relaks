<template>
  <q-layout view="lHh Lpr lFf" class="bg-white">
    <q-header elevated>
      <q-toolbar class="bg-blue-grey-7">
        <q-btn
            flat
            dense
            round
            @click="toggleLeftDrawer"
            aria-label="Menu"
            icon="menu"
            class="lt-md"
        />

        <q-toolbar-title>Ras</q-toolbar-title>
      </q-toolbar>
    </q-header>

    <q-drawer
        v-model="leftDrawerOpen"
        show-if-above
        bordered
        behavior="desktop"
        side="left"
        class="bg-grey-2"
    >
      <q-list separator>

        <q-item to="/" clickable v-ripple exact>
          <q-item-section>Home</q-item-section>
        </q-item>

<!--        <q-item to="/about" clickable v-ripple>-->
<!--          <q-item-section>About</q-item-section>-->
<!--        </q-item>-->

        <q-item to="/entries" clickable v-ripple>
          <q-item-section>Объединения</q-item-section>
        </q-item>

      </q-list>
    </q-drawer>

    <q-page-container>
      <q-page class="doc-page">
        <router-view />
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { ref, defineComponent } from 'vue'
import { useQuasar } from 'quasar';

export default  defineComponent({
  name: 'PageLayout',
  setup () {
    // const $q = useQuasar()

    // // get status
    // console.log($q.dark.isActive) // true, false
    //
    // // get configured status
    // console.log($q.dark.mode) // "auto", true, false
    //
    // // set status
    // $q.dark.set(true) // or false or "auto"

    const leftDrawerOpen = ref(false)
    function toggleLeftDrawer () {
      leftDrawerOpen.value = !leftDrawerOpen.value
    }
    return {
      leftDrawerOpen,
      toggleLeftDrawer
    }
  }
})
</script>

<style lang="scss">
.doc-page {
  margin-left: auto;
  margin-right: auto;
  max-width: 900px;
  padding: 16px 46px;
}
</style>