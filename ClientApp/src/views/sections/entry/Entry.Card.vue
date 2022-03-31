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
<!--                <small class="profile-card__caption">{{ entryMessages.reputation }}</small>-->
                <strong>{{ entry.reputation }}</strong>
                <q-icon name="star" size="1.5em"/>
            </div>
        </q-card-section>

        <q-card-section v-if="withEdit" class="q-gutter-x-sm text-center">
            <q-btn round @click="isShowEditModal = true" color="primary" icon="las la-edit">
                <q-tooltip anchor="center right" self="center left" :offset="[5, 10]" class="bg-secondary"
                           style="font-size: .9em">
                    Изменить объединение
                </q-tooltip>
            </q-btn>
            <q-btn round @click="showCreateEntryTextModal('Phone')" color="primary" icon="las la-phone">
                <arrow-tooltip direction="top">Добавить телефон</arrow-tooltip>
            </q-btn>
            <q-btn round @click="showCreateEntryTextModal('Email')" color="primary" icon="las la-envelope">
                <arrow-tooltip direction="top">Добавить e-mail</arrow-tooltip>
            </q-btn>
            <q-btn round @click="showCreateEntryTextModal('Url')" color="primary" icon="las la-link">
                <arrow-tooltip direction="top">Добавить ссылку</arrow-tooltip>
            </q-btn>
        </q-card-section>

        <card-contacts :entry-id="entry.id"></card-contacts>

        <template v-if="entry.startAt || entry.endAt">
            <q-card-section>
                <div class="row">
                    <div v-if="entry.startAt" class="col text-left">
                        <small class="profile-card__caption">{{ entryMessages.startAt[entry.entryType] }}</small>
                        <br>
                        {{ dateHelper.utcFormat(entry.startAt) }}
                    </div>
                    <div v-if="entry.endAt" class="col text-right">
                        <small class="profile-card__caption">{{ entryMessages.endAt[entry.entryType] }}</small>
                        <br>
                        {{ dateHelper.utcFormat(entry.endAt) }}
                    </div>
                </div>
            </q-card-section>

            <q-separator/>
        </template>

        <q-card-section>
            <small class="profile-card__caption q-mr-xs">{{ actualMessages.actualStartAt.name }}</small>
            {{ dateHelper.utcFormat(entry.actualStartAt) }}
            <div v-if="entry.actualStartAtReason">
                <small class="profile-card__caption q-mr-xs">{{ actualMessages.actualStartAtReason.name }}</small>
                {{ entry.actualStartAtReason }}
            </div>
        </q-card-section>

        <template v-if="entry.actualEndAt || entry.actualEndAtReason">
            <q-separator/>
            <q-card-section>
                <template v-if="entry.actualEndAt">
                    <small class="profile-card__caption q-mr-xs">{{ actualMessages.actualEndAt.name }}</small>
                    {{ dateHelper.utcFormat(entry.actualEndAt) }}
                </template>
                <div v-if="entry.actualEndAtReason">
                    <small class="profile-card__caption q-mr-xs">{{ actualMessages.actualEndAtReason.name }}</small>
                    {{ entry.actualEndAtReason }}
                </div>
            </q-card-section>
        </template>

        <q-separator/>

        <q-card-section>
            <div class="row">
                <div class="col text-left">
                    <small class="profile-card__caption">создано</small>
                    <br>
                    <span class="text-grey-7">{{ dateHelper.utcFormat(entry.createdAt) }}</span>
                </div>
                <div class="col text-right">
                    <small class="profile-card__caption">обновлено</small>
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
    <entry-text-form-modal
        :title="'Добавление: ' + entryTextMessages.val.names[entryTextCreateStore.model.textType]"
        :is-create="true"
        :is-loading="entryTextCreateStore.isLoading"
        v-model:is-show="isShowCreateEntryTextModal"
        v-model="entryTextCreateStore.model"
        btn-title="Добавить"
        @submit="createEntryText"
    />
</template>

<script setup lang="ts">
import CardContacts from './Entry.Card.Contacts.vue';
import ArrowTooltip from '../../components/Arrow.Tooltip.vue';
import {useEntryTextCreateStore} from "../../../store/entry_text/entryText.create.store";
import {withDefaults, ref, watch} from "vue";
import {entryMessages, actualMessages, entryTextMessages} from "../../../localize/messages";
import {Entry} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";
import EntryFormModal from './Entry.Form.Modal.vue';
import {useEntryEditStore} from "../../../store/entry/entry.edit.store";
import {entryMappers} from "../../../api/api_mappers";
import {useRoute} from "vue-router";
import {EntryTextType} from "../../../api/api_types";
import EntryTextFormModal from '../entry_text/EntryText.Form.Modal.vue';
import {useEntryContactsStore} from "../../../store/entry_contacts/entry_cotacts_store";

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
            editStore.model = entryMappers.toUpdateRequest(props.entry);
        }
    })
}
const emit = defineEmits<{
    (e: 'update-entry', entryId: string): void
}>()

const route = useRoute();

const entryId = route.params.entryId as string;
const updateEntry = async () => {
    await editStore.updateEntry(entryId);
    editStore.$reset();
    isShowEditModal.value = false;

    emit('update-entry', props.entry.id);
}

// Добавление entryText email/phone/url
const entryTextCreateStore = useEntryTextCreateStore();
const isShowCreateEntryTextModal = ref(false);
const showCreateEntryTextModal = (textType: EntryTextType) => {
    if (props.withEdit) {
        entryTextCreateStore.model.actualStartAt = new Date().toISOString();
        entryTextCreateStore.model.textType = textType;
        isShowCreateEntryTextModal.value = true
    }
}
const entryContactsStore = useEntryContactsStore();
const createEntryText = async () => {
    await entryTextCreateStore.create(entryId);
    await entryContactsStore.getAllContacts(entryId);
    isShowCreateEntryTextModal.value = false;
    entryTextCreateStore.$reset();
}
</script>

<style lang="scss">
.profile-card {
    &__caption {
        color: $grey-7;
        text-transform: uppercase;
    }
}
</style>