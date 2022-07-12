<template>
    <p>{{dir}}</p>
    <q-input type="text" v-model="dir"/>
    <q-btn @click="onClick" label="send"/>
</template>

<script setup lang="ts">
import {appApi} from "../../../api";
import {ref} from 'vue';

const dir = ref('');
const onClick = async () => {
    await appApi.post(['app-preset'], {dataDir: dir.value});
}
</script>
