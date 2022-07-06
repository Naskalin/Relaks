<template>
    <div class="row items-center justify-between q-my-lg">
        <div class="col-auto">
            <h4 class="q-my-none text-h4">Изменение набора данных</h4>
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
    
    <q-form @submit.prevent="onUpdate">
        <form-fields/>

        <div class="q-my-lg flex justify-between">
            <q-btn label="Сохранить набор данных" type="submit" icon="las la-save" color="primary"/>
            <q-btn color="negative" @click="onDelete" label="Удалить набор данных" icon="las la-trash"/>
        </div>
    </q-form>
</template>

<script setup lang="ts">
import FormFields from './EntryAbout.Form.Fields.vue';
import {useQuasar} from "quasar";
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useRouter, onBeforeRouteLeave} from "vue-router";
import {useEntryAboutStore} from "./entry_about_store";
import {useEntryAboutFormStore} from "./entry_about_form_store";
import {onMounted} from "vue";
import {useLayoutStore} from "../../layouts/layout_store";

const router = useRouter();
const $q = useQuasar();
const formStore = useEntryAboutFormStore();
const aboutStore = useEntryAboutStore();
const entryInfoId = router.currentRoute.value.params.entryInfoId as string;
const entryId = router.currentRoute.value.params.entryId as string;
const layoutStore = useLayoutStore();

const onUpdate = async () => {
    if (formStore.isLoading) return;
    formStore.isLoading = true;
    
    try {
        await apiEntryInfo.update(entryId, entryInfoId, formStore.model);
        formStore.$reset();
        await aboutStore.getItemsAsync(entryId);
        await router.push({name: 'entry-about', params: {entryId: entryId}})
    } finally {
        formStore.isLoading = false;
    }
}

const onDelete = async () => {
    $q.dialog({
        title: 'Полное удаление!',
        message: 'Удаляем, всё верно?',
        class: 'bg-negative text-white',
        cancel: true,
        persistent: true
    }).onOk(async () => {
        if (formStore.isLoading) return;
        formStore.isLoading = true;

        try {
            await apiEntryInfo.delete(entryId, entryInfoId);
            formStore.$reset();
            await aboutStore.getItemsAsync(entryId);
            await router.push({name: 'entry-about', params: {entryId: entryId}})
        } finally {
            formStore.isLoading = false;
        }
    })
}
onMounted(async () => {
    formStore.model = await apiEntryInfo.get(entryId, entryInfoId);
    layoutStore.isBlockLeaving = true;
})
onBeforeRouteLeave((to, from, next) => {
    layoutStore.isBlockLeaving = false;
    next();
})
</script>