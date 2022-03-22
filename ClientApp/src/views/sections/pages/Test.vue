<template>
  <h1>Tests</h1>
<!--  <p>{{store.data}}</p>-->
<!--  <test-input :value="store.data" @input="val => store.data = val"></test-input>-->

<!--  <hr>-->
<!--  <p>Store date: {{store.dateField}}</p>-->
<!--  <date-field :value="store.dateField"-->
<!--              :with-time="true"-->
<!--              label="Супер дата"-->
<!--              @input="val => store.dateField = val"></date-field>-->
</template>

<script setup lang="ts">
// import {date} from 'quasar';


// const dateString = '05.04.1988 00:00';
// // const dateExtract = date.extractDate(dateString, 'DD.MM.YYYY HH:mm');
//
// // const dateString = '1988-05-04';
// const dateString = '2022-01-01';
// const dateExtract = date.extractDate(dateString, 'YYYY-MM-DD');
//
// // console.log(date)
// console.log(dateExtract.getTimezoneOffset());
// console.log((new Date('2022-01-01')).getTimezoneOffset());

// import * as dayjs from "dayjs";
import dayjs from "dayjs";
import timezone from 'dayjs/plugin/timezone';
import utc from 'dayjs/plugin/utc';
import 'dayjs/locale/ru';
import customParseFormat from 'dayjs/plugin/customParseFormat';
dayjs.extend(utc);
dayjs.extend(customParseFormat);
dayjs.extend(timezone);
dayjs.locale('ru')

// const date = dayjs.tz('05.04.1988 00:00', 'DD.MM.YYYY HH:mm', 'Europe/Moscow');
const date = dayjs('1988-04-04T20:00:00.000Z');
// // const date = dayjs.tz('05.04.1988 00:00', 'DD.MM.YYYY HH:mm', 'Europe/Moscow').toString();
console.log(date.format('DD.MM.YYYY HH:mm'));
console.log(date.tz('Europe/Moscow').format('DD.MM.YYYY HH:mm'));
console.log(date.tz('America/New_York').format('DD.MM.YYYY HH:mm'))
// console.log(date.toISOString());
// console.log(date.format());

// import {useTestStore} from "../../store/test_store";
// import TestInput from './entry/TestInput.vue';
// import DateField from './../fields/Date.Field.vue';
// import DateField from '@/fields/DateField.vue';

// const store = useTestStore();

// const onUpdate = (log) => {
//  
// }
</script>