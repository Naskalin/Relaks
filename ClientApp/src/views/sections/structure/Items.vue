<template>
    <new-modal/>
    <edit-modal/>
    
    <div class="row q-col-gutter-md q-mb-md items-center justify-between">
        <div class="col-auto">
            <h6 class="q-ma-none">Объединения</h6>
        </div>
        <div class="col-auto q-gutter-sm">
            <q-btn @click="showNewModal" icon="las la-users" round color="primary" v-tooltip.left="'Добавить объединение'"/>
        </div>
    </div>
    <div v-if="itemsStore.items.length">
        <p style="font-size: .85rem" class="text-center">
            <q-icon name="las la-info-circle"/>
            Клик на аватар откроет предпросмотр.
        </p>
        <q-list bordered separator class="bg-my-grey">
            <structure-item-single
                v-for="sItem in itemsStore.items"
                :structure-item="sItem"
                :key="sItem.id"
                :id="sItem.id"
            />
        </q-list>
    </div>
    <p v-else class="text-blue-grey-8 text-center">Группа не содержит объединений</p>
</template>

<script setup lang="ts">
import NewModal from './Items.New.vue';
import EditModal from './Items.Edit.vue';
import StructureItemSingle from './Items.Single.vue';

import {useStructureItemsStore} from "./structure_items_store";
import {useStructureStore} from "./structure_store";
import {useStructureItemFormStore} from "./structure_items_form_store";
import {dateHelper} from "../../../utils/date_helper";

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

</script>