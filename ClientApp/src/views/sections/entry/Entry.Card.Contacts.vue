<template>
    <template v-if="contactsStore.phones.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in contactsStore.phones"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="contactsStore.emails.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in contactsStore.emails"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="contactsStore.urls.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in contactsStore.urls"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="contactsStore.dates.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in contactsStore.dates"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <entry-info-form-modal v-if="withEdit && editStore[entryInfoType].model !== null"
                           v-model="editStore[entryInfoType].model"
                           v-model:is-show="isShowEditModal"
                           :entry-info-type="entryInfoType"
                           :is-loading="editStore.isLoading"
                           :is-create="false"
                           :title="'Изменение: ' + entryInfoMessages.val.names[entryInfoType]"
                           btn-title="Сохранить"
                           btn-icon="las la-save"
                           @submit="saveEditForm"
                           @delete="onDelete"
                           @softDelete="onSoftDelete"
                           @recover="onRecover"
    />
</template>

<script setup lang="ts">
import {watch, ref, withDefaults} from "vue";
import {useEntryCardContactsStore} from "../../../store/entry/entryCard.contacts.store";
import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';
import EntryInfoItem from '../entry_info/EntryInfo.Item.vue';
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useEntryInfoEditStore} from "../../../store/entry_info/entryInfo.edit.store";
import {entryInfoMessages} from "../../../localize/messages";
import {EntryInfoType} from "../../../api/api_types";

const props = withDefaults(defineProps<{
    entryId: string,
    withEdit?: boolean,
    withDeleted?: boolean
}>(), {
    withDeleted: false,
});

// show
const contactsStore = useEntryCardContactsStore();
watch(() => props.entryId, async () => {
    await contactsStore.getAllContacts(props.entryId);
}, {immediate: true})

// show edit form
const editStore = useEntryInfoEditStore();
const isShowEditModal = ref(false);
const currentEditId = ref('');
const entryInfoType = ref<EntryInfoType>('Phone');
const showEditForm = (eInfo: any) => {
    if (props?.withEdit !== true) {
        return;
    }

    entryInfoType.value = eInfo.discriminator;
    editStore.setModel(eInfo, eInfo.discriminator);
    isShowEditModal.value = true;
    currentEditId.value = eInfo.id;
}

// save edit
const saveEditForm = async () => {
    await editStore.update(props.entryId, currentEditId.value, entryInfoType.value);
    await contactsStore.getAllContacts(props.entryId);
    isShowEditModal.value = false;
    editStore.$reset();
}

// delete
const onDelete = async () => {
    await apiEntryInfo[entryInfoType.value].delete(props.entryId, currentEditId.value);
    await contactsStore.getAllContacts(props.entryId);
    isShowEditModal.value = false;
    editStore.$reset();
}

// softDelete
const onSoftDelete = async () => {
    await saveEditForm();
}

// recover
const onRecover = async () => {
    await saveEditForm();
}
</script>