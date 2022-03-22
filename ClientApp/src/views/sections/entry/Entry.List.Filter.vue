<template>
<!--  <div v-for="(name, key) in entryTypeTrans">-->
<!--    <q-radio color="secondary"-->
<!--             :val="key"-->
<!--             :label="name"/>-->
<!--    -->
<!--  </div>-->


  <p class="text-center text-h6">Фильтр</p>

  <q-btn-toggle
      class="bg-grey-2"
      v-model="entryType"
      spread
      toggle-color="primary"
      :options="entryMessages.entryType.selectOptions"
  />

  <q-input debounce="300" label="Поиск..." color="secondary" filled>
    <template v-slot:append>
      <q-icon name="las la-search"/>
    </template>
  </q-input>
</template>

<script setup lang="ts">
import {ref} from "vue";
import {entryMessages} from "../../../localize/messages";

const entryType = ref('Person');

</script>