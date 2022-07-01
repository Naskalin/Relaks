<template>
    <new-form/>
    
    <div class="row q-col-gutter-md items-center q-my-lg justify-between">
        <div class="col-auto">
            <h6 class="q-ma-none">Объединения</h6>
        </div>
        <div class="col-auto q-gutter-sm">
            <q-btn @click="showNewForm" icon="las la-users" round color="primary" v-tooltip.left="'Добавить объединение'"/>
        </div>
    </div>

    <template v-if="itemsStore.items.length">
        <div v-for="sItem in itemsStore.items" :key="sItem.id">
            <q-item class="q-pa-none" :key="'item'+sItem.id">
                <q-item-section side>
                    <q-avatar size="50px" color="grey-5">
                        <api-image v-if="sItem.entry.avatar" :file-id="sItem.entry.avatar" image-filter="square-thumbnail"/>
                        <q-icon v-else name="las la-question-circle" color="grey-4" size="34px"/>
                    </q-avatar>
                </q-item-section>
                <q-item-section>
                    <q-item-label>{{sItem.entry.name}}</q-item-label>
                    <div v-if="sItem.entry.description" style="font-size: .85rem" class="text-grey-8">{{sItem.entry.description}}</div>
                    <div v-if="sItem.description" class="q-mt-sm">
                        <q-icon name="las la-comment"/> {{sItem.description}}
                    </div>
                </q-item-section>
                <q-item-section side>
                    <q-btn color="primary" icon="las la-edit" round flat/>
                </q-item-section>
            </q-item>

            <q-separator class="q-my-md"/>
        </div>
    </template>
    <p v-else class="text-blue-grey-8 text-center">Группа не содержит объединений</p>
</template>

<script setup lang="ts">
import {useStructureItemsStore} from "./structure_items_store";
import {useStructureStore} from "./structure_store";
import {watch} from "vue";
import ApiImage from '../../components/ApiImage.vue';
import {useStructureConnectionsStore} from "./structure_connections_store";
import {useStructureItemFormStore} from "./structure_items_form_store";
import {dateHelper} from "../../../utils/date_helper";
import NewForm from './Items.New.vue';

const itemsStore = useStructureItemsStore();
const structureStore = useStructureStore();
const connectionStore = useStructureConnectionsStore();
const formStore = useStructureItemFormStore();

const showNewForm = () => {
    if (!structureStore.structureSelectedId) return;
    formStore.$reset();
    formStore.request.startAt = dateHelper.startOfDayISO();
    formStore.request.structureId = structureStore.structureSelectedId;
    formStore.isShowCreate = true;
}
watch(() => structureStore.structureSelectedId, async (structureId: any, oldVal: any) => {
    if (!structureId) return;
    connectionStore.drawActiveStructureConnections(structureId, oldVal);
    await itemsStore.getItemsAsync(structureId);
}, {immediate: true})
</script>