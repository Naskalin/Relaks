<template>
  <q-input color="secondary"
           style="max-width: 300px"
           :mask="mask"
           hide-bottom-space
           v-model="model"
           :required="required"
           :label="label"
  >
    <template v-slot:prepend>
      <q-icon name="event" class="cursor-pointer">
        <q-popup-proxy ref="qDateProxyStart" cover transition-show="scale" transition-hide="scale">
          <q-date v-model="model" :today-btn="todayBtn" mask="DD.MM.YYYY">
            <div class="row items-center justify-end">
              <q-btn v-close-popup label="Выбрать" color="primary" flat/>
            </div>
          </q-date>
        </q-popup-proxy>
      </q-icon>
    </template>

    <template v-if="props.withTime" v-slot:append>
      <q-icon name="access_time" class="cursor-pointer">
        <q-popup-proxy cover transition-show="scale" transition-hide="scale">
          <q-time v-model="model" :now-btn="todayBtn" mask="HH:mm" format24h>
            <div class="row items-center justify-end">
              <q-btn v-close-popup label="Выбрать" color="primary" flat/>
            </div>
          </q-time>
        </q-popup-proxy>
      </q-icon>
    </template>
  </q-input>
</template>

<script setup lang="ts">
import {withDefaults, computed} from "vue";
import {Nullable} from "../../types/types";
import {dateHelper} from "../../utils/date_helper";

const emit = defineEmits<{
  (e: 'update:modelValue', value: Nullable<string>): void
}>()

const props = withDefaults(defineProps<{
  modelValue: string | null,
  label?: string,
  withTime?: boolean,
  required?: boolean,
  todayBtn?: boolean,
}>(), {
  withTime: true,
  required: false,
  todayBtn: true
})

const mask = props.withTime ? '##.##.#### ##:##' : '##.##.####';

const model = computed({
    get: () => {
        if (props.modelValue === null) return props.modelValue;
        return dateHelper.utcFormat(props.modelValue);
    },
    set: (val: any) => {
        const l = val.length;

        if (l === 5) {
            // select current time button
            let dateSlice = dateHelper.utcFormat(new Date().toISOString(), 'DD.MM.YYYY');
            if (model.value?.length === 16) {
                dateSlice = model.value.slice(0, 10);
            }
            val = dateSlice + ' ' + val;
        } else if (l === 10) {
            // select current date button
            let timeSlice = '00:00'
            if (model.value?.length === 16) {
                timeSlice = model.value.slice(11, 16);
            }
            val += ' ' + timeSlice;

        } else if (l === 11) {
            val += '00:00';
        } else if (l === 12) {
            let strSplit = val.split('')
            const lastIndex = strSplit.length - 1;
            const lastVal = parseInt(strSplit[lastIndex]);
            strSplit.splice(lastIndex, 1);

            val = strSplit.join('') + '0' + lastVal + ':00';
        } else if (l === 14) {
            val += '00';
        } else if (l === 15) {
            val += '0';
        }
        
        emit('update:modelValue', dateHelper.toISO(val))
    }
});
</script>