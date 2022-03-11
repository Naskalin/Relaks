<template>
  <q-dialog v-model="store.isShowModal" persistent>
    <q-card style="width: 700px; max-width: 80vw;">
      <q-card-section class="row items-center bg-grey-10">
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
            <div>
              <p class="q-mb-sm text-white">Тип</p>
              <q-radio v-for="(name, key) in entryTypeTrans"
                       color="secondary"
                       v-model="store.model.entryType"
                       :val="key"
                       :label="name"/>
            </div>
            <q-input filled
                     clearable
                     color="secondary"
                     label-color="gold"
                     v-model="store.model.name"
                     :label="store.model.entryType === 'Person' ? 'Ф.И.О.' : 'Название'"/>
            <q-input
                v-model="store.model.description"
                filled
                counter
                autogrow
                color="secondary"
                label="Краткое описание"
                :hint="'Пример: ' + descHelpers[store.model.entryType]"
                type="textarea"
                rows="3"
            />
            <div>
              <p class="q-mb-sm text-white">
                Рейтинг
                <span class="text-white bg-brown q-pa-xs rounded-borders">{{ store.model.reputation }}</span>
              </p>
              <q-rating :max="10"
                        v-model="store.model.reputation"
                        color="secondary"
                        icon="lar la-star"
                        icon-selected="las la-star"
                        size="2.2em">
              </q-rating>
            </div>
            
            <div class="row">
              <div class="col">
                <q-input filled v-model="store.model.startAt" mask="date" :rules="['date']">
                  <template v-slot:append>
                    <q-icon name="event" class="cursor-pointer">
                      <q-popup-proxy ref="qDateProxy" cover transition-show="scale" transition-hide="scale">
                        <q-date v-model="store.model.startAt" mask="YYYY-MM-DD">
                          <div class="row items-center justify-end">
                            <q-btn v-close-popup label="Close" color="primary" flat />
                          </div>
                        </q-date>
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                </q-input>
              </div>
            </div>
          </div>
        </q-card-section>

        <q-card-actions align="right" class="bg-grey-10">
          <q-btn flat label="Закрыть" color="white" icon="las la-times" v-close-popup/>
          <q-btn label="Добавить" type="submit" color="primary" icon="las la-plus-circle"/>
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import {ref, defineEmits, defineProps, withDefaults} from "vue";
import {entryTypeTrans} from "../../localize/default";
import {EntryAddModel, useEntryAddStore} from "../../store/entry/EntryAddStore";
import {useRouter} from "vue-router";
import {ApiGetResponse} from "../../api/get";

const emit = defineEmits<{
  (e: 'created', entry: ApiGetResponse): void
}>()

const props = withDefaults(defineProps<{ hasRedirect?: boolean }>(), {
  hasRedirect: true,
})

// const model = ref<EntryAddModel>({
//   name: '',
//   reputation: 5,
//   entryType: 'Person',
//   description: '',
// });

const store = useEntryAddStore();
const router = useRouter();

const addEntry = async () => {
  const entry = await store.addEntry();
  emit('created', entry);
  if (props.hasRedirect) {
    await router.push({name: 'entries-profile', params: {id: entry.id}});
  }
  store.isShowModal = false;
}

const descHelpers = {
  'Person': 'Хороший ветеринар',
  'Company': 'Ветеринарная клиника доктора Айболита',
  'Meet': 'Осмотр щенка у доктора Айболита',
}
</script>