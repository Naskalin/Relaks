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
        <entry-info-custom-form @delete="state = null; formStore.$reset()"/>
    </div>
    <template v-else-if="infoStore.customs(null)">
        <q-card v-for="eInfo in infoStore.customs(null)" class="q-mb-lg">
            <q-card-section v-if="eInfo.title" class="q-pb-none">
                <div class="text-h6">{{eInfo.title}}</div>
            </q-card-section>
            <q-card-section>
                <div class="groups">
                    <div v-for="group in eInfo.info.groups" class="q-mb-lg">
                        <div v-if="group.title" class="q-mb-md"><b>{{group.title}}</b></div>
                        <q-markup-table flat bordered separator="cell" class="custom-table">
                            <tbody>
                            <tr v-for="item in group.items" class="q-tr--no-hover">
                                <td v-if="item.key">{{item.key}}</td>
                                <td :colspan="item.key ? 1 : 2">{{item.value}}</td>
                            </tr>
                            </tbody>
                        </q-markup-table>
<!--                        <div class="items">-->
<!--                            <div v-for="item in group.items" class="items__el">-->
<!--                                <div class="row q-col-gutter-lg">-->
<!--                                    <div v-if="item.key" class="col-auto">{{item.key}}</div>-->
<!--                                    <div class="col">{{item.value}}</div>-->
<!--                                </div>-->
<!--                            </div>-->
<!--                        </div>-->
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

const entryId = (useRoute()).params.entryId as string;

const profileStore = useEntryProfileStore();
const infoStore = useEntryInfoListStore();
const formStore = useEntryInfoCustomFormStore();
const state = ref<'new' | 'edit' | null>(null);
</script>

<style lang="scss" scoped>
    .custom-table {
        td {
            white-space: normal;
            text-align: left;
        }
    }
    //.groups {
    //    &__el {
    //        //border: 1px solid $grey-5;
    //        //padding: 0 1rem;
    //        margin: 1rem 0;
    //    }
    //    &__title {
    //        font-weight: bold;
    //    }
    //}
    //.items {
    //    &__el {
    //        padding: .5rem 0;
    //        &:not(:last-child) {
    //            border-bottom: 1px solid $grey-5;
    //        }
    //    }
    //}
</style>