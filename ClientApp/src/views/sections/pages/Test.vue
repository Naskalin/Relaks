<template>
<!--    <p>{{dir}}</p>-->
<!--    <q-input type="text" v-model="dir"/>-->
    <input @update:model-value="onUpdate" type="file" webkitdirectory directory/>
<!--    <q-btn @click="onClick" label="send"/>-->
    
</template>

<script setup lang="ts">
import {appApi} from "../../../api";
import {ref} from 'vue';

const dir = ref('');
const onClick = async () => {
    await appApi.post(['app-preset'], {dataDir: dir.value});
}
const onUpdate = (val: any) => {
    
    console.log(val);
}
</script>
