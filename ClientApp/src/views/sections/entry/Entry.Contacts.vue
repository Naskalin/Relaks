<template>
    <template v-if="store.phones.length">
        <q-card-section class="q-gutter-x-sm text-center">
            <div v-for="eText in store.phones">
                <q-icon name="phone" color="secondary"/> {{ eText.val }}
<!--                <q-icon name="phone" color="secondary"/> {{ phoneHelper.toPhone(eText.val).number }}-->
            </div>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="store.emails.length">
        <q-card-section class="q-gutter-x-sm text-center">
            <div v-for="eText in store.emails">
                <q-icon name="email" color="secondary"/> {{ eText.val }}
            </div>
        </q-card-section>
        <q-separator></q-separator>
    </template>
    <template v-if="store.urls.length">
        <q-card-section class="q-gutter-x-sm text-center">
            <div v-for="eText in store.urls">
                <q-icon name="link" color="secondary"/> {{ eText.val }}
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
import {phoneHelper, Phone} from "../../../utils/phone_helper";

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