<template>
    <new-modal/>
    <edit-modal/>
    
    <div class="row q-col-gutter-md items-center q-my-lg justify-between">
        <div class="col-auto">
            <h6 class="q-ma-none">Объединения</h6>
        </div>
        <div class="col-auto q-gutter-sm">
            <q-btn @click="showNewModal" icon="las la-users" round color="primary" v-tooltip.left="'Добавить объединение'"/>
        </div>
    </div>
    
    <q-list v-if="itemsStore.items.length" bordered separator>
        <entry-item v-for="sItem in itemsStore.items" :entry="sItem.entry" :key="sItem.id">
            <template v-slot:body>
                <div v-if="sItem.description" class="q-mt-xs">
                    <q-icon name="las la-comment"/> {{sItem.description}}
                </div>
                <div class="q-mt-xs text-grey-7">
                    <q-icon name="las la-calendar"/>
                    <small class="label-caption q-mx-sm">с</small>
                    <time>{{dateHelper.utcFormat(sItem.startAt, 'DD.MM.YYYY')}}</time>
                </div>
            </template>
            <template v-slot:end>
                <q-item-section side>
                    <q-btn color="primary" @click="showEditModal(sItem)" icon="las la-edit" round flat/>
                </q-item-section>
            </template>
        </entry-item>
    </q-list>
    <p v-else class="text-blue-grey-8 text-center">Группа не содержит объединений</p>
</template>

<script setup lang="ts">
import NewModal from './Items.New.vue';
import EditModal from './Items.Edit.vue';
import EntryItem from '../../components/Entry.Item.vue';
import {useStructureItemsStore} from "./structure_items_store";
import {useStructureStore} from "./structure_store";
import {useStructureItemFormStore} from "./structure_items_form_store";
import {dateHelper} from "../../../utils/date_helper";
import {StructureItem} from "../../../api/rerources/api_structure_items";

const itemsStore = useStructureItemsStore();
const structureStore = useStructureStore();
const formStore = useStructureItemFormStore();

const showNewModal = () => {
    if (!structureStore.structureSelectedId) return;
    
    formStore.$reset();
    formStore.request.startAt = dateHelper.startOfDayISO();
    formStore.request.structureId = structureStore.structureSelectedId;
    formStore.isShowCreate = true;
}
const showEditModal = (sItem: StructureItem) => {
    formStore.$reset();
    formStore.editId = sItem.id;
    formStore.request = Object.assign({}, sItem);
    formStore.isShowEdit = true;
}
</script>