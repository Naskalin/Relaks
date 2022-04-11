<template>
    <template v-if="contactsStore.phones.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list separator>
                <q-item v-for="eInfo in contactsStore.phones" :class="{'bg-pink-1': eInfo.deletedAt}" :key="eInfo.id">
                    <q-item-section>
                        <phone :phone="eInfo"></phone>
                        <q-item-label v-if="eInfo.title" :lines="1" class="text-grey-9">{{ eInfo.title }}</q-item-label>
                        <q-item-label v-if="eInfo.deletedReason" :lines="1" class="text-negative">{{ eInfo.deletedReason }}</q-item-label>
                        <entry-info-tooltip :e-info="eInfo"/>
                    </q-item-section>
                    <q-item-section side v-if="withEdit">
                        <q-btn icon="las la-edit" @click="showEditForm(eInfo)" flat round color="primary"/>
                    </q-item-section>
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="contactsStore.emails.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list separator>
                <q-item v-for="eInfo in contactsStore.emails" :class="{'bg-pink-1': eInfo.deletedAt}" :key="eInfo.id">
                    <q-item-section>
                        <email :email="eInfo.email" with-link></email>
                        <q-item-label v-if="eInfo.title" :lines="1" class="text-grey-9">{{ eInfo.title }}</q-item-label>
                        <q-item-label v-if="eInfo.deletedReason" :lines="1" class="text-negative">{{ eInfo.deletedReason }}</q-item-label>
                        <entry-info-tooltip :e-info="eInfo"/>
                    </q-item-section>
                    <q-item-section side v-if="withEdit">
                        <q-btn icon="las la-edit" @click="showEditForm(eInfo)" flat round color="primary"/>
                    </q-item-section>
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="contactsStore.urls.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list separator>
                <q-item v-for="eInfo in contactsStore.urls" :class="{'bg-pink-1': eInfo.deletedAt}" :key="eInfo.id">
                    <q-item-section>
                        <url :url="eInfo.url" with-link></url>
                        <q-item-label :lines="1" v-if="eInfo.title" class="text-grey-9">{{ eInfo.title }}</q-item-label>
                        <q-item-label v-if="eInfo.deletedReason" :lines="1" class="text-negative">{{ eInfo.deletedReason }}</q-item-label>
                        <entry-info-tooltip :e-info="eInfo"/>
                    </q-item-section>
                    <q-item-section side v-if="withEdit">
                        <q-btn icon="las la-edit" @click="showEditForm(eInfo)" flat round color="primary"/>
                    </q-item-section>
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="contactsStore.dates.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list separator>
                <q-item v-for="eInfo in contactsStore.dates" :class="{'bg-pink-1': eInfo.deletedAt}" :key="eInfo.id">
                    <q-item-section>
                        <date :date="eInfo.date"></date>
                        <q-item-label v-if="eInfo.title" :lines="1" class="text-grey-9">{{ eInfo.title }}</q-item-label>
                        <q-item-label v-if="eInfo.deletedReason" :lines="1" class="text-negative">{{ eInfo.deletedReason }}</q-item-label>
                        <entry-info-tooltip :e-info="eInfo"/>
                    </q-item-section>
                    <q-item-section side v-if="withEdit">
                        <q-btn icon="las la-edit" @click="showEditForm(eInfo)" flat round color="primary"/>
                    </q-item-section>
                </q-item>
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
import Phone from '../../components/Phone.vue';
import Email from '../../components/Email.vue';
import Url from '../../components/Url.vue';
import Date from '../../components/Date.vue';
import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';
import EntryInfoTooltip from '../entry_info/EntryInfo.Tooltip.vue';
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