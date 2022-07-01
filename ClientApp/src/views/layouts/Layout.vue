<template>
    <q-layout view="lHh Lpr lFf"  class="shadow-2 rounded-borders">
        <q-header bordered class="bg-secondary text-white">
            <q-toolbar>
                <q-tabs>
                    <q-route-tab label="Объединения" to="/entries"/>
                    <q-route-tab label="Relaks" to="/" exact/>
                </q-tabs>
            </q-toolbar>
        </q-header>

        <router-view/>
    </q-layout>
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar';
import { ref } from 'vue';

const $q = useQuasar()
const drawerLeft = ref($q.screen.width > 700);
</script>
