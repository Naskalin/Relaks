<template>
    <router-view/>
    <api-errors-notify/>
</template>

<script setup lang="ts">
import ApiErrorsNotify from './views/components/ApiErrorsNotify.vue';
</script>

<style lang="scss">
@import "./css/main";
</style>