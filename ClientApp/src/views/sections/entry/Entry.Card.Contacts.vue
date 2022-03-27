<template>
    <template v-if="store.phones.length">
        <q-card-section class="text-center q-gutter-y-sm">
            <div v-for="eText in store.phones">
                <entry-text-phone :entry-text="eText" :key="eText.id"></entry-text-phone>
            </div>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="store.emails.length">
        <q-card-section class="text-center q-gutter-y-sm">
            <div v-for="eText in store.emails">
                <entry-text-email :entry-text="eText" :key="eText.id"></entry-text-email>
            </div>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="store.urls.length">
        <q-card-section class="text-center q-gutter-y-sm">
            <div v-for="eText in store.urls">
                <entry-text-url :entry-text="eText" :key="eText.id"></entry-text-url>
            </div>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <!--    <template v-if="emails !== ''">-->
    <!--        <q-card-section>{{emails}}</q-card-section>-->
    <!--        <q-separator></q-separator>-->
    <!--    </template>-->
    <!--    <template v-if="urls !== ''">-->
    <!--        <q-card-section>{{urls}}</q-card-section>-->
    <!--        <q-separator></q-separator>-->
    <!--    </template>-->
</template>

<script setup lang="ts">
import {onMounted, computed} from "vue";
import {useEntryContactsStore} from "../../../store/entry_contacts/entry_cotacts_store";
import EntryTextPhone from '../../sections/entry_text/EntryText.Phone.vue';
import EntryTextEmail from '../../sections/entry_text/EntryText.Email.vue';
import EntryTextUrl from '../../sections/entry_text/EntryText.Url.vue';
// import {phoneHelper, Phone} from "../../../utils/phone_helper";

const props = defineProps<{
    entryId: string
}>();
const store = useEntryContactsStore();
store.entryId = props.entryId;

onMounted(async () => {
    await store.getEmails();
    await store.getPhones();
    await store.getUrls();
})

// const phones = computed(() => {
//     return store.phones.map(eText => {
//         const phone = phoneHelper.toPhone(eText.val);
//         return phone.number
//     }).join(', ');
// })
// const emails = computed(() => {
//     return store.emails.map(eText => eText.val).join(', ');
// })
// const urls = computed(() => {
//     return store.urls.map(eText => eText.val).join(', ');
// })
</script>