<template>
    <q-card-section v-if="withEdit" class="q-gutter-x-sm text-center flex justify-center">
        <q-btn round @click="emit('clickEditEntryBtn')" v-tooltip="'Изменить объединение'" outline color="primary" icon="las la-edit"/>
        <q-separator vertical color="grey-5"/>
        <q-btn round @click="showCreateEntryInfoModal('PHONE')" v-tooltip="'Добавить телефон'" color="primary" icon="las la-phone"/>
        <q-btn round @click="showCreateEntryInfoModal('EMAIL')" v-tooltip="'Добавить e-mail'" color="primary" icon="las la-envelope"/>
        <q-btn round @click="showCreateEntryInfoModal('URL')" v-tooltip="'Добавить ссылку'" color="primary" icon="las la-link"/>
        <q-btn round @click="showCreateEntryInfoModal('DATE')" v-tooltip="'Добавить дату'" color="primary" icon="las la-calendar"/>
        <q-btn round @click="showCreateEntryInfoModal('NOTE')" v-tooltip="'Добавить заметку'" color="primary" icon="las la-file-alt"/>
    </q-card-section>

    <q-card-section class="text-center">
        <q-btn-toggle
            v-model="isShowDeleted"
            toggle-color="secondary"
            size="sm"
            :options="[
          {label: 'Вся информация', value: null},
          {label: 'Актуальная', value: false},
          {label: 'Архивная', value: true}
        ]"
        />
    </q-card-section>
    
    <template v-if="phones.length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in phones"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="emails.length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in emails"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="urls.length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in urls"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="dates.length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in dates"
                    :e-info="eInfo"
                    :key="eInfo.id"/>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="notes.length">
        <q-card-section class="q-py-none">
            <q-list separator>
                <entry-info-item
                    @showEditForm="showEditForm(eInfo)"
                    :with-edit="withEdit"
                    v-for="eInfo in notes"
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

    <entry-info-form-modal
        v-if="withEdit"
        :label="'Добавление: ' + entryInfoMessages.val.names[entryInfoFormType]"
        :is-create="true"
        :entry-info-type="entryInfoFormType"
        :is-loading="entryInfoCreateStore.isLoading"
        v-model:is-show="isShowCreateModal"
        v-model="entryInfoCreateStore.request"
        btn-title="Добавить"
        @submit="createEntryInfo"
    />
</template>

<script setup lang="ts">
import {watch, ref, computed} from "vue";
import EntryInfoFormModal from '../entry_info/EntryInfo.Form.Modal.vue';
import EntryInfoItem from '../entry_info/EntryInfo.Item.vue';
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useEntryInfoEditStore} from "../../../store/entry_info/entryInfo.edit.store";
import {entryInfoMessages} from "../../../localize/messages";
import {EntryInfo, EntryInfoType} from "../../../api/api_types";
import {filterByType} from "../../../utils/entry_info_helper";
import {useEntryInfoCreateStore} from "../../../store/entry_info/entryInfo.create.store";

const props = defineProps<{
    entryId: string,
    withEdit?: boolean,
}>()
const isShowDeleted = ref<null | boolean>(false);
const emit = defineEmits<{
    (e: 'clickEditEntryBtn'): void
}>()
const isShowCreateModal = ref(false);
const isListLoading = ref(false);
const allInfos = ref<EntryInfo[]>([])
const getAllAsync = async () => {
    if (isListLoading.value) {
        return;
    }
    isListLoading.value = true;

    try {
        allInfos.value = await apiEntryInfo.list(props.entryId, {
            type: ['NOTE', 'PHONE', 'URL', 'EMAIL', 'DATE']
        });
    } finally {
        isListLoading.value = false;
    }
};
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
    await getAllAsync();
    isShowEditModal.value = false;
    editStore.$reset();
}
// delete
const onDelete = async () => {
    await apiEntryInfo.delete(props.entryId, currentEditId.value);
    const indexOf = allInfos.value.findIndex(x => x.id === currentEditId.value);
    if (indexOf > -1) allInfos.value.splice(indexOf, 1);
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

const notes = computed(() => filterByType(allInfos.value, 'NOTE', isShowDeleted.value));
const emails = computed(() => filterByType(allInfos.value, 'EMAIL', isShowDeleted.value));
const urls = computed(() => filterByType(allInfos.value, 'URL', isShowDeleted.value));
const phones = computed(() => filterByType(allInfos.value, 'PHONE', isShowDeleted.value));
const dates = computed(() => filterByType(allInfos.value, 'DATE', isShowDeleted.value));

watch(() => props.entryId, async () => {
    await getAllAsync();
}, {immediate: true})

const entryInfoCreateStore = useEntryInfoCreateStore();
const entryInfoFormType = ref<EntryInfoType>('PHONE');
const showCreateEntryInfoModal = (type: EntryInfoType) => {
    if (props.withEdit) {
        entryInfoCreateStore.$reset();
        
        entryInfoFormType.value = type;
        isShowCreateModal.value = true

        entryInfoCreateStore.request.type = type;
        entryInfoCreateStore.resetRequestInfo(type);
    }
}

const createEntryInfo = async () => {
    // Создаём entryInfo
    const entryInfo = await entryInfoCreateStore.create(props.entryId);
    allInfos.value.unshift(entryInfo);
    isShowCreateModal.value = false;
    entryInfoCreateStore.$reset();
}

</script>