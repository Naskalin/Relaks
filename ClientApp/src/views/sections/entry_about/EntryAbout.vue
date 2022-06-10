<template>
    <div class="row items-center justify-between q-col-gutter-lg q-mb-lg">
        <div class="col-auto">
            <h5 v-if="profileStore.entry" class="q-my-md">
                {{ entryMessages.profile.tabs(profileStore.entry.entryType)["entry-about"] }}
            </h5>
        </div>
        <div class="col-auto">
            <q-btn
                v-if="state !== 'new'"
                @click="state = 'new'; formStore.$reset()"
                label="Добавить набор данных"
                icon="las la-plus-circle"
                color="secondary"
            />
        </div>
    </div>
    <div v-if="state === 'new'">
        {{formStore.model}}
        <h5 class="q-mb-sm">Добавление набора данных</h5>
        <entry-info-custom-form @delete="state = null; formStore.$reset()" @save="createNew"/>
    </div>
    <template v-else-if="infoListStore.customs(null)">
        <q-card v-for="eInfo in infoListStore.customs(null)" class="q-mb-lg">
            <q-card-section v-if="eInfo.title" class="q-pb-none">
                <div class="text-h6">{{eInfo.title}}</div>
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
import {useEntryInfoListStore} from "../../../store/entry_info/entryInfo.list.store";
import {useRoute} from "vue-router";
import {useEntryInfoCustomFormStore} from "./entry_info_custom/entry_info_custom_form_store";
import {ref} from 'vue';
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";

const entryId = (useRoute()).params.entryId as string;
const profileStore = useEntryProfileStore();
const infoListStore = useEntryInfoListStore();
const formStore = useEntryInfoCustomFormStore();
const state = ref<'new' | 'edit' | null>(null);
const createNew = async (val: any) => {
    if (formStore.isLoading) return;
    formStore.isLoading = true;

    try {
        const eInfo = await apiEntryInfo.create(entryId, formStore.model);
        infoListStore.list.push(eInfo);
        state.value = null;
        infoListStore.$reset();
    } finally {
        formStore.isLoading = false;
    }
}
</script>

<style lang="scss" scoped>
    .custom-table {
        td {
            white-space: normal;
            text-align: left;
        }
    }
</style>