<template>
    <span>
        <q-icon name="las la-file-alt" :color="iconColor" size="1.4em" class="q-mr-xs"/>
        {{ note.note }}
    </span>
</template>

<script setup lang="ts">
import {NoteInfo} from "../../api/api_types";

defineProps<{
    iconColor?: string
    note: NoteInfo
}>()
</script>