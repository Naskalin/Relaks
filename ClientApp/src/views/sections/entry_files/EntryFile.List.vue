<template>
    <div class="row justify-between items-center q-mb-md">
        <h5 class="q-my-md">{{ entryMessages.profile.tabs['entry-file-list'] }}</h5>
        <div style="width: 400px">
            <entry-file-create @onUploaded="onUploaded" :entry-id="entryId"/>
        </div>
    </div>

    b30e1423-8aa7-44b1-a41f-023fdf988abb.jpg
<!--    <img src="C:\app\RiderProjects\Relaks\App\Data\files\01fbdddd-1d69-4757-a8d2-5050a1aed4d4\1a0143a0-fc33-4296-a1c0-5e96af906a88.jpg" width="100" height="100" alt="">-->
    <br>

    <entry-file-list-table :entry-id="entryId"></entry-file-list-table>
</template>

<script setup lang="ts">
import EntryFileListTable from './EntryFile.List.Table.vue';
import EntryFileCreate from './EntryFile.Create.vue';

import {entryMessages} from "../../../localize/messages";
import {useRoute} from "vue-router";
import {useEntryFileListTableStore} from "../../../store/entryFile/entryFile.list.table.store";

const route = useRoute();
const entryId = route.params.entryId as string;

const listStore = useEntryFileListTableStore();
const onUploaded = async () => {
    listStore.$reset();
    await listStore.getFiles(entryId);
}
</script>