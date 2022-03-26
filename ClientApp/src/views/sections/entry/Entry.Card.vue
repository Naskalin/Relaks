<template>
    <q-card class="profile-card">
        <q-card-section class="q-pb-none text-center">
            <div class="profile-card__edit">
                <q-btn round v-if="withEdit" @click="isShowEditModal = true" color="primary" icon="las la-edit">
                    <q-tooltip anchor="center left" self="center right" :offset="[5, 10]" class="bg-secondary">
                        Изменить объединение
                    </q-tooltip>
                </q-btn>
            </div>
            <q-avatar size="180px">
                <img
                    src="https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=580&q=80">
            </q-avatar>
            
<!--            <div class="row">-->
<!--                <div class="col-auto">-->
<!--&lt;!&ndash;                    <div>&ndash;&gt;-->
<!--&lt;!&ndash;                        <q-btn round v-if="withEdit" color="primary" icon="las la-phone">&ndash;&gt;-->
<!--&lt;!&ndash;                            <q-tooltip anchor="center right" self="center left" :offset="[5, 10]" class="bg-secondary">&ndash;&gt;-->
<!--&lt;!&ndash;                                Добавить телефон&ndash;&gt;-->
<!--&lt;!&ndash;                            </q-tooltip>&ndash;&gt;-->
<!--&lt;!&ndash;                        </q-btn>&ndash;&gt;-->
<!--&lt;!&ndash;                    </div>&ndash;&gt;-->
<!--&lt;!&ndash;                    <div>&ndash;&gt;-->
<!--&lt;!&ndash;                        <q-btn round v-if="withEdit" color="primary" icon="las la-envelope">&ndash;&gt;-->
<!--&lt;!&ndash;                            <q-tooltip anchor="center right" self="center left" :offset="[5, 10]" class="bg-secondary">&ndash;&gt;-->
<!--&lt;!&ndash;                                Добавить e-mail&ndash;&gt;-->
<!--&lt;!&ndash;                            </q-tooltip>&ndash;&gt;-->
<!--&lt;!&ndash;                        </q-btn>&ndash;&gt;-->
<!--&lt;!&ndash;                    </div>&ndash;&gt;-->
<!--&lt;!&ndash;                    <div>&ndash;&gt;-->
<!--&lt;!&ndash;                        <q-btn round v-if="withEdit" color="primary" icon="las la-link">&ndash;&gt;-->
<!--&lt;!&ndash;                            <q-tooltip anchor="center right" self="center left" :offset="[5, 10]" class="bg-secondary">&ndash;&gt;-->
<!--&lt;!&ndash;                                Добавить ссылку&ndash;&gt;-->
<!--&lt;!&ndash;                            </q-tooltip>&ndash;&gt;-->
<!--&lt;!&ndash;                        </q-btn>&ndash;&gt;-->
<!--&lt;!&ndash;                    </div>&ndash;&gt;-->
<!--                </div>-->
<!--                <div class="col text-center">-->
<!--                 -->
<!--                </div>-->
<!--                <div class="col q-gutter-sm text-right">-->
<!--                 -->
<!--                    <div>-->
<!--                        <q-btn round v-if="withEdit" color="primary" icon="las la-link">-->
<!--                            <q-tooltip anchor="center right" self="center left" :offset="[5, 10]" class="bg-secondary">-->
<!--                                Изменить контакты-->
<!--                            </q-tooltip>-->
<!--                        </q-btn>-->
<!--                    </div>-->
<!--                </div>-->
<!--            </div>-->
        </q-card-section>
        
        <q-card-section class="q-pb-none">
            <div class="text-h6 text-center">{{ entry.name }}</div>
            <div v-if="entry.description" class="text-subtitle2 text-center">{{ entry.description }}</div>
            <div class="items-center flex justify-center q-gutter-x-sm">
                <small class="profile-card__caption">{{ entryMessages.reputation }}</small>
                <strong>{{ entry.reputation }}</strong>
                <q-icon name="star" size="1.5em"/>
            </div>
            <div class="text-center">
                <q-badge color="secondary">{{ entryMessages.entryType.names[entry.entryType] }}</q-badge>
            </div>
        </q-card-section>
        
        <card-contacts :entry-id="entry.id"></card-contacts>

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
</template>

<script setup lang="ts">
import CardContacts from './Entry.Card.Contacts.vue';
// import {useEntryProfileStore} from "../../../store/entry/entry.profile.store";
// import EntryFormContactsModal from './Entry.Form.Contacts.Modal.vue';
import {withDefaults, ref, watch} from "vue";
import {entryMessages, actualMessages} from "../../../localize/messages";
import {Entry} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";
import EntryFormModal from './Entry.Form.Modal.vue';
import {useEntryEditStore} from "../../../store/entry/entry.edit.store";
import {entryMappers} from "../../../api/api_mappers";
import {useRoute} from "vue-router";

const editStore = useEntryEditStore();
// const profileStore = useEntryProfileStore();

const isShowEditModal = ref(false);
// const entry = computed((): Entry | null => profileStore.entry);
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
const updateEntry = async () => {
    await editStore.updateEntry(route.params.entryId as string);
    editStore.$reset();
    isShowEditModal.value = false;

    emit('update-entry', props.entry.id);
}
</script>

<style lang="scss">
    .profile-card {
        &__edit {
            position: absolute;
            right: 10px;
            top: 10px;
        }
        &__caption {
            color: $grey-7;
            text-transform: uppercase;
        }
    }
</style>