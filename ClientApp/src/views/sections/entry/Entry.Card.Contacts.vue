<template>
    <template v-if="listStore.Phone.list.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list bordered separator>
                <q-item v-for="eText in listStore.Phone.list" :key="eText.id">
                    <q-item-section>
                        <q-item-label><phone :phone="eText"></phone></q-item-label>
                        <q-item-label v-if="eText.title" :lines="2" class="text-grey-9">{{ eText.title }}</q-item-label>
                        <actual-timestamp-tooltip :entry-text="eText">
                            <phone :phone="eText.val"></phone>

                            <q-separator></q-separator>

                            <div v-if="eText.title">
                                {{ eText.title }}
                                <q-separator></q-separator>
                            </div>
                        </actual-timestamp-tooltip>
                    </q-item-section>
<!--                    <q-item-section side v-if="withEdit">-->
<!--                        <q-btn icon="las la-edit" @click="showEditForm(eText)" flat round color="primary"/>-->
<!--                    </q-item-section>-->
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="listStore.Email.list.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list bordered separator>
                <q-item v-for="eText in listStore.Email.list" :key="eText.id">
                    <q-item-section>
                        <q-item-label><email :email="eText.email" with-link></email></q-item-label>
                        <q-item-label v-if="eText.title" :lines="2" class="text-grey-9">{{ eText.title }}</q-item-label>
                        <actual-timestamp-tooltip :entry-text="eText">
                            <email :email="eText.email" icon-color="grey-5"></email>

                            <q-separator></q-separator>

                            <div v-if="eText.title">
                                {{ eText.title }}
                                <q-separator></q-separator>
                            </div>
                        </actual-timestamp-tooltip>
                    </q-item-section>
<!--                    <q-item-section side v-if="withEdit">-->
<!--                        <q-btn icon="las la-edit" @click="showEditForm(eText)" flat round color="primary"/>-->
<!--                    </q-item-section>-->
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="listStore.Url.list.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list bordered separator>
                <q-item v-for="eText in listStore.Url.list" :key="eText.id">
                    <q-item-section>
                        <q-item-label><url :url="eText.url" with-link></url></q-item-label>
                        <q-item-label v-if="eText.title" :lines="2" class="text-grey-9">{{ eText.title }}</q-item-label>
                        <actual-timestamp-tooltip :entry-text="eText">
                            <url :url="eText.url" icon-color="grey-5"></url>

                            <q-separator></q-separator>

                            <div v-if="eText.title">
                                {{ eText.title }}
                                <q-separator></q-separator>
                            </div>
                        </actual-timestamp-tooltip>
                    </q-item-section>
<!--                    <q-item-section side v-if="withEdit">-->
<!--                        <q-btn icon="las la-edit" @click="showEditForm(eText)" flat round color="primary"/>-->
<!--                    </q-item-section>-->
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    
<!--    <entry-text-form-modal v-if="withEdit && editStore.model"-->
<!--                           v-model="editStore.model"-->
<!--                           v-model:is-show="isShowEditModal"-->
<!--                           :is-loading="editStore.isLoading"-->
<!--                           :is-create="false"-->
<!--                           :title="'Изменение: ' + entryInfoMessages.val.names[editStore.model.textType]"-->
<!--                           btn-title="Сохранить"-->
<!--                           btn-icon="las la-save"-->
<!--                           @submit="saveEditForm"-->
<!--                           @delete="onDelete"-->
<!--    >-->
<!--        -->
<!--    </entry-text-form-modal>-->
</template>

<script setup lang="ts">
import {watch, ref} from "vue";
import {useEntryInfoListStore} from "../../../store/entry_info/entryInfo.list.store";
import Phone from '../../components/Phone.vue';
import Email from '../../components/Email.vue';
import Url from '../../components/Url.vue';
import ActualTimestampTooltip from '../../components/Actual.Timestamp.Tooltip.vue';
import EntryTextFormModal from '../entry_text/EntryText.Form.Modal.vue';
// import {useEntryTextEditStore} from "../../../store/entry_text/entryText.edit.store";
// import {EntryText} from "../../../api/api_types";
// import {apiMappers} from "../../../api/api_mappers";
// import {apiEntryText} from "../../../api/rerources/api_entry_text";
import {useRouter} from 'vue-router';
import {entryInfoMessages} from "../../../localize/messages";

const props = defineProps<{
    entryId: string,
    withEdit?: boolean
}>();

// show
const listStore = useEntryInfoListStore();
watch(() => props.entryId, async () => {
    await listStore.getAllContacts(props.entryId);
}, {immediate: true})

// show-edit
// const router = useRouter();
// const editStore = useEntryTextEditStore();
// const isShowEditModal = ref(false);
// const currentEditId = ref('');
// const showEditForm = (eText: EntryText) => {
//     if (props?.withEdit !== true) {
//         return;
//     } 
//    
//     editStore.model = apiMappers.toUpdateEntryTextRequest(eText);
//     isShowEditModal.value = true;
//     currentEditId.value = eText.id;
// }

// save-edit
// const saveEditForm = async () => {
//     await editStore.update(props.entryId, currentEditId.value);
//     await store.getAllContacts(props.entryId);
//     isShowEditModal.value = false;
//     editStore.$reset();
// }

// delete
// const onDelete = async () => {
//     await apiEntryText.delete(props.entryId, currentEditId.value);
//     await store.getAllContacts(props.entryId);
//     isShowEditModal.value = false;
//     editStore.$reset();
// }
</script>