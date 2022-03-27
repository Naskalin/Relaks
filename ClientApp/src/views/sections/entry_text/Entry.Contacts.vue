<template>
    <div class="row">
        <div class="col">
            <q-card v-for="eText in store.emails" :key="eText.id">
                {{eText.val}}
            </q-card>
        </div>
        <div class="col"></div>
        <div class="col"></div>
    </div>
</template>

<script setup lang="ts">
import {useRoute} from 'vue-router';
import {useEntryContactsStore} from "../../../store/entry_contacts/entry_cotacts_store";

const route = useRoute();
const store = useEntryContactsStore();
</script>