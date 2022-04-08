<template>
    <template v-if="contactsStore.phones.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list separator>
                <q-item v-for="eInfo in contactsStore.phones" :class="{'bg-pink-1': eInfo.deletedAt}" :key="eInfo.id">
                    <q-item-section>
                        <q-item-label><phone :phone="eInfo"></phone></q-item-label>
                        <q-item-label v-if="eInfo.title" :lines="2" class="text-grey-9">{{ eInfo.title }}</q-item-label>
                        <meta-tooltip :meta="eInfo"></meta-tooltip>
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
                        <q-item-label><email :email="eInfo.email" with-link></email></q-item-label>
                        <q-item-label v-if="eInfo.title" :lines="2" class="text-grey-9">{{ eInfo.title }}</q-item-label>
                        <meta-tooltip :meta="eInfo"></meta-tooltip>
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
                        <q-item-label><url :url="eInfo.url" with-link></url></q-item-label>
                        <q-item-label v-if="eInfo.title" :lines="2" class="text-grey-9">{{ eInfo.title }}</q-item-label>
                        <meta-tooltip :meta="eInfo"></meta-tooltip>
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
                        <q-item-label>
                            <date :date="eInfo.date"></date>
                        </q-item-label>
                        <q-item-label v-if="eInfo.title" :lines="2" class="text-grey-9">{{ eInfo.title }}</q-item-label>
                        <meta-tooltip :meta="eInfo"></meta-tooltip>
                    </q-item-section>
                    <q-item-section side v-if="withEdit">
                        <q-btn icon="las la-edit" @click="showEditForm(eInfo)" flat round color="primary"/>
                    </q-item-section>
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <entry-info-form-modal v-if="withEdit && editStore[entryInfoType].model"
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
                           @archive="onArchive"
                           @recover="saveEditForm"
    />
</template>

<script setup lang="ts">
import {watch, ref, withDefaults} from "vue";
import {useEntryCardContactsStore} from "../../../store/entry/entryCard.contacts.store";
import Phone from '../../components/Phone.vue';
import Email from '../../components/Email.vue';
import Url from '../../components/Url.vue';
import Date from '../../components/Date.vue';
import MetaTooltip from '../../components/MetaTooltip.vue';
import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';

// import ActualTimestampTooltip from '../../components/Actual.Timestamp.Tooltip.vue';
import EntryTextFormModal from '../entry_text/EntryText.Form.Modal.vue';
// import {useEntryTextEditStore} from "../../../store/entry_text/entryText.edit.store";
// import {EntryText} from "../../../api/api_types";
// import {apiMappers} from "../../../api/api_mappers";
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useEntryInfoEditStore} from "../../../store/entry_info/entryInfo.edit.store";
import {useRouter} from 'vue-router';
import {entryInfoMessages} from "../../../localize/messages";
import {EntryInfo, EntryInfoType} from "../../../api/api_types";

const isShowDeletedContacts = ref(false);
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

// show-edit
// const router = useRouter();
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

// save-edit
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

// archive
const onArchive = async () => {
    await editStore.update(props.entryId, currentEditId.value, entryInfoType.value);
    await onDelete();
}
</script>