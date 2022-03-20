import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/pages/Home.vue';
import About from '../views/pages/About.vue';
import ListEntry from '../views/pages/entry/ListEntry.vue';
// import CreateEntry from '../views/pages/entry/CreateEntry.vue';
import ProfileLayout from "../views/pages/entry/ProfileLayout.vue";

import Page from '../views/layouts/Page.vue';

import EntryNotes from '../views/pages/entry_notes/EntryNotes.vue';
import EntryFiles from '../views/pages/entry_files/EntryFiles.vue';


// const isServer = typeof window === 'undefined';
const history = createWebHistory();
const routes = [
    {
        path: '/',
        component: Page,
        children: [
            {
                path: '',
                name: 'Home',
                component: Home,
            },
            {
                path: '/about',
                name: 'About',
                component: About,
            },
            {
                path: '/entries',
                name: 'entries',
                component: ListEntry,
            },
            // {
            //     path: '/entries/create',
            //     name: 'entries-create',
            //     component: CreateEntry,
            // },
            {
                path: '/entries/:id',
                name: 'entries-profile',
                component: ProfileLayout,
                redirect: (to: any) => {
                    return {name: 'entry-notes', params: {id: to.params.id}}
                },
                children: [
                    {
                        name: 'entry-notes',
                        path: '',
                        component: EntryNotes,
                    },
                    {
                        name: 'entry-files',
                        path: 'files',
                        component: EntryFiles,
                    }
                ]
            }
        ]
    },
];

const router = createRouter({
    history,
    routes,
});

export default router;