<template>
    <h1>Tests</h1>

    <q-form>
        <q-file
            v-model="files"
            label="Pick files"
            filled
            multiple
            style="max-width: 300px"
        />
    </q-form>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const files = ref(null);
</script>