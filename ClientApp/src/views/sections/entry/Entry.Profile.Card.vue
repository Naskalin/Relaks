<template>
  <q-card v-if="entry" class="profile-card">
    <q-card-section class="text-center">
      <div class="q-mb-md">
        <q-avatar size="150px" rounded>
          <img
              src="https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=580&q=80">
        </q-avatar>
      </div>
      
      <div class="text-h6">{{entry.name}}</div>
      <div v-if="entry.description" class="text-subtitle2">{{entry.description}}</div>
      <div class="items-center flex justify-center q-gutter-sm q-mt-xs">
        <small class="text-grey-7 text-uppercase">репутация</small>
        <strong>{{entry.reputation}}</strong>
        <q-icon name="star" size="1.5em" />
      </div>
    </q-card-section>

    <q-card-section>
      <div class="row">
        <div v-if="entry.startAt" class="col text-left">
          <small class="text-grey-7 text-uppercase">{{entryMessages.startAt[entry.entryType]}}</small>
          <br>
          {{ dateHelper.utcFormat(entry.startAt) }}
        </div>
        <div v-if="entry.endAt" class="col text-right">
          <small class="text-grey-7 text-uppercase">{{entryMessages.endAt[entry.entryType]}}</small>
          <br>
          {{ dateHelper.utcFormat(entry.endAt) }}
        </div>
      </div>
    </q-card-section>

    <q-separator/>
    
    <q-card-section>
      <small class="text-grey-7 text-uppercase q-mr-xs">{{actualMessages.actualStartAt.name}}</small>
      {{ dateHelper.utcFormat(entry.actualStartAt) }}
      <div v-if="entry.actualStartAtReason">
        <small class="text-grey-7 text-uppercase q-mr-xs">{{actualMessages.actualStartAtReason.name}}</small>
        {{entry.actualStartAtReason}}
      </div>
    </q-card-section>

    <template v-if="entry.actualEndAt || entry.actualEndAtReason">
      <q-separator/>
      <q-card-section>
        <template v-if="entry.actualEndAt">
          <small class="text-grey-7 text-uppercase q-mr-xs">{{actualMessages.actualEndAt.name}}</small>
          {{ dateHelper.utcFormat(entry.actualEndAt) }}
        </template>
        <div v-if="entry.actualEndAtReason">
          <small class="text-grey-7 text-uppercase q-mr-xs">{{actualMessages.actualEndAtReason.name}}</small>
          {{entry.actualEndAtReason}}
        </div>
      </q-card-section>
    </template>
    
    <q-separator/>

    <q-card-section>
      <div class="row">
        <div class="col text-left text-grey-7">
          <small class="text-uppercase">создано</small>
          <br>
          {{ dateHelper.utcFormat(entry.createdAt) }}
        </div>
        <div class="col text-right text-grey-7">
          <small class="text-uppercase">обновлено</small>
          <br>
          {{ dateHelper.utcFormat(entry.updatedAt) }}
        </div>
      </div>
    </q-card-section>

    <entry-edit-modal></entry-edit-modal>
    <q-card-actions>
      <q-btn flat @click="showEditForm">Изменить</q-btn>
    </q-card-actions>
  </q-card>
</template>

<script setup lang="ts">
import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
import {useEntryEditModal} from "../../../store/entry/entry.edit.modal.store";
import {computed, watch} from "vue";
import {entryMessages, actualMessages} from "../../../localize/messages";
import {Entry} from "../../../api/resource_types";
import {dateHelper} from "../../../utils/date_helper";
import EntryEditModal from './Entry.Edit.Modal.vue';

const profileStore = useEntryProfileStore();
const editStore = useEntryEditModal();
const entry = computed(() : Entry | null => profileStore.entry);

watch(() => profileStore.entry, (val) => {
  // if (val) editStore.$state.model = val;
}, {immediate: true})

const showEditForm = () => {
  editStore.isShowModal = true;
}
</script>