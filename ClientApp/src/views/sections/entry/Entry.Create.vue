<template>
  <q-dialog v-model="store.isShowModal" persistent>
    <q-card style="width: 700px; max-width: 80vw;">
      <q-card-section class="row items-center card-header">
        <div class="text-h6">Добавление объединения</div>
        <q-space/>
        <q-btn icon="close" flat round dense v-close-popup/>
      </q-card-section>

      <q-form autocorrect="off"
              autocapitalize="off"
              autocomplete="off"
              spellcheck="false"
              @submit.prevent="addEntry">
        <q-card-section>
          <div class="q-gutter-lg">
            <q-radio v-for="(name, key) in entryTypeTrans"
                     color="secondary"
                     v-model="store.model.entryType"
                     :val="key"
                     :label="name"/>
            <q-input 
                     clearable
                     color="secondary"
                     v-model="store.model.name"
                     required="required"
                     :label="store.model.entryType === 'Person' ? 'Ф.И.О.' : 'Название'"/>

            <div class="q-mt-md">
              <p class="q-mb-sm text-secondary">
                Рейтинг
                <span class="text-white bg-grey-8 q-pa-xs rounded-borders">{{ store.model.reputation }}</span>
              </p>
              <q-rating :max="10"
                        v-model="store.model.reputation"
                        color="secondary"
                        icon="lar la-star"
                        icon-selected="las la-star"
                        size="2em">
              </q-rating>
            </div>
            
            <q-input
                v-model="store.model.description"
                counter
                autogrow
                color="secondary"
                label="Краткое описание"
                :hint="'Пример: ' + entryDescriptionHelpers[store.model.entryType]"
                type="textarea"
            />
          </div>

          <br>
          <div class="row q-col-gutter-md">
            <div class="col">
              <date-field :value="store.model.startAt"
                          :with-time="true"
                          :label="entryDateFields[store.model.entryType].startAt"
                          @input="val => store.model.startAt = val"></date-field>
            </div>
            <div class="col">
              <date-field :value="store.model.endAt"
                          :with-time="true"
                          :label="entryDateFields[store.model.entryType].endAt"
                          @input="val => store.model.endAt = val"></date-field>
            </div>
          </div>
        </q-card-section>

        <q-card-actions align="right" class="q-pa-md">
          <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
          <q-btn label="Добавить" type="submit" color="primary" icon="las la-plus-circle" :loading="store.isCreating"/>
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import DateField from './../../fields/DateField.vue';
import {withDefaults} from "vue";
import {entryTypeTrans, entryDescriptionHelpers, entryDateFields} from "../../../localize/messages";
import {useEntryCreateStore} from "../../../store/entry/entry.create.store";
import {useRouter} from "vue-router";
import {Entry, EntryType} from "../../../api/resource_types";

const emit = defineEmits<{
  (e: 'created', entry: Entry): void
}>()

type PropsType = { 
  hasRedirect?: boolean,
  entryType?: EntryType
};

const props = withDefaults(defineProps<PropsType>(), {
  hasRedirect: true,
  entryType: 'Person'
})

const store = useEntryCreateStore();
// if entryType in define props
store.model.entryType = props.entryType;

const router = useRouter();
const addEntry = async () => {
  store.model.actualStartAt = new Date().toISOString()
  const entry = await store.createEntry();
  
  emit('created', entry);
  if (props.hasRedirect) {
    await router.push({name: 'entry-profile', params: {entryId: entry.id}});
    store.$reset();
  }
  store.isShowModal = false;
}
</script>