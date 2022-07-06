<template>
    <div class="row items-center justify-between q-my-lg">
        <div class="col-auto flex items-center q-gutter-x-md">
            <span class="q-my-none text-h4">Добавление шаблона</span>
            <q-btn
                v-if="entry"
                @click="formStore.$reset();$router.push({name: 'entry-about', params: {entryId: entry ? entry.id : ''}})"
                label="Вернуться в объединение"
                v-tooltip.right="entry.name"
                icon-right="las la-angle-right"
                outline
                color="secondary"
            />
        </div>
        <div class="col-auto">
            <q-btn
                @click="formStore.$reset();$router.push({name: 'info-templates'});"
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

        <div class="q-my-lg row items-center q-col-gutter-md">
            <div class="col-auto">
                <q-btn label="Добавить шаблон" type="submit" icon="las la-save" color="primary"/>    
            </div>
            <div v-if="entry" class="col-auto text-grey-8">
                <q-icon name="las la-long-arrow-alt-right"/> И вернуться в объединение [{{entry.name}}]
            </div>
        </div>
    </q-form>
</template>

<script setup lang="ts">
import {onBeforeRouteLeave, useRouter} from "vue-router";
import {onMounted, ref} from "vue";
import {apiInfoTemplate} from "../../../api/rerources/api_info_templates";
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useInfoTemplateFormStore} from "./info_template_form_store";
import FormFields from './Info.Template.Form.Fields.vue';
import {useInfoTemplatesStore} from "./info_templates_store";
import {Entry, CustomInfo} from "../../../api/api_types";
import {apiEntry} from "../../../api/rerources/api_entry";
import {useLayoutStore} from "../../layouts/layout_store";

const router = useRouter();
const formStore = useInfoTemplateFormStore();
const listStore = useInfoTemplatesStore();
const entry = ref<Entry | null>()
const layoutStore = useLayoutStore();
const onSave = async () => {
    await apiInfoTemplate.create(formStore.request);
    await listStore.getItemsAsync();
    formStore.$reset();

    if (entry.value) {
        await router.push({name: 'entry-about', params: {entryId: entry.value.id}})   
    } else {
        await router.push({name: 'info-templates'});   
    }
}
onMounted(async () => {
    formStore.$reset();
    layoutStore.isBlockLeaving = true;
    const entryInfoId = router.currentRoute.value.query.entryInfoId || null;
    const entryId = router.currentRoute.value.query.entryId || null;
    if (
        (entryInfoId && typeof entryInfoId === 'string' && entryInfoId !== '')
        &&
        (entryId && typeof entryId === 'string' && entryId !== '')
    ) {
        const entryInfo = await apiEntryInfo.get(entryId, entryInfoId);
        if (entryInfo.type !== 'CUSTOM') return;
        
        entry.value = await apiEntry.get(entryId);
        
        // map form
        formStore.request.title = entryInfo.title;
        formStore.request.template = entryInfo.info as CustomInfo
    }
})

onBeforeRouteLeave((to, from, next) => {
    layoutStore.isBlockLeaving = false;
    next();
})
</script>