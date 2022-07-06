<template>
    <component v-if="currentSidebar" :is="currentSidebar"/>
</template>

<script setup lang="ts">
import StructuresRightSidebar from '../sections/structure/Structures.Right.Sidebar.vue';
import EntryAboutRightSidebar from '../sections/entry_about/EntryAbout.Right.Sidebar.vue';
import {computed} from "vue";
import {useRoute} from "vue-router";

const route = useRoute();

const currentSidebar = computed((): any => {
    if (typeof route.name === 'string') {
        if (route.name === 'entry-structures') return StructuresRightSidebar;
        if (['entry-about',
            'entry-about-list',
            'entry-about-edit',
            'entry-about-new'].includes(route.name)) return EntryAboutRightSidebar;
    }
    return null;
});
</script>