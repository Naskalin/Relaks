<template>
    <div class="text-h6 q-mb-md">Шаблоны</div>

    <q-list v-if="listStore.items.length" bordered separator class="bg-my-grey">
        <q-item
            v-for="infoTemplate in listStore.items"
            clickable
            @click="onClickItem(infoTemplate)"
        >
            <q-item-section class="q-pr-none">
                <q-item-label :lines="3">{{ infoTemplate.title }}</q-item-label>
            </q-item-section>
        </q-item>
    </q-list>
    
</template>

<script setup lang="ts">
import {useInfoTemplatesStore} from "./info_templates_store";
import {scroll} from "quasar";
import {InfoTemplate} from "../../../api/rerources/api_info_templates";
import {useInfoTemplateFormStore} from "./info_template_form_store";

const formStore = useInfoTemplateFormStore();
const listStore = useInfoTemplatesStore();
const onClickItem = (infoTemplate: InfoTemplate) => {
    const { getScrollTarget, setVerticalScrollPosition } = scroll
    const el = document.getElementById('info_template_'+infoTemplate.id) as HTMLElement;
    const target = getScrollTarget(el);
    const offset = el.offsetTop
    setVerticalScrollPosition(target, offset, 200);
}
</script>