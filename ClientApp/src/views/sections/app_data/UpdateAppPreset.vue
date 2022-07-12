<template>
    <modal
        label="Внешняя рабочая директория"
        v-model:is-show="isShow"
        :is-show-close-icon="false"
        :persistent="true"
    >
        <q-card-section>
            <q-form @submit.prevent="setDataDirAsync" id="app-data-dir-form">
                <q-input type="text" 
                         v-model="appPresetStore.dataDir" 
                         required="required" 
                         label="Путь к директории с данными"
                         hint="Создайте новую или выберите существующую директорию"
                />
            </q-form>
        </q-card-section>
        
        <q-card-actions align="right" class="q-pa-md">
            <q-btn form="app-data-dir-form" type="submit" label="Сохранить путь к директории" icon="las la-save" color="primary"/>
        </q-card-actions>
    </modal>
</template>

<script setup lang="ts">
import Modal from '../../components/Modal.vue';
import {ref} from 'vue';
import {useAppPresetStore} from "./app_preset_store";
import {appApi} from "../../../api";

const appPresetStore = useAppPresetStore();
const isShow = ref(true);
const setDataDirAsync = async () => {
    await appApi.post(['app-data-dir'], {dataDir: appPresetStore.dataDir});
    window.close();
}
</script>