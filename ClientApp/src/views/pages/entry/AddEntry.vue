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
                     :label="store.model.entryType === 'Person' ? 'Ф.И.О.' : 'Название'"/>
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
        </q-card-section>

        <q-card-actions align="right" class="q-pa-md">
          <q-btn flat label="Закрыть" icon="las la-times" v-close-popup/>
          <q-btn label="Добавить" type="submit" color="primary" icon="las la-plus-circle"/>
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import DateField from './../../fields/DateField.vue';
import {withDefaults} from "vue";
import {entryTypeTrans, entryDescriptionHelpers, entryDateFields} from "../../../localize/messages";
import {useEntryAddStore} from "../../../store/entry/EntryAddStore";
import {useRouter} from "vue-router";
import {ApiGetResponse} from "../../../api/get";
import {EntryType} from "../../../types/types";
import {useQuasar} from "quasar";
const $q = useQuasar();
console.log($q.lang);

const emit = defineEmits<{
  (e: 'created', entry: ApiGetResponse): void
}>()

type PropsType = { 
  hasRedirect?: boolean,
  entryType?: EntryType
};

const props = withDefaults(defineProps<PropsType>(), {
  hasRedirect: true,
  entryType: 'Person'
})

const store = useEntryAddStore();
store.model.entryType = props.entryType

const router = useRouter();
const addEntry = async () => {
  const entry = await store.addEntry();
  emit('created', entry);
  if (props.hasRedirect) {
    await router.push({name: 'entries-profile', params: {id: entry.id}});
  }
  store.isShowModal = false;
}
</script>