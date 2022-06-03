<template>
    <q-banner v-if="model.deletedAt" class="bg-pink-1">
        {{deletedMessages.deletedAt}} <date :date="model.deletedAt"/>
        <q-input v-if="model.deletedAt" v-model="model.deletedReason" type="textarea" maxlength="250" counter autogrow :label="entryInfoMessages.deletedReason"/>
    </q-banner>
    
    <q-input
        v-model="model.title"
        counter
        maxlength="250"
        autogrow
        color="secondary"
        label="Название (не обязательно)"
        type="textarea"
    />
    
    <q-input v-if="entryInfoType === 'Email'" type="email" :label="entryInfoMessages.val.names.Email" v-model="model.info.email"/>
    <q-input v-else-if="entryInfoType === 'Url'" type="url" :label="entryInfoMessages.val.names.Url" v-model="model.info.url"/>
    <phone-field v-else-if="entryInfoType === 'Phone'" v-model="model.info"/>
    <date-field v-else-if="entryInfoType === 'Date'" v-model="model.info.date"/>
    <q-input v-else-if="entryInfoType === 'Note'" 
             v-model="model.info.note" :label="entryInfoMessages.val.names.Note"
             type="text"
    />
<!--    <q-editor v-else-if="entryInfoType === 'Note'"-->
<!--              @paste="onPaste"-->
<!--              ref="editorRef"-->
<!--              toolbar-bg="grey-4"-->
<!--              toolbar-toggle-color="positive"-->
<!--              content-class="bg-none"-->
<!--              class="bg-transparent q-my-md"-->
<!--              v-model="model.info.note"/>-->
    <q-banner v-else class="bg-negative text-white">
        <q-icon name="las la-exclamation-triangle la-fw"/>
        Что то пошло не так, EntryText.textType не определён.
    </q-banner>
</template>

<script setup lang="ts">
import {EntryInfoFormRequest, EntryInfoType} from "../../../api/api_types";
import {computed, ref} from "vue";
import {deletedMessages} from "../../../localize/messages";
import PhoneField from '../../fields/Phone.Field.vue';
import DateField from '../../fields/Date.Field.vue';
import {entryInfoMessages} from "../../../localize/messages";
import {editorHelper} from "../../../utils/editorOnPaste";
import Date from '../../components/Date.vue';

const props = defineProps<{
    modelValue: EntryInfoFormRequest,
    entryInfoType: EntryInfoType
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', value: EntryInfoFormRequest): void
}>()
const model = computed({
    get: (): any => {
        return props.modelValue
    },
    set: (val) => emit('update:modelValue', val)
})

const editorRef = ref(null)
const onPaste = (evt: any) => {
    editorHelper.onPaste(evt, editorRef);
}
</script>