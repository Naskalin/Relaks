<template>
    <q-btn round @click="emit('click', file)">
        <q-avatar size="50px" color="grey-5">
            <img v-if="isImage && imgSrc" :src="imgSrc" alt="">
            <q-icon v-else :name="iconClass" color="grey-4" size="2.4rem" />
        </q-avatar>
    </q-btn>
</template>

<script setup lang="ts">
import {FileModel} from "../../api/api_types";
import {apiFiles} from "../../api/rerources/api_files";
import {computed, ref, onMounted} from "vue";

const props = defineProps<{
    file: FileModel
}>()

const emit = defineEmits<{
    (e: 'click', fileModel: FileModel): void,
}>();

const getImgSrc = async () => {
    const resp = await apiFiles.download({
        fileId: props.file.id,
        imageFilter: 'square-thumbnail'
    });

    return URL.createObjectURL(resp.data);
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

const imgSrc = ref<string | null>(null);
onMounted(async () => {
    if (isImage) {
        imgSrc.value = await getImgSrc();
    }
})
</script>