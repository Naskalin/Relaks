import {RouteRecordRaw} from "vue-router";

import ListEntry from './Entry.List.vue';
import Profile from './Entry.Profile.vue';

import {entryNoteRoutes} from "../entry_note/routes";

export const entryRoutes: RouteRecordRaw[] = [
    {path: '/entries', component: ListEntry, name: 'entry-list'},
    {
        path: '/entries/:entryId', component: Profile, name: 'entry-profile',
        redirect: (to: any) => {
            return {name: 'entry-note-list', params: {entryId: to.params.entryId}}
        },
        children: [
            ...entryNoteRoutes,
        ]
    }
];

// {
//     path: '/entries',
//         name: 'entries',
//     component: ListEntry,
// },
// // {
// //     path: '/entries/create',
// //     name: 'entries-create',
// //     component: CreateEntry,
// // },
// {
//     path: '/entries/:id',
//         name: 'entries-profile',
//     component: ProfileLayout,
//     redirect: (to: any) => {
//     return {name: 'entry-notes', params: {id: to.params.id}}
// },
//     children: [
//     {
//         name: 'entry-notes',
//         path: '',
//         component: EntryNotes,
//     },
//     {
//         name: 'entry-files',
//         path: 'files',
//         component: EntryFiles,
//     }
// ]
// }