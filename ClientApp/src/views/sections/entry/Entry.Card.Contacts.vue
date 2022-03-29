<template>
    <template v-if="store.phones.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list bordered separator>
                <q-item v-for="eText in store.phones" :key="eText.id">
                    <q-item-section>
                        <q-item-label>
                            <phone :phone="eText.val"></phone>
                        </q-item-label>
                        <q-item-label v-if="eText.title" :lines="2" class="text-grey-9">{{ eText.title }}</q-item-label>
                        <actual-timestamp-tooltip :entry-text="eText">
                            <phone :phone="eText.val"></phone>

                            <q-separator></q-separator>

                            <div v-if="eText.title">
                                {{ eText.title }}
                                <q-separator></q-separator>
                            </div>
                        </actual-timestamp-tooltip>
                    </q-item-section>
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="store.emails.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list bordered separator>
                <q-item v-for="eText in store.emails" :key="eText.id">
                    <!--                <entry-text-email :entry-text="eText" :key="eText.id"></entry-text-email>-->
                    <q-item-section>
                        <q-item-label>
                            <email :email="eText.val" with-link></email>
                        </q-item-label>
                        <q-item-label v-if="eText.title" :lines="2" class="text-grey-9">{{ eText.title }}</q-item-label>
                        <actual-timestamp-tooltip :entry-text="eText">
                            <email :email="eText.val" icon-color="grey-5"></email>

                            <q-separator></q-separator>

                            <div v-if="eText.title">
                                {{ eText.title }}
                                <q-separator></q-separator>
                            </div>
                        </actual-timestamp-tooltip>
                    </q-item-section>
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="store.urls.length">
        <q-card-section class="q-gutter-y-sm">
            <q-list bordered separator>
                <q-item v-for="eText in store.urls" :key="eText.id">
                    <q-item-section>
                        <q-item-label>
                            <url :url="eText.val" with-link></url>
                        </q-item-label>
                        <q-item-label v-if="eText.title" :lines="2" class="text-grey-9">{{ eText.title }}</q-item-label>
                        <actual-timestamp-tooltip :entry-text="eText">
                            <url :url="eText.val" icon-color="grey-5"></url>

                            <q-separator></q-separator>

                            <div v-if="eText.title">
                                {{ eText.title }}
                                <q-separator></q-separator>
                            </div>
                        </actual-timestamp-tooltip>
                    </q-item-section>
                </q-item>
            </q-list>
        </q-card-section>
        <q-separator></q-separator>
    </template>
</template>

<script setup lang="ts">
import {watch} from "vue";
import {useEntryContactsStore} from "../../../store/entry_contacts/entry_cotacts_store";
import Phone from '../../components/Phone.vue';
import Email from '../../components/Email.vue';
import Url from '../../components/Url.vue';
import ActualTimestampTooltip from '../../components/Actual.Timestamp.Tooltip.vue';

const props = defineProps<{
    entryId: string
}>();
const store = useEntryContactsStore();

watch(() => props.entryId, async () => {
    await store.getAllContacts(props.entryId);
}, {immediate: true})
</script>