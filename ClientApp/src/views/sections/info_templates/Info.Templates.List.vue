<template>
    <div class="text-h5 q-mb-lg">Список шаблонов</div>

    <custom-info
        v-for="infoTemplate in listStore.items"
        :timestamps="infoTemplate"
        :custom-info="infoTemplate.template"
        :header-title="infoTemplate.title"
        :key="'info_template_'+infoTemplate.id"
        :id="'info_template_'+infoTemplate.id"
        class="q-mb-xl"
        @clickChangeBtn="$router.push({name: 'info-templates-edit', params: {infoTemplateId: infoTemplate.id}})"
    >
    </custom-info>
</template>

<script setup lang="ts">
import {useInfoTemplatesStore} from "./info_templates_store";
import CustomInfo from '../../components/custom_info/CustomInfo.vue';

const listStore = useInfoTemplatesStore();
</script>