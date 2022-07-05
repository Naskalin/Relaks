<template>
    <div class="row q-mb-lg items-center q-col-gutter-lg">
        <div class="col-auto">
            <div class="text-h4">Шаблоны</div>
        </div>
        <div class="col">
            <q-icon name="las la-info-circle"/>
            Здесь вы можете создавать шаблоны данных для раздела о человеке/компании/встрече, например  шаблон паспорта или водительского удостоверения и т.д.
        </div>
    </div>
    
    <router-view/>
</template>

<script setup lang="ts">
import {onMounted} from "vue";
import {useInfoTemplatesStore} from "./info_templates_store";

const listStore = useInfoTemplatesStore();
onMounted(async () => {
    await listStore.getItemsAsync();
})
</script>