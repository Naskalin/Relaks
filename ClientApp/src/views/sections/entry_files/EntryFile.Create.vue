<template>
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
</template>

<script setup lang="ts">
import {useQuasar} from "quasar";
import {useEntryFileCreateStore} from "../../../store/entryFile/entryFile.create.store";
import {onMounted} from "vue";

const store = useEntryFileCreateStore();

onMounted(() => {
    store.$reset();
});

const props = defineProps<{
    entryId: string
}>()

const emit = defineEmits<{
    (e: 'onUploaded'): void
}>()

const $q = useQuasar();

const onSelectFiles = async () => {
    const resp: any = await store.uploadFiles(props.entryId);
    console.log(resp);
    
    $q.notify({
        message: `Файлы успешно загружены: ${resp.count} шт.`,
        type: 'positive',
        progress: true,
        position: 'bottom-right',
        actions: [
            {
                label: 'Закрыть', color: 'white', handler: () => {
                }
            }
        ]
    });
    
    emit('onUploaded');
}
</script>