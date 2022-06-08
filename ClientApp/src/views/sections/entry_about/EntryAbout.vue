<template>
    <div class="row items-center justify-between q-col-gutter-lg">
        <div class="col-auto">
            <h5 v-if="profileStore.entry" class="q-my-md">
                {{ entryMessages.profile.tabs(profileStore.entry.entryType)["entry-about"] }}
            </h5>
        </div>
        <div class="col-auto">
            <q-btn @click="$router.push({name: 'entry-about', params: {entryId: entryId}, query: {new: ''}})"
                   label="Добавить набор данных"
                   icon="las la-plus-circle"
                   color="secondary"
            />
        </div>
    </div>
    <div v-if="infoStore.customs(null)">
        {{ infoStore.customs(null) }}
    </div>
    {{ model }}
<!--    <q-input v-model="model.title" filled label="Название набора данных" counter maxlength="250"/>-->
    <custom-info-form v-if="model" v-model="model"/>
</template>

<script setup lang="ts">
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {entryMessages} from "../../../localize/messages";
import CustomInfoForm from '../../components/custom_info/CustomInfoForm.vue';
import {ref} from 'vue';
import {CustomInfo} from "../../../api/api_types";
import {useEntryInfoListStore} from "../../../store/entry_info/entryInfo.list.store";
import {useRoute} from "vue-router";

const entryId = (useRoute()).params.entryId as string;

const profileStore = useEntryProfileStore();
const infoStore = useEntryInfoListStore();

const model = ref<CustomInfo | null>(null);
const createNewCustom = () => {
    model.value = {
        title: '',
        groups: []
    };
}
</script>