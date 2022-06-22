<template>
    <q-layout view="hHh lpR fFf">
        <q-drawer
            show-if-above
            bordered
            behavior="desktop"
            side="left"
            class="app-sidebar"
            :width="250"
        >
            <q-list separator>
                <q-item to="/" clickable v-ripple exact>
                    <q-item-section>Информация</q-item-section>
                </q-item>
                <q-item to="/entries" clickable v-ripple>
                    <q-item-section>Объединения</q-item-section>
                </q-item>
            </q-list>
        </q-drawer>

        <q-page-container>
            <q-page class="doc-page">
                <router-view/>
            </q-page>
        </q-page-container>
    </q-layout>
</template>

<script setup lang="ts">
// import { ref } from 'vue'
//
// const leftDrawerOpen = ref(false);
// const toggleLeftDrawer = () => {
//   leftDrawerOpen.value = !leftDrawerOpen.value
// }
</script>

<style lang="scss">
.doc-page {
    margin-left: auto;
    margin-right: auto;
    //max-width: 900px;
    padding: 1.5vh;
}
</style>