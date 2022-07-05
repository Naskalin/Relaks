<template>
    <q-input
        v-model="formStore.request.title"
        required="required"
        filled
        label="Название шаблона"
        autogrow
        counter
        maxlength="250"
    />
    <custom-info-fields v-model="formStore.request.template">
        <template v-slot:custom-info-actions>
            <q-btn
                @click="onClear"
                label="Очистить от всех значений и пустых строк"
                v-tooltip="'Останутся все ключи, заголовки групп и название шаблона'"
                icon="las la-broom"
                outline
                color="negative"/>
        </template>
    </custom-info-fields>
</template>

<script setup lang="ts">
import {useInfoTemplateFormStore} from "./info_template_form_store";
import CustomInfoFields from '../../components/custom_info/CustomInfoFields.vue';

const formStore = useInfoTemplateFormStore();
const onClear = () => {
    for (const [_, group] of Object.entries(formStore.request.template.groups)) {
        for (const [_, item] of Object.entries(group.items)) {
            if (item.key) {
                item.value = '';
            } else {
                const indexOf = group.items.findIndex(x => x === item);
                if (indexOf > -1) group.items.splice(indexOf, 1);
            }
        }
    }
}
</script>