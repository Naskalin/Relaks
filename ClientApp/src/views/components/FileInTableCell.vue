<template>
    <q-avatar v-if="isImage && imgSrc">
        <img :src="imgSrc" alt="">
    </q-avatar>
    <q-avatar v-else size="50px" 
              font-size="34px" 
              color="grey-5" 
              text-color="grey-4"
              icon="las la-question-circle"/>
</template>

<script setup lang="ts">
import {EntryFile} from "../../api/api_types";
import {apiFiles} from "../../api/rerources/api_files";
import {computed, ref, onMounted} from "vue";

const props = defineProps<{
    file: EntryFile
}>()

const downloadFile = async () => {
    return await apiFiles.entryFile({
        fileId: props.file.id,
        imageFilter: 'thumbnail'
    });
}

const isImage = computed(() => {
    return props.file.contentType.match(/image\//);
})

const imgSrc = ref(null);
onMounted(async () => {
    if (isImage) {
        imgSrc.value = await downloadFile();
    }
})
</script>