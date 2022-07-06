<template>
    <modal v-model:is-show="isShowTemplateModal" label="Выберите шаблон">
        <q-list v-if="infoTemplatesStore.items.length" bordered separator class="bg-my-grey">
            <q-item
                v-for="infoTemplate in infoTemplatesStore.items"
                clickable
                @click="onClickItem(infoTemplate)"
            >
                <q-item-section class="q-pr-none">
                    <q-item-label :lines="3">{{ infoTemplate.title }}</q-item-label>
                </q-item-section>
            </q-item>
        </q-list>
    </modal>
    <div class="row items-center justify-between q-my-lg">
        <div class="col-auto q-gutter-x-md flex items-center">
            <span class="q-my-none text-h4">Добавление набора данных</span>
            <q-btn @click="isShowTemplateModal = true" icon="las la-file-alt" label="По шаблону" color="secondary"/>
        </div>
        
        <div class="col-auto">
            <q-btn
                @click="formStore.$reset(); router.push({name: 'entry-about', params: {entryId: entryId}})"
                class="q-mt-md"
                label="Выйти без сохранения"
                icon-right="las la-angle-right"
                outline
                color="secondary"
            />
        </div>
    </div>
    
    <q-form @submit.prevent="onSave">
        <form-fields/>

        <div class="q-my-lg flex justify-between">
            <q-btn label="Добавить набор данных" type="submit" icon="las la-plus-circle" color="primary"/>
        </div>
    </q-form>
</template>

<script setup lang="ts">
import FormFields from './EntryAbout.Form.Fields.vue';
import {useQuasar} from "quasar";
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useRouter} from "vue-router";
import {useEntryAboutStore} from "./entry_about_store";
import {useEntryAboutFormStore} from "./entry_about_form_store";
import Modal from '../../components/Modal.vue';
import {ref, onMounted} from 'vue';
import {useInfoTemplatesStore} from "../info_templates/info_templates_store";
import {InfoTemplate} from "../../../api/rerources/api_info_templates";

const infoTemplatesStore = useInfoTemplatesStore();
const isShowTemplateModal = ref(false);
const router = useRouter();
const $q = useQuasar();
const formStore = useEntryAboutFormStore();
const aboutStore = useEntryAboutStore();
const entryId = router.currentRoute.value.params.entryId as string;
const onClickItem = (infoTemplate: InfoTemplate) => {
    formStore.model.title = infoTemplate.title;
    formStore.model.info = infoTemplate.template;
    isShowTemplateModal.value = false;
}
const onSave = async () => {
    if (formStore.isLoading) return;
    formStore.isLoading = true;

    try {
        const eInfo = await apiEntryInfo.create(entryId, formStore.model);
        aboutStore.list.unshift(eInfo);
        formStore.$reset();
        await router.push({name: 'entry-about', params: {entryId: entryId}})
    } finally {
        formStore.isLoading = false;
    }
}

onMounted(async () => {
    formStore.$reset();
    await infoTemplatesStore.getItemsAsync();
})
</script>