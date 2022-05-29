<template>
    <q-img v-if="src" :src="src" v-bind="props"/>
</template>

<script setup lang="ts">
import {apiFiles} from "../../api/rerources/api_files";
import {ref, watch} from 'vue';

const props = defineProps<{
    fileId?: string | null
    imageFilter?: string
}>();

const src = ref<string | undefined>(undefined);

watch(() => props.fileId, async () => {
    if (!props.fileId) throw new Error('fileId is null');
    const resp = await apiFiles.download({
        fileId: props.fileId,
        imageFilter: props.imageFilter
    });

    src.value = URL.createObjectURL(resp.data);
}, {immediate: true})

</script>