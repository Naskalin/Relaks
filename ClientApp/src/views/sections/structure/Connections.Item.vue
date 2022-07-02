<template>
    <q-expansion-item
        :group="structureStore.structureSelectedId"
        expand-separator
        :model-value="connectionsStore.activeConnectionId === connection.id"
        @update:model-value="setActiveConnection"
        :icon="relationData.icon"
        :label="relationData.text"
        :class="connectionsStore.activeConnectionId === connection.id ?  connection.arrow +' active' : connection.arrow +' bg-blue-grey-2'"
        class="connection-item"
    >
        <template v-slot:header>
            <q-item-section side>
                <q-icon :name="relationData.icon" :size="connection.direction === 'Bidirectional' ? '1.5em' : '1.8em'"/>
            </q-item-section>
            <q-item-section>
                {{relationData.text}}
            </q-item-section>
            <q-item-section side v-if="connection.description">
                <q-icon name="las la-comment" v-tooltip="connection.description"/>
            </q-item-section>
            <q-item-section side v-if="connection.deletedAt">
                <q-avatar
                    icon="las la-archive" 
                    color="red-8"
                    text-color="white"
                    size="md"
                    v-tooltip="'В архиве с ' + dateHelper.utcFormat(connection.deletedAt, 'DD.MM.YYYY')"/>
            </q-item-section>
        </template>
        <q-card>
            <deleted :deleted="connection"/>
            <q-card-section>
                <div class="q-gutter-y-xs">
                    <div class="row justify-between q-col-gutter-sm items-center">
                        <div class="col">
                            <q-icon name="las la-calendar"/>
                            <small class="label-caption q-mx-sm">с</small>
                            <time>{{dateHelper.utcFormat(connection.startAt, 'DD.MM.YYYY')}}</time>
                        </div>
                        <div class="col-auto">
                            <div class="text-center">
                                <q-btn
                                    @click="showEditModal"
                                    round
                                    v-tooltip="'Изменить'"
                                    flat
                                    color="primary"
                                    icon="las la-edit"
                                    outline/>
                            </div>
                        </div>
                    </div>
                    <div v-if="connection.description">{{ connection.description }}</div>
                    
                    <div class="row">
                        <div class="col text-left">
                            <span class="label-caption text-grey-8">создано</span>
                            <br>
                            <span class="text-grey-7">{{ dateHelper.utcFormat(connection.createdAt) }}</span>
                        </div>
                        <div class="col text-right">
                            <span class="label-caption text-grey-8">обновлено</span>
                            <br>
                            <span class="text-grey-7">{{ dateHelper.utcFormat(connection.updatedAt) }}</span>
                        </div>
                    </div>
                </div>
            </q-card-section>
        </q-card>
    </q-expansion-item>
</template>

<script setup lang="ts">
import {useStructureConnectionsStore, StructureConnectionWithArrow} from "./structure_connections_store";
import {computed} from 'vue';
import {useStructureStore} from "./structure_store";
import {useStructureConnectionsFormStore} from "./structure_connections_form_store";
import {dateHelper} from "../../../utils/date_helper";
import Deleted from '../../components/Deleted.vue';

const props = defineProps<{
    connection: StructureConnectionWithArrow,
}>()
const formStore = useStructureConnectionsFormStore();
const connectionsStore = useStructureConnectionsStore();
const structureStore = useStructureStore();
const setActiveConnection = (val: boolean) => {
    if (val) {
        connectionsStore.activeConnectionId = props.connection.id;   
    } else if (connectionsStore.activeConnectionId === props.connection.id) {
        // Если кликают на уже активный элемент, то делаем его не активным
        connectionsStore.activeConnectionId = null;
    }
}
const showEditModal = () => {
    formStore.$reset();
    formStore.isShowEdit = true;
    formStore.editId = props.connection.id;
    formStore.request = Object.assign({}, props.connection);
}
declare type RelationObj = {
    text: string,
    icon: string
}
const relationData = computed(() => {
    if (structureStore.structureSelected && Object.keys(structureStore.structuresById).length) {
        const data: RelationObj = {text: '', icon: ''}
        let secondStructureId = props.connection.structureSecondId;
        if(props.connection.structureFirstId !== structureStore.structureSelected.id) {
            secondStructureId = props.connection.structureFirstId;
        }
        data.text = structureStore.structuresById[secondStructureId].title || '';
        switch (props.connection.arrow) {
            case "in":
                // str = isFirst ? '<<-' : '->>';
                data.icon = 'las la-sign-in-alt';
                break;
            case "out":
                // str = isFirst ? '->>' : '<<-';
                data.icon = 'las la-sign-out-alt';
                break;
            case "double":
                // str = '<<->>';
                data.icon = 'las la-arrows-alt-h';
                break;
        }
        
        return data;
    }
})
</script>

<style lang="scss">
.tooltip-inner {
    max-width: 300px !important;
}
.connection-item {
    &.active {
        color: #fff;
        &.in {
            background: $positive;
        }
        &.out {
            background: $indigo-6;
        }
        &.double {
            background: $deep-orange-7;
        }
    }
    .q-expansion-item__content {
        color: $dark;
    }
    //.q-icon.la-sign-out-alt,
    //.q-icon.la-sign-in-alt {
    //    font-size: 1.8em;
    //}
    .q-icon.la-sign-out-alt {
        color: $indigo-6;
    }
    .q-icon.la-sign-in-alt {
        color: $positive;
    }
    .q-icon.la-arrows-alt-h {
        color: $deep-orange-7;
        //color: #00695c;
    }
    &.active .q-item__section .q-icon {
        color: white
    }
}
</style>