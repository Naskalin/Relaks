<template>
    <div class="row items-center justify-between q-my-lg">
        <div class="col-auto">
            <h4 class="q-my-none text-h4">Список шаблонов</h4>
        </div>
        <div class="col-auto">
            <q-btn
                @click="$router.push({name: 'info-templates-new'});"
                class="q-mt-md"
                label="Добавить шаблон"
                icon-right="las la-plus-circle"
                color="primary"
            />
        </div>
    </div>

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