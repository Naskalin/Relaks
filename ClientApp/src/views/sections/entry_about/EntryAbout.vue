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
                label="Вернуться без сохранения"
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
        <entry-info-custom-form @delete="formStore.$reset()" @save="createNew"/>
    </div>
    <div v-else-if="formStore.status === 'edit'">
        <h5 class="q-mb-sm">Изменение набора данных</h5>
        <entry-info-custom-form @delete="onDelete" @save="onUpdate"/>
    </div>
    <template v-else-if="aboutStore.customs.length">
        <q-card v-for="eInfo in aboutStore.customs" class="q-mb-lg" :id="'eInfo_custom_'+eInfo.id">
            <q-card-section class="q-pb-none">
                <div class="row justify-between">
                    <div class="col">
                        <div class="text-h6">{{eInfo.title}}</div>
                    </div>
                    <div class="col-auto">
                        <q-btn
                            @click="formStore.model = Object.assign({}, eInfo); formStore.status = 'edit'"
                            round
                            icon="las la-edit"
                            color="primary"
                            outline
                            v-tooltip="'Изменить'"/>
                    </div>
                </div>
            </q-card-section>
            <q-card-section>
                <div class="groups">
                    <div v-for="group in eInfo.info.groups" class="q-mb-lg">
                        <div v-if="group.title" class="q-mb-md">
                            <q-icon name="las la-object-ungroup" color="grey" class="q-mr-xs"/>
                            <b>{{group.title}}</b>
                        </div>
                        <q-markup-table flat bordered separator="cell" class="custom-table">
                            <tbody>
                            <tr v-for="item in group.items" class="q-tr--no-hover">
                                <td v-if="item.key">
                                    <q-icon name="las la-key" color="grey" class="q-mr-xs"/>
                                    <span class="">{{item.key}}</span>
                                </td>
                                <td :colspan="item.key ? 1 : 2">
                                    <q-icon name="las la-comment" color="grey" class="q-mr-xs"/>
                                    {{item.value}}
                                </td>
                            </tr>
                            </tbody>
                        </q-markup-table>
                    </div>
                </div>
            </q-card-section>
        </q-card>
    </template>
</template>

<script setup lang="ts">
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {entryMessages} from "../../../localize/messages";
import EntryInfoCustomForm from './entry_info_custom/Form.vue';
import {useRoute} from "vue-router";
import {useEntryInfoCustomFormStore} from "./entry_info_custom/entry_info_custom_form_store";
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useEntryAboutStore} from "./entry_about_store";
import {onMounted, watch} from "vue";
import {useLayoutStore} from "../../layouts/layout_store";

const layoutStore = useLayoutStore();
const entryId = (useRoute()).params.entryId as string;
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
        await aboutStore.getItems(entryId);
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
        await aboutStore.getItems(entryId);
        formStore.$reset();
    } finally {
        formStore.isLoading = false;
    }
}
onMounted(async () => {
    layoutStore.isRightSidebar = true;
    await aboutStore.getItems(entryId);
})
watch(() => formStore.status, val => {
    if (val) {
        layoutStore.isBlockLeaving = true;
        return;
    }

    layoutStore.isBlockLeaving = false;
})
</script>

<style lang="scss" scoped>
    .custom-table {
        td {
            white-space: normal;
            text-align: left;
            font-size: 15px !important;
        }
    }
</style>