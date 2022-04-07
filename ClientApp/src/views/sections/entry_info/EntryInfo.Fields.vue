<template>
    <q-input v-if="entryInfoType === 'Email'" type="email" :label="entryInfoMessages.val.names.Email" v-model="model.email"/>
    <q-input v-else-if="entryInfoType === 'Url'" type="url" :label="entryInfoMessages.val.names.Url" v-model="model.url"/>
    <phone-field v-else-if="entryInfoType === 'Phone'" v-model="model"/>
    <date-field v-else-if="entryInfoType === 'Date'" v-model="model.date"/>
    <q-editor v-else-if="entryInfoType === 'Note'"
              @paste="onPaste"
              ref="editorRef"
              toolbar-bg="grey-4"
              toolbar-toggle-color="positive"
              content-class="bg-none"
              class="bg-transparent"
              v-model="model.val"/>
    <q-banner v-else class="bg-negative text-white">
        <q-icon name="las la-exclamation-triangle la-fw"/>
        Что то пошло не так, EntryText.textType не определён.
    </q-banner>
    
    <q-input
        v-model="model.title"
        counter
        autogrow
        color="secondary"
        label="Название"
        type="textarea"
    />
    
<!--    <q-input v-model="model.deletedReason" type="text" :label="entryInfoMessages.deletedReason"/>-->
<!--    <actual-fieldset v-model="model"></actual-fieldset>-->
</template>

<script setup lang="ts">
import {
    EntryEmailFormRequest,
    EntryUrlFormRequest,
    EntryPhoneFormRequest,
    EntryDateFormRequest,
    EntryNoteFormRequest,
    EntryInfoType
} from "../../../api/api_types";
import {computed, ref} from "vue";
// import ActualFieldset from '../../fieldsets/Actual.Fieldset.vue';
import PhoneField from '../../fields/Phone.Field.vue';
import DateField from '../../fields/Date.Field.vue';
import {entryInfoMessages} from "../../../localize/messages";
import {editorHelper} from "../../../utils/editorOnPaste";

const props = defineProps<{
    modelValue: EntryEmailFormRequest | EntryUrlFormRequest | EntryPhoneFormRequest | EntryDateFormRequest | EntryNoteFormRequest,
    entryInfoType: EntryInfoType
}>()
const emit = defineEmits<{
    (e: 'update:modelValue', value: EntryEmailFormRequest | EntryUrlFormRequest | EntryPhoneFormRequest | EntryDateFormRequest | EntryNoteFormRequest): void
}>()
const model = computed({
    get: (): any => {
        return props.modelValue
    },
    set: (val) => emit('update:modelValue', val as any)
})

const editorRef = ref(null)
const onPaste = (evt: any) => {
    editorHelper.onPaste(evt, editorRef);
}
</script>