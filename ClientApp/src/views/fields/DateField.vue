<template>
  <q-input color="secondary"
           style="max-width: 300px"
           :mask="mask"
           hide-bottom-space
           v-model="value"
           :label="label"
           @update:model-value="onUpdate"
  >
    <template v-slot:prepend>
      <q-icon name="event" class="cursor-pointer">
        <q-popup-proxy ref="qDateProxyStart" cover transition-show="scale" transition-hide="scale">
          <q-date v-model="value" :mask="format" @update:model-value="onUpdate">
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
          <q-time v-model="value" :mask="format" format24h @update:model-value="onUpdate">
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
import {withDefaults} from "vue";

const emit = defineEmits<{
  (e: 'input', value: string): void
}>()

const props = withDefaults(defineProps<{
  value?: string | null,
  label?: string,
  withTime?: boolean,
}>(), {
  withTime: false,
})

const onUpdate = (e: any) => {
  emit('input', e as any as string);
}
const mask = props.withTime ? '##.##.#### ##:##' : '##.##.####'
const format = props.withTime ? 'DD.MM.YYYY HH:mm' : 'DD.MM.YYYY'
</script>