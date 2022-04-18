<template>
    <q-avatar v-if="isImage && imgSrc" @dblclick="onDblclick">
        <img :src="imgSrc" alt="">
    </q-avatar>
    <q-avatar v-else size="50px"
              font-size="34px"
              color="grey-5"
              text-color="grey-4"
              :icon="iconClass"/>
</template>

<script setup lang="ts">
import {FileModel} from "../../api/api_types";
import {apiFiles} from "../../api/rerources/api_files";
import {computed, ref, onMounted} from "vue";

const props = defineProps<{
    file: FileModel
}>()

const downloadFile = async () => {
    return await apiFiles.download({
        fileId: props.file.id,
        imageFilter: 'thumbnail'
    });
}

const isImage = computed(() => {
    return props.file.contentType.match(/image\//);
})
const isVideo = computed(() => {
    return props.file.contentType.match(/video\//);
})
const isAudio = computed(() => {
    return props.file.contentType.match(/audio\//);
})
const isFont = computed(() => {
    return props.file.contentType.match(/font\//);
})
const isArchive = computed(() => {
    return [
        'application/zip',
        'application/x-7z-compressed',
        'application/vnd.rar',
        'application/x-tar',
        'application/x-freearc',
        'application/gzip',
        'application/java-archive',
        'application/x-bzip',
        'application/x-bzip2',
        'application/x-zip-compressed',
    ].includes(props.file.contentType);
})
const iconClass = computed(() => {
    const iconDefault = 'las la-file-alt';

    const classMap: { [index: string]: string } = {
        'text/csv': 'las la-file-csv',
        'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet': 'las la-file-excel',
        'application/vnd.ms-excel': 'las la-file-excel',
        'application/pdf': 'las la-file-pdf',
        'application/msword': 'las la-file-word',
        'application/vnd.openxmlformats-officedocument.wordprocessingml.document': 'las la-file-word',
    }

    let iconDetected: string | null;
    iconDetected = classMap[props.file.contentType] ?? null;
    if (!iconDetected && isAudio.value) {
        iconDetected = 'las la-file-audio';
    } else if (!iconDetected && isVideo.value) {
        iconDetected = 'las la-file-video';
    } else if (!iconDetected && isImage.value) {
        iconDetected = 'las la-file-image';
    } else if (!iconDetected && isFont.value) {
        iconDetected = 'las la-font';
    } else if (!iconDetected && isArchive.value) {
        iconDetected = 'las la-file-archive';
    }

    return iconDetected ?? iconDefault;
})

const imgSrc = ref(null);
onMounted(async () => {
    if (isImage) {
        imgSrc.value = await downloadFile();
    }
})
const onDblclick = () => {
  return imgSrc.value;
}
</script>