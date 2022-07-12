<template>
    <component :is="components[activeComponent]"/>
</template>

<script setup lang="ts">
import LayoutDefault from './LayoutDefault.vue';
import UpdateAppPreset from '../sections/app_data/UpdateAppPreset.vue';
import {useAppPresetStore} from "../sections/app_data/app_preset_store";
import {computed, onMounted} from "vue";

const components = {
    LayoutDefault: LayoutDefault,
    UpdateAppPreset: UpdateAppPreset,
};
const appPresetStore = useAppPresetStore();
const activeComponent = computed(() => {
    if (appPresetStore.appPreset) return 'LayoutDefault';
    return 'UpdateAppPreset';
})
onMounted(async () => {
    await appPresetStore.getAppPreset();
})
</script>