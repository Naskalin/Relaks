<template>
    <q-layout view="lHh Lpr lFf"  class="shadow-2 rounded-borders">
        <q-header bordered class="bg-secondary text-white">
            <q-toolbar>
                <q-tabs>
                    <q-route-tab label="Объединения" to="/entries"/>
                </q-tabs>
                <q-space/>
<!--                <q-btn flat round dense icon="info" to="/" />-->

                <q-tabs>
<!--                    <q-route-tab icon="info" to="/" exact>-->
<!--                        <q-badge color="primary" text-color="white" floating>2</q-badge>-->
<!--                    </q-route-tab>-->
                    <q-route-tab
                        to="/"
                        alert-icon="las la-info-circle text-positive"
                        label="Relaks"
                        exact
                    >
                        <q-badge color="indigo-8" text-color="white" floating>TEST</q-badge>
                    </q-route-tab>
                </q-tabs>
            </q-toolbar>
        </q-header>

        <router-view/>
    </q-layout>
</template>

<script setup lang="ts">

</script>
