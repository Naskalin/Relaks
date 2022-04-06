<template>
    <q-card class="profile-card">
        <q-card-section class="q-pb-none text-center">
            <!--            <div class="profile-card__edit">-->
            <!--            -->
            <!--            </div>-->
            <!--            <q-avatar size="180px">-->
            <!--                <img-->
            <!--                    src="https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=580&q=80">-->
            <!--            </q-avatar>-->
            <q-avatar size="180px" font-size="150px" color="grey-5" text-color="grey-4" icon="las la-question-circle"/>
        </q-card-section>

        <q-card-section class="q-pb-none">
            <div class="text-h6 text-center">{{ entry.name }}</div>
            <div v-if="entry.description" class="text-subtitle2 text-center">{{ entry.description }}</div>
            <div class="items-center flex justify-center q-gutter-x-sm">
                <strong>{{ entry.reputation }}</strong>
                <q-icon name="star" size="1.5em"/>
            </div>
        </q-card-section>

        <!--        <q-card-section v-if="withEdit" class="q-gutter-x-sm text-center">-->
        <!--            <q-btn round @click="isShowEditModal = true" color="primary" icon="las la-edit">-->
        <!--                <q-tooltip anchor="center right" self="center left" :offset="[5, 10]" class="bg-secondary"-->
        <!--                           style="font-size: .9em">-->
        <!--                    Изменить объединение-->
        <!--                </q-tooltip>-->
        <!--            </q-btn>-->
        <!--            <q-btn round @click="showCreateEntryInfoModal('Phone')" color="secondary" icon="las la-phone">-->
        <!--                <arrow-tooltip direction="top">Добавить телефон</arrow-tooltip>-->
        <!--            </q-btn>-->
        <!--            <q-btn round @click="showCreateEntryInfoModal('Email')" color="secondary" icon="las la-envelope">-->
        <!--                <arrow-tooltip direction="top">Добавить e-mail</arrow-tooltip>-->
        <!--            </q-btn>-->
        <!--            <q-btn round @click="showCreateEntryInfoModal('Url')" color="secondary" icon="las la-link">-->
        <!--                <arrow-tooltip direction="top">Добавить ссылку</arrow-tooltip>-->
        <!--            </q-btn>-->
        <!--        </q-card-section>-->

        <card-contacts :entry-id="entry.id" :with-edit="true"></card-contacts>

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

        <!--        <q-card-section>-->
        <!--            <span class="label-caption text-grey-8 q-mr-xs">{{ actualMessages.actualStartAt.name }}</span>-->
        <!--            {{ dateHelper.utcFormat(entry.actualStartAt) }}-->
        <!--            <div v-if="entry.actualStartAtReason">-->
        <!--                <span class="label-caption text-grey-8 q-mr-xs">{{ actualMessages.actualStartAtReason.name }}</span>-->
        <!--                {{ entry.actualStartAtReason }}-->
        <!--            </div>-->
        <!--        </q-card-section>-->

        <!--        <template v-if="entry.actualEndAt || entry.actualEndAtReason">-->
        <!--            <q-separator/>-->
        <!--            <q-card-section>-->
        <!--                <template v-if="entry.actualEndAt">-->
        <!--                    <span class="label-caption text-grey-8 q-mr-xs">{{ actualMessages.actualEndAt.name }}</span>-->
        <!--                    {{ dateHelper.utcFormat(entry.actualEndAt) }}-->
        <!--                </template>-->
        <!--                <div v-if="entry.actualEndAtReason">-->
        <!--                    <span class="label-caption text-grey-8 q-mr-xs">{{ actualMessages.actualEndAtReason.name }}</span>-->
        <!--                    {{ entry.actualEndAtReason }}-->
        <!--                </div>-->
        <!--            </q-card-section>-->
        <!--        </template>-->

<!--        <q-separator/>-->

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

    <!--    <entry-form-modal v-if="withEdit && editStore.model"-->
    <!--                      title="Изменение объединения"-->
    <!--                      btn-title="Сохранить"-->
    <!--                      btn-icon="las la-save"-->
    <!--                      :is-loading="editStore.isLoading"-->
    <!--                      :is-create="false"-->
    <!--                      v-model="editStore.model"-->
    <!--                      v-model:is-show="isShowEditModal"-->
    <!--                      @submit="updateEntry"-->
    <!--    />-->
    <entry-info-form-modal
        :title="'Добавление: ' + entryInfoMessages.val.names[entryInfoFormType]"
        :is-create="true"
        :entry-info-type="entryInfoFormType"
        :is-loading="entryInfoCreateStore.isLoading"
        v-model:is-show="isShowCreateModal"
        v-model="entryInfoCreateStore.model"
        btn-title="Добавить"
        @submit="createEntryInfo"
    />
</template>

<script setup lang="ts">
import CardContacts from './Entry.Card.Contacts.vue';
import ArrowTooltip from '../../components/Arrow.Tooltip.vue';
import EntryFormModal from './Entry.Form.Modal.vue';
import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';

import {useEntryInfoCreateStore} from "../../../store/entry_info/entryInfo.create.store";
import {withDefaults, ref, watch} from "vue";
import {entryMessages, entryInfoMessages} from "../../../localize/messages";
import {Entry} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";
import {useEntryEditStore} from "../../../store/entry/entry.edit.store";
import {apiMappers} from "../../../api/api_mappers";
import {useRoute} from "vue-router";
import {EntryInfoType} from "../../../api/api_types";
import {useEntryInfoListStore} from "../../../store/entry_info/entryInfo.list.store";

const editStore = useEntryEditStore();
const isShowEditModal = ref(false);
const props = withDefaults(defineProps<{
    entry: Entry,
    withEdit?: boolean,
}>(), {
    withEdit: false
})

// if (props.withEdit) {
//     watch(() => isShowEditModal.value, (val) => {
//         if (val) {
//             editStore.model = apiMappers.toEntryFormRequest(props.entry);
//         }
//     })
// }
// const emit = defineEmits<{
//     (e: 'update-entry', entryId: string): void
// }>()
//
const route = useRoute();

const entryId = route.params.entryId as string;
// const updateEntry = async () => {
//     await editStore.updateEntry(entryId);
//     editStore.$reset();
//     isShowEditModal.value = false;
//
//     emit('update-entry', props.entry.id);
// }

// Добавление entryText email/phone/url
const entryInfoCreateStore = useEntryInfoCreateStore();
const isShowCreateModal = ref(false);
const entryInfoFormType = ref<EntryInfoType>('Phone');
const showCreateEntryInfoModal = (textType: EntryInfoType) => {
    if (props.withEdit) {
        entryInfoFormType.value = textType;
        // entryInfoCreateStore.model.actualStartAt = new Date().toISOString();
        // entryInfoCreateStore.model.textType = textType;
        isShowCreateModal.value = true
    }
}

// card contacts
const entryInfoListStore = useEntryInfoListStore();
const createEntryInfo = async (entryInfoType: EntryInfoType) => {
    // Создаём entryInfo
    await entryInfoCreateStore.create(entryId, entryInfoType)
    
    // update card contacts list
    await entryInfoListStore.getAllContacts(entryId);
    isShowCreateModal.value = false;
    entryInfoCreateStore.$reset();
}
</script>