<template>
    <q-card v-bind="$attrs">
        <deleted :deleted="entry"></deleted>

        <q-card-section class="q-pb-none text-center">
            <q-avatar @dblclick="emit('card-dblclick', entry)"
                      @click="isShowAvatarSelect = true"
                      :class="{'cursor-pointer': withEdit === true}"
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

        <entry-card-infos 
            :entry-id="entry.id"
            :with-edit="withEdit"
            @click-edit-entry-btn="editStore.isShowEditModal = true"
        />

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
        label="Изменение объединения"
        btn-title="Сохранить"
        btn-icon="las la-save"
        :is-loading="editStore.isLoading"
        :is-create="false"
        v-model="editStore.model"
        v-model:is-show="editStore.isShowEditModal"
        @submit="updateEntry"
    />

    <file-select-modal
        :entry-id="entry.id"
        label="Выбор аватара"
        v-model:is-show="isShowAvatarSelect"
        v-if="withEdit"
        @fileSelect="onFileSelect"
    />
</template>

<script setup lang="ts">
import EntryCardInfos from './Entry.Card.Infos.vue';
import EntryFormModal from './Entry.Form.Modal.vue';
import FileSelectModal from '../../fields/File.Select.Modal.vue';
import Deleted from '../../components/Deleted.vue';
import ApiImage from '../../components/ApiImage.vue';
import {withDefaults, ref, watch, onMounted} from "vue";
import {entryMessages} from "../../../localize/messages";
import {Entry, FileModel} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";
import {useEntryEditStore} from "../../../store/entry/entry.edit.store";
import {apiMappers} from "../../../api/api_mappers";

const editStore = useEntryEditStore();
// const isShowEditModal = ref(false);
const isShowAvatarSelect = ref(false);

const props = withDefaults(defineProps<{
    entry: Entry,
    withEdit?: boolean,
}>(), {
    withEdit: false
})

if (props.withEdit) {
    watch([
        () => editStore.isShowEditModal,
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
    editStore.isShowEditModal = false;

    emit('update-entry', props.entry.id);
}
// avatar select
const onFileSelect = async (file: FileModel) => {
    if (!editStore.model) throw new Error('onFileSelect => EditStore Entry model is null');
    editStore.model.avatar = file.id;
    isShowAvatarSelect.value = false;
    await updateEntry();
    // await getAvatar(file.id);
}

onMounted(() => {
    editStore.$reset();
})

</script>