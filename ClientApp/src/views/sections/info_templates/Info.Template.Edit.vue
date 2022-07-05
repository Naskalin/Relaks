<template>
    <div class="text-h5 q-mb-lg">Изменение шаблона</div>
    
    <q-form>
        <form-fields/>
    </q-form>
</template>

<script setup lang="ts">
import {useRoute} from "vue-router";
import {onMounted, ref} from "vue";
import {InfoTemplate} from "../../../api/rerources/api_info_templates";
import {apiInfoTemplate} from "../../../api/rerources/api_info_templates";
import {useInfoTemplateFormStore} from "./info_template_form_store";
import FormFields from './Info.Template.Form.Fields.vue';

const infoTemplateId = (useRoute()).params.infoTemplateId as string;
const formStore = useInfoTemplateFormStore();
onMounted(async () => {
    console.log('>> ok')
    formStore.request = await apiInfoTemplate.get(infoTemplateId);
})
</script>