<template>
    <div class="row items-center justify-between q-my-lg">
        <div class="col-auto">
            <h4 class="q-my-none text-h4">Изменение шаблона</h4>
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
        
        <div class="q-my-lg flex justify-between">
            <div>
                <q-btn label="Сохранить шаблон" type="submit" icon="las la-save" color="primary"/>
                <q-banner v-if="entry">
                    <q-icon name="las la-info-circle"/> И вернуться в объединение [{{entry.name}}]
                </q-banner>
            </div>
            <q-btn color="negative" @click="onDelete" label="Удалить набор данных" icon="las la-trash"/>
        </div>
    </q-form>
</template>

<script setup lang="ts">
import {onBeforeRouteLeave, useRouter} from "vue-router";
import {onMounted, ref} from "vue";
import {apiInfoTemplate} from "../../../api/rerources/api_info_templates";
import {useInfoTemplateFormStore} from "./info_template_form_store";
import FormFields from './Info.Template.Form.Fields.vue';
import {useInfoTemplatesStore} from "./info_templates_store";
import {Entry} from "../../../api/api_types";
import {useQuasar} from "quasar";
import {useLayoutStore} from "../../layouts/layout_store";

const router = useRouter();
const infoTemplateId = router.currentRoute.value.params.infoTemplateId as string;
const formStore = useInfoTemplateFormStore();
const listStore = useInfoTemplatesStore();
const entry = ref<Entry | null>()
const $q = useQuasar();
const layoutStore = useLayoutStore();
const onSave = async () => {
    await apiInfoTemplate.update(infoTemplateId, formStore.request);
    await listStore.getItemsAsync();
    formStore.$reset();
    await router.push({name: 'info-templates'});
}
const onDelete = async () => {
    $q.dialog({
        title: 'Полное удаление!',
        message: 'Удаляем, всё верно?',
        class: 'bg-negative text-white',
        cancel: true,
        persistent: true
    }).onOk(async () => {
        await apiInfoTemplate.delete(infoTemplateId)
        await listStore.getItemsAsync();
        formStore.$reset();
        await router.push({name: 'info-templates'});
    })
}
onMounted(async () => {
    formStore.$reset();
    layoutStore.isBlockLeaving = true;
    formStore.request = await apiInfoTemplate.get(infoTemplateId);
})
onBeforeRouteLeave((to, from, next) => {
    layoutStore.isBlockLeaving = false;
    next();
})
</script>