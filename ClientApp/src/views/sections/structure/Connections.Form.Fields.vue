<template>
    <soft-delete-fields v-model="store.request"/>
    <structure-select-field label="Группа #1" v-model="store.request.structureFirstId"/>
    <div class="row items-center q-col-gutter-md q-my-sm">
        <div class="col-auto">
            <span class="label-caption">Направление</span>
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
    <date-field v-model="store.request.startAt" label="Существует с" :with-time="false" required/>
</template>

<script setup lang="ts">
import {useStructureConnectionsFormStore} from "./structure_connections_form_store";
import StructureSelectField from './fields/Structure.Select.Field.vue';
import DateField from '../../fields/Date.Field.vue';
import {StructureConnectionDirection} from "../../../api/rerources/api_structure_connections";
import SoftDeleteFields from '../../fields/SoftDeleteFields.vue';

const store = useStructureConnectionsFormStore();
const directionsOptions :Array<{value: StructureConnectionDirection, icon: string}> = [
    {value: 'Normal', icon: 'las la-arrow-right'},
    {value: 'Reverse', icon: 'las la-arrow-left'},
    {value: 'Bidirectional', icon: 'las la-arrows-alt-h'},
]
</script>