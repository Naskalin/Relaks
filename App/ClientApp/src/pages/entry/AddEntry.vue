<template>
  <h1>Добавление объединения</h1>

<!--  <q-dialog v-model="icon">-->
<!--    <q-card>-->
<!--      <q-card-section class="row items-center q-pb-none">-->
<!--        <div class="text-h6">Close icon</div>-->
<!--        <q-space />-->
<!--        <q-btn icon="close" flat round dense v-close-popup />-->
<!--      </q-card-section>-->

<!--      <q-card-section>-->
<!--        Lorem ipsum dolor sit amet consectetur adipisicing elit. Rerum repellendus sit voluptate voluptas eveniet porro. Rerum blanditiis perferendis totam, ea at omnis vel numquam exercitationem aut, natus minima, porro labore.-->
<!--      </q-card-section>-->
<!--    </q-card>-->
<!--  </q-dialog>-->
  
  <q-card class="q-pa-md">
    <q-form class="q-gutter-md" @submit.prevent="addEntry">
      <q-btn-toggle
          v-model="model.entryType"
          toggle-color="secondary"
          :options="entryTypeSelect"
      />
      <q-input filled v-model="model.name" :label="model.entryType === 'Person' ? 'Ф.И.О.' : 'Название'"/>
      <div class="bg-grey-2 q-pa-sm rounded-borders">
        <p class="q-mb-xs text-grey-7">Рейтинг
          <q-badge class="bg-grey-7">{{ model.reputation }}</q-badge>
        </p>
        <q-rating :max="10"
                  v-model="model.reputation"
                  color="secondary"
                  icon="lar la-star"
                  icon-selected="las la-star"
                  size="3em">
        </q-rating>
      </div>

      <q-btn label="Добавить" type="submit" color="primary" icon="las la-plus-circle">
      </q-btn>
    </q-form>

  </q-card>
</template>

<script lang="ts">
import {defineComponent, ref, onMounted} from "vue";
import {entryTypeTrans} from "../../localize/default";
import {EntryAddModel, useEntryAddStore} from "../../store/entry/EntryAddStore";
import {useRouter} from "vue-router";

export default defineComponent({
  setup() {
    const model = ref<EntryAddModel>({
      name: '',
      reputation: 5,
      entryType: 'Person'
    });
    

    // const entryType = ref('person');
    const store = useEntryAddStore();
    const router = useRouter();

    let entryTypeSelect = [];
    for (const [value, label] of Object.entries(entryTypeTrans)) {
      entryTypeSelect.push({label, value})
    }

    const addEntry = async () => {
      const resource = await store.addEntry(model.value);
      await router.push({name: 'entries-profile', params: {id: resource.id}});
    }

    return {
      addEntry,
      model,
      entryTypeSelect
    }
  }
})
</script>