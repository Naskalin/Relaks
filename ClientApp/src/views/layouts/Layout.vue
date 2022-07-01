<template>
    <q-layout view="lHh Lpr lFf"  class="shadow-2 rounded-borders">
        <q-header bordered class="bg-secondary text-white">
            <q-toolbar>
                <q-tabs>
                    <q-route-tab label="Объединения" to="/entries"/>
                </q-tabs>
                <q-space/>
                <q-btn flat round dense icon="info" to="/" />
            </q-toolbar>
        </q-header>

        <router-view/>
    </q-layout>
</template>

<script setup lang="ts">

</script>
