<template>
    <div class="row justify-between items-center q-mb-md">
        <h5 class="q-my-md">{{ entryMessages.profile.tabs['entry-file-list'] }}</h5>
        <div style="width: 400px">
            <entry-file-create @onUploaded="onUploaded" :entry-id="entryId"/>
        </div>
    </div>

    test file
    
<!--    href="" width="100" height="100"-->
    <p>
<!--        <a >C:\app\RiderProjects\Relaks\Data\files\01fbdddd-1d69-4757-a8d2-5050a1aed4d4\71c043bc-3b72-4c94-8f80-d69150c65ce9.jpg</a>-->
    </p>

<!--    <p>-->
<!--        <img-->
<!--            src="C:\app\RiderProjects\Relaks\Data\files\01fbdddd-1d69-4757-a8d2-5050a1aed4d4\71c043bc-3b72-4c94-8f80-d69150c65ce9.jpg"-->
<!--            width="200"-->
<!--            height="200"-->
<!--            alt="">-->
<!--    </p>-->
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