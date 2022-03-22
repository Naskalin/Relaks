<template>
  <q-dialog v-model="store.isShowModal" persistent>
    <q-card style="width: 700px; max-width: 80vw;">
      <q-card-section class="row items-center card-header">
        <div class="text-h6">Добавление объединения</div>
        <q-space/>
        <q-btn icon="close" flat round dense v-close-popup/>
      </q-card-section>

      <q-card-section>
        <q-form autocorrect="off"
                autocapitalize="off"
                autocomplete="off"
                spellcheck="false"
                id="entry-form"
                @submit.prevent="addEntry">
          <entry-form-fields :model="store.model"></entry-form-fields>
          <actual-fieldset :model="store.model"></actual-fieldset>
        </q-form>
      </q-card-section>

      <q-card-actions align="right" class="q-pa-md">
        <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
        <q-btn label="Добавить" form="entry-form" type="submit" color="primary" icon="las la-plus-circle" :loading="store.isCreating"/>
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import {withDefaults} from "vue";
import {useEntryCreateModalStore} from "../../../store/entry/entry.create.modal.store";
import {useRouter} from "vue-router";
import {Entry, EntryType} from "../../../api/resource_types";
import EntryFormFields from './Entry.Form.Fields.vue';
import {dateHelper} from "../../../utils/date_helper";
import ActualFieldset from '../../fieldsets/Actual.Fieldset.vue';

const emit = defineEmits<{
  (e: 'created', entry: Entry): void
}>()

type PropsType = {
  entryType?: EntryType
};

const props = withDefaults(defineProps<PropsType>(), {
  entryType: 'Person'
})

const store = useEntryCreateModalStore();
store.model.actualStartAt = dateHelper.utcFormat(new Date().toISOString());

if (props.entryType) {
  store.model.entryType = props.entryType;
}

store.isShowModal = true;

const router = useRouter();
const addEntry = async () => {
  const entry = await store.createEntry();
  emit('created', entry);
  store.$reset();
}
</script>