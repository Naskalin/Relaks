<template></template>

<script setup lang="ts">
import {useQuasar} from "quasar";
import {ApiError, useApiErrorsStore} from "../../store/api_errors_store";
import {watch} from 'vue';

const $q = useQuasar();
const store = useApiErrorsStore();

watch(() => store.apiError, (apiError: null | ApiError) => {
    if (apiError) {
        const title = [];
        if (apiError?.status) title.push(apiError.status);
        if (apiError?.title) title.push(apiError.title);
        let html = '';
        if (title.length) html += `<div>${title.join(', ')}</div>`;
        
        
        if (apiError?.errors) {
            for (const [prop, messages] of Object.entries(apiError?.errors)) {
                html += '<ul>'
                messages.map((e: string) => html += `<li>${e}</li>`);
                html += '</ul>'
            }
        }
        
        if (html === '') html = 'Неизвестная ошибка.';

        $q.notify({
            message: html,
            html: true,
            type: 'negative',
            progress: true,
            position: 'bottom-right',
            actions: [
                {label: 'Закрыть', color: 'white', handler: () => {}}
            ]
        });
    }
})


</script>