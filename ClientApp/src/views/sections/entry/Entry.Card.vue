<template>
    <q-card class="profile-card">
        <deleted :deleted="entry"></deleted>

        <q-card-section class="q-pb-none text-center">
            <q-avatar @dblclick="emit('card-dblclick', entry)" size="180px" font-size="150px" color="grey-5"
                      text-color="grey-4" icon="las la-question-circle"/>
            
        </q-card-section>

        <q-card-section class="q-pb-xs q-gutter-y-sm">
            <div class="text-h6 text-center">{{ entry.name }}</div>
            <div v-if="entry.description" class="text-subtitle2 text-center">{{ entry.description }}</div>
            <div class="items-center flex justify-center q-gutter-x-sm">
                <q-icon :name="entryMessages.entryType.icons[entry.entryType]" size="1.4rem"/>
                <span>{{entryMessages.entryType.names[entry.entryType]}}</span>
                <q-separator vertical class="q-mx-md"/>
                <strong>{{ entry.reputation }}</strong>
                <q-icon name="star" size="1.5em"/>
            </div>
        </q-card-section>

        <q-card-section v-if="withEdit" class="q-gutter-x-sm text-center flex justify-center">
            <q-btn round @click="isShowEditModal = true" color="primary" icon="las la-edit">
                <arrow-tooltip direction="top">Изменить объединение</arrow-tooltip>
            </q-btn>
            <q-separator vertical color="grey-5" class="q-mx-sm q-ml-md"/>
            <q-btn round @click="showCreateEntryInfoModal('Phone')" color="primary" icon="las la-phone">
                <arrow-tooltip direction="top">Добавить телефон</arrow-tooltip>
            </q-btn>
            <q-btn round @click="showCreateEntryInfoModal('Email')" color="primary" icon="las la-envelope">
                <arrow-tooltip direction="top">Добавить e-mail</arrow-tooltip>
            </q-btn>
            <q-btn round @click="showCreateEntryInfoModal('Url')" color="primary" icon="las la-link">
                <arrow-tooltip direction="top">Добавить ссылку</arrow-tooltip>
            </q-btn>
            <q-btn round @click="showCreateEntryInfoModal('Date')" color="primary" icon="las la-calendar">
                <arrow-tooltip direction="top">Добавить дату</arrow-tooltip>
            </q-btn>
        </q-card-section>

        <q-card-section class="text-center">
            <q-btn-toggle
                v-model="contactsStore.isShowDeleted"
                toggle-color="secondary"
                size="sm"
                :options="[
          {label: 'Все контакты', value: null},
          {label: 'Актуальные', value: false},
          {label: 'Архив', value: true}
        ]"
            />
        </q-card-section>

        <card-contacts :entry-id="entryId" :with-edit="withEdit"></card-contacts>

        <template v-if="entry.startAt || entry.endAt">
            <q-card-section>
                <div class="row">
                    <div v-if="entry.startAt" class="col text-left">
                        <span class="label-caption text-grey-8">{{ entryMessages.startAt[entry.entryType] }}</span>
                        <br>
                        {{ dateHelper.utcFormat(entry.startAt) }}
                    </div>
                    <div v-if="entry.endAt" class="col text-right">
                        <span class="label-caption text-grey-8">{{ entryMessages.endAt[entry.entryType] }}</span>
                        <br>
                        {{ dateHelper.utcFormat(entry.endAt) }}
                    </div>
                </div>
            </q-card-section>

            <q-separator/>
        </template>

        <q-card-section>
            <div class="row">
                <div class="col text-left">
                    <span class="label-caption text-grey-8">создано</span>
                    <br>
                    <span class="text-grey-7">{{ dateHelper.utcFormat(entry.createdAt) }}</span>
                </div>
                <div class="col text-right">
                    <span class="label-caption text-grey-8">обновлено</span>
                    <br>
                    <span class="text-grey-7">{{ dateHelper.utcFormat(entry.updatedAt) }}</span>
                </div>
            </div>
        </q-card-section>
    </q-card>

    <entry-form-modal v-if="withEdit && editStore.model"
                      title="Изменение объединения"
                      btn-title="Сохранить"
                      btn-icon="las la-save"
                      :is-loading="editStore.isLoading"
                      :is-create="false"
                      v-model="editStore.model"
                      v-model:is-show="isShowEditModal"
                      @submit="updateEntry"
    />

    <entry-info-form-modal
        :title="'Добавление: ' + entryInfoMessages.val.names[entryInfoFormType]"
        :is-create="true"
        :entry-info-type="entryInfoFormType"
        :is-loading="entryInfoCreateStore.isLoading"
        v-model:is-show="isShowCreateModal"
        v-model="entryInfoCreateStore[entryInfoFormType]"
        btn-title="Добавить"
        @submit="createEntryInfo"
    />
</template>

<script setup lang="ts">
import CardContacts from './Entry.Card.Contacts.vue';
import ArrowTooltip from '../../components/Arrow.Tooltip.vue';
import EntryFormModal from './Entry.Form.Modal.vue';
import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';
import Deleted from '../../components/Deleted.vue';

import {useEntryInfoCreateStore} from "../../../store/entry_info/entryInfo.create.store";
import {withDefaults, ref, watch} from "vue";
import {entryMessages, entryInfoMessages} from "../../../localize/messages";
import {Entry} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";
import {useEntryEditStore} from "../../../store/entry/entry.edit.store";
import {apiMappers} from "../../../api/api_mappers";
import {EntryInfoType} from "../../../api/api_types";
import {useEntryCardContactsStore} from "../../../store/entry/entryCard.contacts.store";

const editStore = useEntryEditStore();
const isShowEditModal = ref(false);
const props = withDefaults(defineProps<{
    entry: Entry,
    withEdit?: boolean,
}>(), {
    withEdit: false
})

if (props.withEdit) {
    watch(() => isShowEditModal.value, (val) => {
        if (val) {
            editStore.model = apiMappers.toEntryUpdateRequest(props.entry);
        }
    })
}
const emit = defineEmits<{
    (e: 'update-entry', entryId: string): void,
    (e: 'card-dblclick', entry: Entry): void,
}>()

// const route = useRoute();

const entryId = props.entry.id;
const updateEntry = async () => {
    await editStore.updateEntry(entryId);
    editStore.$reset();
    isShowEditModal.value = false;

    emit('update-entry', entryId);
}

// Добавление entryText email/phone/url
const entryInfoCreateStore = useEntryInfoCreateStore();
const isShowCreateModal = ref(false);
const entryInfoFormType = ref<EntryInfoType>('Phone');
const showCreateEntryInfoModal = (textType: EntryInfoType) => {
    if (props.withEdit) {
        entryInfoFormType.value = textType;
        isShowCreateModal.value = true
    }
}

// card contacts
const contactsStore = useEntryCardContactsStore();
const createEntryInfo = async () => {
    // Создаём entryInfo
    await entryInfoCreateStore.create(entryId, entryInfoFormType.value)

    // update card contacts list
    await contactsStore.getAllContacts(entryId);
    isShowCreateModal.value = false;
    entryInfoCreateStore.$reset();
}
</script>