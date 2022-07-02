<template>
    <template v-if="eInfoListStore.phones(isShowDeleted).length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in eInfoListStore.phones(isShowDeleted)"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="eInfoListStore.emails(isShowDeleted).length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in eInfoListStore.emails(isShowDeleted)"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="eInfoListStore.urls(isShowDeleted).length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in eInfoListStore.urls(isShowDeleted)"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="eInfoListStore.dates(isShowDeleted).length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in eInfoListStore.dates(isShowDeleted)"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="eInfoListStore.notes(isShowDeleted).length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in eInfoListStore.notes(isShowDeleted)"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <entry-info-form-modal
        v-if="withEdit && editStore.model !== null"
        v-model="editStore.model"
        v-model:is-show="isShowEditModal"
        :entry-info-type="entryInfoType"
        :is-loading="editStore.isLoading"
        :is-create="false"
        :label="'Изменение: ' + entryInfoMessages.val.names[entryInfoType]"
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
import {useEntryInfoListStore} from "../../../store/entry_info/entryInfo.list.store";
import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';
import EntryInfoItem from '../entry_info/EntryInfo.Item.vue';
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useEntryInfoEditStore} from "../../../store/entry_info/entryInfo.edit.store";
import {entryInfoMessages} from "../../../localize/messages";
import {EntryInfoType} from "../../../api/api_types";

const props = withDefaults(defineProps<{
    entryId: string,
    withEdit?: boolean,
    withDeleted?: boolean,
    isShowDeleted: boolean | null
}>(), {
    withDeleted: false,
    isShowDeleted: false,
});

// show
const eInfoListStore = useEntryInfoListStore();
watch(() => props.entryId, async () => {
    await eInfoListStore.getAll(props.entryId);
}, {immediate: true})

// show edit form
const editStore = useEntryInfoEditStore();
const isShowEditModal = ref(false);
const currentEditId = ref('');
const entryInfoType = ref<EntryInfoType>('PHONE');
const showEditForm = (eInfo: any) => {
    if (props?.withEdit !== true) {
        return;
    }

    entryInfoType.value = eInfo.type;
    editStore.setModel(eInfo);
    isShowEditModal.value = true;
    currentEditId.value = eInfo.id;
}

// save edit
const saveEditForm = async () => {
    await editStore.update(props.entryId, currentEditId.value);
    await eInfoListStore.getAll(props.entryId);
    isShowEditModal.value = false;
    editStore.$reset();
}

// delete
const onDelete = async () => {
    await apiEntryInfo.delete(props.entryId, currentEditId.value);
    await eInfoListStore.getAll(props.entryId);
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