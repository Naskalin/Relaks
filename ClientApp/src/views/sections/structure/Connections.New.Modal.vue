<template>
    <modal title="Добавление связи" v-model:is-show="formStore.isShowCreate">
        
    </modal>
</template>

<script setup lang="ts">
import Modal from '../../components/Modal.vue';
import {useStructureConnectionsFormStore} from "./structure_connections_form_store";
import {useStructureStore} from "./structure_store";

const formStore = useStructureConnectionsFormStore();

</script>