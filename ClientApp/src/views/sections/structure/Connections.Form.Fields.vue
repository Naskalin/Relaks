<template>
    <structure-select-field label="Группа #1" v-model="store.request.structureFirstId"/>
    <div class="row items-center q-col-gutter-md q-my-sm">
        <div class="col-auto">
            Направление связи
        </div>
        <div class="col">
            <q-btn-toggle
                v-model="store.request.direction"
                toggle-color="secondary"
                spread
                :options="directionsOptions"/>
        </div>
    </div>
    <structure-select-field label="Группа #2" v-model="store.request.structureSecondId"/>
    <q-input v-model="store.request.description" type="textarea" autogrow label="Описание" counter maxlength="500"/>
</template>

<script setup lang="ts">
import {useStructureConnectionsFormStore} from "./structure_connections_form_store";
import StructureSelectField from './fields/Structure.Select.Field.vue';
import {StructureConnectionDirection} from "../../../api/rerources/api_structure_connections";

const store = useStructureConnectionsFormStore();
const directionsOptions :Array<{label: string, value: StructureConnectionDirection, icon: string}> = [
    {label: 'Прямая', value: 'Normal', icon: 'las la-arrow-right'},
    {label: 'Обратная', value: 'Reverse', icon: 'las la-arrow-left'},
    {label: 'Двойная', value: 'Bidirectional', icon: 'las la-arrows-alt-h'},
]
</script>