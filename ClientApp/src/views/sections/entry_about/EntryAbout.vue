<template>
    <router-view/>
</template>

<script setup lang="ts">
import {onBeforeRouteLeave, useRouter} from "vue-router";
import {useEntryAboutStore} from "./entry_about_store";
import {onMounted} from "vue";
import {useLayoutStore} from "../../layouts/layout_store";

const router = useRouter();
const layoutStore = useLayoutStore();
const entryId = router.currentRoute.value.params.entryId as string;
const aboutStore = useEntryAboutStore();
onMounted(async () => {
    layoutStore.isRightSidebar = true;
    await aboutStore.getItemsAsync(entryId);
})
onBeforeRouteLeave((to, from, next) => {
    layoutStore.isRightSidebar = false;
    next();
})
</script>