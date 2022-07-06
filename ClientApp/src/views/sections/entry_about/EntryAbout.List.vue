<template>
    <div class="row items-center justify-between q-my-lg">
        <div class="col-auto">
            <h4 v-if="profileStore.entry" class="q-my-none">
                {{ entryMessages.profile.tabs(profileStore.entry.entryType)["entry-about"] }}
            </h4>
        </div>
        <div class="col-auto">
            <q-btn
                @click="$router.push({name: 'entry-about-new'})"
                label="Добавить набор данных"
                icon="las la-plus-circle"
                color="primary"
            />
        </div>
    </div>

    <custom-info
        v-for="eInfo in aboutStore.customs"
        :timestamps="eInfo"
        :custom-info="eInfo.info"
        :header-title="eInfo.title"
        :key="'eInfo_custom_'+eInfo.id"
        :id="'eInfo_custom_'+eInfo.id"
        class="q-mb-xl"
        hide-keys
        @clickChangeBtn="$router.push({name: 'entry-about-edit', params: {entryId: entryId, entryInfoId: eInfo.id}})"
    >
        <template v-slot:card-actions>
            <q-btn
                size="sm"
                label="В шаблон"
                icon-right="las la-share-square"
                v-tooltip="'Добавить шаблон на основе набора данных'"
                @click="onAddInfoTemplate(eInfo)"
                color="primary"/>
        </template>
    </custom-info>
</template>

<script setup lang="ts">
import {EntryInfo} from "../../../api/api_types";
import {useEntryAboutStore} from "./entry_about_store";
import {useRouter} from "vue-router";
import {entryMessages} from "../../../localize/messages";
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import CustomInfo from '../../components/custom_info/CustomInfo.vue';

const profileStore = useEntryProfileStore();
const router = useRouter();
const entryId = router.currentRoute.value.params.entryId as string;
const aboutStore = useEntryAboutStore();
const onAddInfoTemplate = (eInfo: EntryInfo) => {
    router.push({name: 'info-templates-new', query: {entryId: entryId, entryInfoId: eInfo.id}})
}
</script>