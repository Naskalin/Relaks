<template>
    <q-card class="profile-card">
        <deleted :deleted="entry"></deleted>

        <q-card-section class="q-pb-none text-center">
            <q-avatar @dblclick="emit('card-dblclick', entry)"
                      @click="isShowAvatarSelect = true"
                      :class="{'cursor-pointer': withEdit}"
                      size="180px"
                      color="grey-5">
                <api-image v-if="entry.avatar" :file-id="entry.avatar" image-filter="square-medium"/>
                <q-icon v-else name="las la-question-circle" color="grey-4" size="150px"/>
                <q-tooltip v-if="withEdit" class="bg-primary">Изменить</q-tooltip>
            </q-avatar>
        </q-card-section>

        <q-card-section class="q-pb-xs q-gutter-y-sm">
            <div class="text-h6 text-center">{{ entry.name }}</div>
            <div v-if="entry.description" class="text-subtitle2 text-center">{{ entry.description }}</div>
            <div class="items-center flex justify-center q-gutter-x-sm">
                <q-icon :name="entryMessages.entryType.icons[entry.entryType]" size="1.4rem"/>
                <span>{{ entryMessages.entryType.names[entry.entryType] }}</span>
                <q-separator vertical class="q-mx-md"/>
                <strong>{{ entry.reputation }}</strong>
                <q-icon name="star" size="1.5em"/>
            </div>
        </q-card-section>

        <q-card-section v-if="withEdit" class="q-gutter-x-sm text-center flex justify-center">
            <q-btn round @click="isShowEditModal = true" v-tooltip="'Изменить объединение'" color="primary" icon="las la-edit"/>
            <q-separator vertical color="grey-5" class="q-mx-sm q-ml-md"/>
            <q-btn round @click="showCreateEntryInfoModal('Phone')" v-tooltip="'Добавить телефон'" color="primary" icon="las la-phone"/>
            <q-btn round @click="showCreateEntryInfoModal('Email')" v-tooltip="'Добавить e-mail'" color="primary" icon="las la-envelope"/>
            <q-btn round @click="showCreateEntryInfoModal('Url')" v-tooltip="'Добавить ссылку'" color="primary" icon="las la-link"/>
            <q-btn round @click="showCreateEntryInfoModal('Date')" v-tooltip="'Добавить дату'" color="primary" icon="las la-calendar"/>
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

        <card-contacts :entry-id="entry.id" :with-edit="withEdit"></card-contacts>

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

    <entry-form-modal
        v-if="withEdit && editStore.model"
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
        v-if="withEdit"
        :title="'Добавление: ' + entryInfoMessages.val.names[entryInfoFormType]"
        :is-create="true"
        :entry-info-type="entryInfoFormType"
        :is-loading="entryInfoCreateStore.isLoading"
        v-model:is-show="isShowCreateModal"
        v-model="entryInfoCreateStore[entryInfoFormType]"
        btn-title="Добавить"
        @submit="createEntryInfo"
    />

    <file-select-modal
        :entry-id="entry.id"
        title="Выбор аватара"
        v-model:is-show="isShowAvatarSelect"
        v-if="withEdit"
        @fileSelect="onFileSelect"
    />
</template>

<script setup lang="ts">
import CardContacts from './Entry.Card.Contacts.vue';
// import ArrowTooltip from '../../components/Arrow.Tooltip.vue';
import EntryFormModal from './Entry.Form.Modal.vue';
import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';
import FileSelectModal from '../../fields/File.Select.Modal.vue';
import Deleted from '../../components/Deleted.vue';
import ApiImage from '../../components/ApiImage.vue';

import {useEntryInfoCreateStore} from "../../../store/entry_info/entryInfo.create.store";
import {withDefaults, ref, watch, onMounted} from "vue";
import {entryMessages, entryInfoMessages} from "../../../localize/messages";
import {Entry, FileModel} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";
import {useEntryEditStore} from "../../../store/entry/entry.edit.store";
import {apiMappers} from "../../../api/api_mappers";
import {EntryInfoType} from "../../../api/api_types";
import {useEntryCardContactsStore} from "../../../store/entry/entryCard.contacts.store";

const editStore = useEntryEditStore();
const isShowEditModal = ref(false);
const isShowAvatarSelect = ref(false);

const props = withDefaults(defineProps<{
    entry: Entry,
    withEdit?: boolean,
}>(), {
    withEdit: false
})

if (props.withEdit) {
    watch([
        () => isShowEditModal.value,
        () => isShowAvatarSelect.value,
    ], (val) => {
        if (val) {
            editStore.model = apiMappers.toEntryUpdateRequest(props.entry);
        }
    }, {immediate: true})
}

const emit = defineEmits<{
    (e: 'update-entry', entryId: string): void,
    (e: 'card-dblclick', entry: Entry): void,
}>()

const updateEntry = async () => {
    await editStore.updateEntry(props.entry.id);
    editStore.$reset();
    isShowEditModal.value = false;

    emit('update-entry', props.entry.id);
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
    await entryInfoCreateStore.create(props.entry.id, entryInfoFormType.value)

    // update card contacts list
    await contactsStore.getAllContacts(props.entry.id);
    isShowCreateModal.value = false;
    entryInfoCreateStore.$reset();
}

// avatar select
const onFileSelect = async (file: FileModel) => {
    if (!editStore.model) throw new Error('onFileSelect => EditStore Entry model is null');
    editStore.model.avatar = file.id;
    isShowAvatarSelect.value = false;
    await updateEntry();
    // await getAvatar(file.id);
}

// avatar
// const avatarSrc = ref<string | null>(null);
// const getAvatar = async (fileId: string) => {
//     const resp = await apiFiles.download({
//         fileId: fileId,
//         imageFilter: 'square-medium'
//     });
//
//     avatarSrc.value = URL.createObjectURL(resp.data);
// }
// onMounted(async () => {
//     if (props.entry.avatar) {
//         await getAvatar(props.entry.avatar);
//     }
// })

onMounted(() => {
    editStore.$reset();
    entryInfoCreateStore.$reset();
})

</script>