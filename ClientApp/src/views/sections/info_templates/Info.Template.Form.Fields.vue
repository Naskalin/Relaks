<template>
    <q-input v-model="formStore.request.title" required="required" filled label="Название шаблона" autogrow counter maxlength="250"/>
    <custom-info-fields v-model="formStore.request.template"/>
</template>

<script setup lang="ts">
    import {useInfoTemplateFormStore} from "./info_template_form_store";
    import CustomInfoFields from '../../components/custom_info/CustomInfoFields.vue';

    const formStore = useInfoTemplateFormStore();
</script>