import {RouteRecordRaw} from "vue-router";

import EntryContacts from './Entry.Contacts.vue';

export const entryTextRoutes: RouteRecordRaw[] = [
    {component: EntryContacts, path: 'contacts', name: 'entry-contacts'}
]