import {RouteRecordRaw} from "vue-router";

import ListEntry from './Entry.List.vue';
import Profile from './Entry.Profile.vue';
import EntryFileList from '../entry_files/EntryFile.List.vue';
import EntryAbout from '../entry_about/EntryAbout.vue';

import Structures from '../structure/Structures.vue';

export const entryRoutes: RouteRecordRaw[] = [
    {
        path: '/entries', component: ListEntry, name: 'entry-list'
    },
    {
        path: '/entries/:entryId', component: Profile, name: 'entry-profile',
        redirect: (to: any) => {
            return {name: 'entry-about', params: {entryId: to.params.entryId}}
        },
        children: [
            {component: EntryAbout, path: '', name: 'entry-about'},
            {component: EntryFileList, path: 'files', name: 'entry-file-list'},
            {component: Structures, path: 'structures', name: 'entry-structures'},
        ]
    }
];
