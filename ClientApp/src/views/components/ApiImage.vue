<template>
<!--    <img v-if="src" :src="src" alt="">-->
    <q-img :src="src" v-bind="props"/>
</template>

<script setup lang="ts">
import {apiFiles} from "../../api/rerources/api_files";
import {ref, watch} from 'vue';

const props = defineProps<{
    fileId: string,
    imageFilter?: string
}>();

const src = ref<string | null>(null);

watch(() => props.fileId, async () => {
    const resp = await apiFiles.download({
        fileId: props.fileId,
        imageFilter: props.imageFilter
    });

    src.value = URL.createObjectURL(resp.data);
}, {immediate: true})

</script>