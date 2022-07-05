<template>
    <div class="row items-center justify-between q-my-lg">
        <div class="col-auto">
            <h4 v-if="profileStore.entry" class="q-my-none">
                {{ entryMessages.profile.tabs(profileStore.entry.entryType)["entry-about"] }}
            </h4>
            <q-btn
                v-if="formStore.status"
                @click="formStore.$reset()"
                class="q-mt-md"
                label="Выйти без сохранения"
                icon="las la-angle-left"
                outline
                color="secondary"
            />
        </div>
        <div class="col-auto">
            <q-btn
                v-if="!formStore.status"
                @click="formStore.status = 'new'"
                label="Добавить набор данных"
                icon="las la-plus-circle"
                color="primary"
            />
        </div>
    </div>

    <div v-if="formStore.status === 'new'">
        <h5 class="q-mb-sm">Добавление набора данных</h5>
        <entry-info-custom-form @save="createNew"/>
    </div>
    <div v-else-if="formStore.status === 'edit'">
        <h5 class="q-mb-sm">Изменение набора данных</h5>
        <entry-info-custom-form
            with-delete-btn
            @delete="onDelete" 
            @save="onUpdate"
        />
    </div>
    <template v-else-if="aboutStore.customs.length">
        <custom-info 
            v-for="eInfo in aboutStore.customs"
            :timestamps="eInfo" 
            :custom-info="eInfo.info"
            :header-title="eInfo.title"
            :key="'eInfo_custom_'+eInfo.id"
            :id="'eInfo_custom_'+eInfo.id"
            class="q-mb-xl"
            hide-keys
            @clickChangeBtn="formStore.model = Object.assign({}, eInfo); formStore.status = 'edit'"
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
</template>

<script setup lang="ts">
import Timestamps from '../../components/Timestamps.vue';
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {entryMessages} from "../../../localize/messages";
import EntryInfoCustomForm from './entry_info_custom/Form.vue';
import {onBeforeRouteLeave, useRouter} from "vue-router";
import {useEntryInfoCustomFormStore} from "./entry_info_custom/entry_info_custom_form_store";
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useEntryAboutStore} from "./entry_about_store";
import {onMounted, watch} from "vue";
import {useLayoutStore} from "../../layouts/layout_store";
import CustomInfo from '../../components/custom_info/CustomInfo.vue';
import {EntryInfo} from "../../../api/api_types";

const router = useRouter();
const layoutStore = useLayoutStore();
const entryId = router.currentRoute.value.params.entryId as string;
const profileStore = useEntryProfileStore();
const aboutStore = useEntryAboutStore();
const formStore = useEntryInfoCustomFormStore();
const createNew = async (val: any) => {
    if (formStore.isLoading) return;
    formStore.isLoading = true;

    try {
        const eInfo = await apiEntryInfo.create(entryId, formStore.model);
        aboutStore.list.unshift(eInfo);
        formStore.$reset();
    } finally {
        formStore.isLoading = false;
    }
}
const onDelete = async () => {
    if (formStore.isLoading) return;
    formStore.isLoading = true;

    try {
        const eInfoId = formStore.model.id!;
        await apiEntryInfo.delete(entryId, eInfoId);
        await aboutStore.getItemsAsync(entryId);
        formStore.$reset();
    } finally {
        formStore.isLoading = false;
    }
}
const onUpdate = async () => {
    if (formStore.isLoading) return;
    formStore.isLoading = true;
    try {
        const eInfoId = formStore.model.id!;
        await apiEntryInfo.update(entryId, eInfoId, formStore.model);
        await aboutStore.getItemsAsync(entryId);
        formStore.$reset();
    } finally {
        formStore.isLoading = false;
    }
}
const onAddInfoTemplate = (eInfo: EntryInfo) => {
    router.push({name: 'info-templates-new', query: {entryId: entryId, entryInfoId: eInfo.id}})
}
onMounted(async () => {
    layoutStore.isRightSidebar = true;
    await aboutStore.getItemsAsync(entryId);
})
watch(() => formStore.status, val => {
    if (val) {
        layoutStore.isBlockLeaving = true;
        return;
    }

    layoutStore.isBlockLeaving = false;
})
onBeforeRouteLeave((to, from, next) => {
    layoutStore.isRightSidebar = false;
    next();
})
</script>