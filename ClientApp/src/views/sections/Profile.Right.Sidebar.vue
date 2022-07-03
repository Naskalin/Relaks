<template>
    <component v-if="currentSidebar" :is="currentSidebar"/>
</template>

<script setup lang="ts">
import StructuresRightSidebar from '../sections/structure/Structures.Right.Sidebar.vue';
import EntryAboutRightSidebar from '../sections/entry_about/EntryAbout.Right.Sidebar.vue';
import {computed, ref} from "vue";
import {useRoute} from "vue-router";

const route = useRoute();

const currentSidebar = computed((): any => {
   if (route.name === 'entry-structures') return StructuresRightSidebar;
   if (route.name === 'entry-about') return EntryAboutRightSidebar;
   return null;
});
</script>