<template>
    <div style="width: 500px">
        <div class="q-mb-sm">
            <q-file
                v-model="store.files"
                @update:model-value="onSelectFiles"
                :loading="store.isLoading"
                :disable="store.isLoading"
                multiple
                filled
                use-chips
                label="Выберите или переместите файлы"
            >
                <template v-slot:prepend>
                    <q-icon name="cloud_upload" @click.stop/>
                </template>
            </q-file>
        </div>
        <q-icon name="las la-info-circle la-fw" /> Файлы загрузятся в выбранную категорию.
    </div>
</template>

<script setup lang="ts">
import {useQuasar} from "quasar";
import {useEntryFileCreateStore} from "../../../store/entryFile/entryFile.create.store";
import {onMounted} from "vue";

const store = useEntryFileCreateStore();
const props = defineProps<{
    entryId: string
}>()
const emit = defineEmits<{
    (e: 'onUploaded'): void
}>()
const $q = useQuasar();
const onSelectFiles = async () => {
    const resp: any = await store.uploadFiles(props.entryId);
    
    $q.notify({
        message: `Файлы успешно загружены: ${resp.count} шт.`,
        type: 'positive',
        progress: true,
        position: 'bottom-right',
        actions: [
            {
                label: 'Закрыть', color: 'white', handler: () => {}
            }
        ]
    });
    
    emit('onUploaded');
}

onMounted(() => {
    store.$reset();
});
</script>