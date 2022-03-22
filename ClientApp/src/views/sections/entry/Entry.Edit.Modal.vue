<!--<template>-->
<!--  <q-dialog v-model="editStore.isShowModal" persistent>-->
<!--    <q-card style="width: 700px; max-width: 80vw;">-->
<!--      <q-card-section class="row items-center card-header">-->
<!--        <div class="text-h6">Изменение объединения</div>-->
<!--        <q-space/>-->
<!--        <q-btn icon="close" flat round dense v-close-popup/>-->
<!--      </q-card-section>-->

<!--      <q-form autocorrect="off"-->
<!--              autocapitalize="off"-->
<!--              autocomplete="off"-->
<!--              spellcheck="false"-->
<!--              @submit.prevent="updateEntry">-->
<!--        <q-card-section>-->
<!--          <entry-form-fields :model="editStore.model" @input="val => editStore.model = val"></entry-form-fields>-->
<!--        </q-card-section>-->

<!--        <q-card-section>-->
<!--          <actual-fieldset :model="editStore.model"></actual-fieldset>-->
<!--        </q-card-section>-->

<!--        <q-card-actions align="right" class="q-pa-md">-->
<!--          <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>-->
<!--          <q-btn label="Сохранить" type="submit" color="primary" icon="las la-edit" :loading="editStore.isEditing"/>-->
<!--        </q-card-actions>-->
<!--      </q-form>-->

<!--    </q-card>-->
<!--  </q-dialog>-->
<!--</template>-->

<!--<script setup lang="ts">-->
<!--import {useEntryEditModal} from "../../../store/entry/entry.edit.modal.store";-->
<!--import EntryFormFields from './Entry.Form.Fields.vue';-->
<!--import ActualFieldset from '../../fieldsets/Actual.Fieldset.vue';-->
<!--const emit = defineEmits<{-->
<!--  (e: 'updated'): void-->
<!--}>()-->

<!--const editStore = useEntryEditModal();-->

<!--const updateEntry = async () => {-->
<!--  await editStore.updateEntry();-->
<!--  emit('updated');-->
<!--}-->
<!--</script>-->