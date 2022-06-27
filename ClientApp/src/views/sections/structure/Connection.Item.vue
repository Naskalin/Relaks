<template>
<!--    <q-card-->
<!--        @click="setActiveConnection"-->
<!--        :class="connectionsStore.activeConnectionId === connection.id ? 'bg-indigo-9 text-white' : 'bg-blue-grey-2'"-->
<!--    >-->
<!--        <q-card-section class="q-gutter-y-sm">-->
<!--            <div v-if="relationData">-->
<!--                <q-icon :name="relationData.icon" :size="connection.direction === 'Bidirectional' ? '1.6em' : '2em'" class="q-mr-xs"/>-->
<!--                {{relationData.text}}-->
<!--            </div>-->
<!--            <div v-if="connection.title" class="text-weight-bold">{{ connection.title }}</div>-->
<!--            <div v-if="connection.description" style="font-size: .85rem" class="text-italic">{{ connection.description }}</div>-->
<!--        </q-card-section>-->
<!--    </q-card>-->

    <q-expansion-item
        :group="structureStore.structureSelectedId"
        @click="setActiveConnection"
        expand-separator
        :icon="relationData.icon"
        :label="relationData.text"
        :class="connectionsStore.activeConnectionId === connection.id ?  connection.arrow +' active' : connection.arrow +' bg-blue-grey-2'"
        class="connection-item"
    >
        <q-card>
            <q-card-section v-if="connection.title || connection.description">
                <div v-if="connection.title" class="text-weight-bold">{{ connection.title }}</div>
                <div v-if="connection.description" style="font-size: .85rem" class="text-italic">{{ connection.description }}</div>
            </q-card-section>
        </q-card>
    </q-expansion-item>
<!--    <q-item-->
<!--        v-else-->
<!--        class="connection-item"-->
<!--        :class="connectionsStore.activeConnectionId === connection.id ? 'active' : 'bg-blue-grey-2'"-->
<!--        @click="setActiveConnection"-->
<!--        clickable-->
<!--    >-->
<!--        <q-item-section avatar>-->
<!--            <q-icon :name="relationData.icon" />-->
<!--        </q-item-section>-->
<!--        <q-item-section>-->
<!--            <q-item-label>{{relationData.text}}</q-item-label>-->
<!--        </q-item-section>-->
<!--    </q-item>-->
</template>

<script setup lang="ts">
import {useStructureConnectionsStore} from "./structure_connections_store";
import {StructureConnectionWithArrow} from "./structure_connections_store";
import {computed} from 'vue';
import {useStructureStore} from "./structure_store";

const props = defineProps<{
    connection: StructureConnectionWithArrow,
}>()
const connectionsStore = useStructureConnectionsStore();
const structureStore = useStructureStore();
const setActiveConnection = () => {
    if (connectionsStore.activeConnectionId === props.connection.id) {
        // Если кликают на уже активный элемент, то делаем его не активным
        connectionsStore.activeConnectionId = null;
        return;
    }
    connectionsStore.activeConnectionId = props.connection.id;
}
declare type RelationObj = {
    text: string,
    icon: string
}
const connectionTooltip = computed(() => {
    
})
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
    .q-icon.la-sign-out-alt,
    .q-icon.la-sign-in-alt {
        font-size: 1.8em;
    }
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
    &.active .q-icon {
        color: white
    }
}
</style>