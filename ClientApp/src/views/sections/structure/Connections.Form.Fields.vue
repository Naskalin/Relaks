<template>
    <q-input v-model="formStore.request.title" type="text" label="Название" counter maxlength="250" required/>
    <q-input v-model="formStore.request.title" type="text" label="Название" counter maxlength="250" required/>

<!--    public string Title { get; set; } = null!;-->
<!--    public string Description { get; set; } = null!;-->
<!--    public StructureConnection.DirectionEnum Direction { get; set; }-->
<!--    public Guid StructureFirstId { get; set; }-->
<!--    public Guid StructureSecondId { get; set; }-->
</template>

<script setup lang="ts">
import {useStructureConnectionsFormStore} from "./structure_connections_form_store";

const formStore = useStructureConnectionsFormStore();
</script>