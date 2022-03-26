import {RouteRecordRaw} from "vue-router";

import EntryContacts from './Entry.Contacts.vue';

export const entryContactsRoutes: RouteRecordRaw[] = [
    {component: EntryContacts, path: 'contacts', name: 'entry-contacts'}
]