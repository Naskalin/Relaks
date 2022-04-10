<template>
    <div class="row justify-between items-center q-mb-md">
        <h5 class="q-my-md">{{ entryMessages.profile.tabs[route.name] }}</h5>
        <!--        <q-btn label="Добавить" @click="showCreateForm" color="primary" icon="las la-plus-circle"/>-->
    </div>

    <!--    <q-form @submit="onSubmit" enctype="multipart/form-data">-->
    <q-file
        name="files"
        v-model="files"
        @update:model-value="onSelectFiles"
        multiple
        use-chips
        label="Выберите файлы"
    />
<!--    <button type="submit">upload</button>-->
    <!--    </q-form>-->
</template>

<script setup lang="ts">
import {entryMessages} from "../../../localize/messages";
// import {apiEntryFile} from "../../../api/rerources/api_entry_file";
import {appApi} from "../../../api";
import {useRoute} from "vue-router";
import {ref} from 'vue';

const route = useRoute();
const entryId = route.params.entryId as string;
const files = ref(null);
const onSelectFiles = async (evt: any) => {
    // console.log(evt.length);

    const formData = new FormData();
    if (files.value) {
        for (const [key, val] of Object.entries(files.value)) {
            formData.append('files', val as any);
        }
    }
    await appApi.post(
        {
            resource: 'entries',
            resourceId: entryId,
            subResource: 'files'
        },
        formData,
        {headers: {'Content-Type': 'multipart/form-data'}}
    );
}
</script>