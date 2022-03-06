<template>
  <h1>Добавление объединения</h1>
<!--  {{persons}}-->
  <q-form class="q-gutter-md" @submit.prevent="addPersonAsync">
    <q-btn-toggle
        v-model="entryType"
        toggle-color="secondary"
        :options="entryTypeSelect"
    />
    <q-input filled v-model="model.name" :label="entryType === 'person' ? 'Ф.И.О.' : 'Название'"/>
    <div class="bg-grey-2 q-pa-sm rounded-borders">
      <p class="q-mb-xs text-grey-7">Рейтинг <q-badge class="bg-grey-7">{{model.reputation}}</q-badge></p>
      <q-rating :max="10"
                v-model="model.reputation"
                color="secondary"
                icon="lar la-star"
                icon-selected="las la-star"
                size="3em">
      </q-rating>
    </div>

    <q-btn label="Добавить" type="submit" color="primary" icon="add_circle"/>
  </q-form>
</template>

<script lang="ts">
import {defineComponent, ref, onMounted} from "vue";
import {entryTypeTrans} from "../../localize/default";

export default defineComponent({
  name: "CreateEntry",
  setup() {
    const model = ref({
      name: '',
      reputation: 5,
    });
    
    const entryType = ref('person');

    let entryTypeSelect = [];
    for (const [key, label] of Object.entries(entryTypeTrans)) {
      entryTypeSelect.push({
        label: label,
        value: key,
      })
    }
    const addPersonAsync = ref();
    onMounted(async () => {
      
      addPersonAsync.value = async () => {
          // const person = new ApiPerson();
          // person.setAttr('name', model.value.name);
          // person.setAttr('reputation', model.value.reputation);
          // await person.save();
      }
      // await ApiPerson
      //     .orderBy('name', 'desc')
      //     .get();
    })
    
    return {
      model,
      entryTypeSelect,
      entryType,
      // persons,
      addPersonAsync,
    }
  }
})
</script>