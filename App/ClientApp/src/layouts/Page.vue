<template>
  <q-layout view="lHh Lpr lFf" class="bg-white">
    <q-header elevated>
      <q-toolbar>
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
        class="bg-grey-2"
    >
      <q-list separator>

        <q-item to="/" clickable v-ripple exact>
          <q-item-section>Home</q-item-section>
        </q-item>

        <q-item to="/about" clickable v-ripple>
          <q-item-section>About</q-item-section>
        </q-item>

      </q-list>
    </q-drawer>

    <q-page-container>
      <q-page padding>
        <router-view />
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<script>
import { ref } from 'vue'
import { useQuasar } from 'quasar'
export default {
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
}
</script>