<template>
<!--  <div v-for="(name, key) in entryTypeTrans">-->
<!--    <q-radio color="secondary"-->
<!--             :val="key"-->
<!--             :label="name"/>-->
<!--    -->
<!--  </div>-->

  <q-btn-toggle
      v-model="entryType"
      spread
      toggle-color="primary"
      :options="entryTypesSelectOptions()"
  />
<!--  -->
<!--  <q-input borderless debounce="300" label="Поиск..." color="secondary" filled>-->
<!--    <template v-slot:append>-->
<!--      <q-icon name="las la-search"/>-->
<!--    </template>-->
<!--  </q-input>-->
<!--           v-model="model.entryType"-->
</template>

<script setup lang="ts">
import {ref} from "vue";
import {entryTypesSelectOptions} from "../../../localize/messages";

const entryType = ref('Person');

</script>