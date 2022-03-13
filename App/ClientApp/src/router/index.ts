import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/pages/Home.vue';
import About from '../views/pages/About.vue';
import ListEntry from '../views/pages/entry/ListEntry.vue';
import AddEntry from '../views/pages/entry/AddEntry.vue';
import ProfileEntry from "../views/pages/entry/ProfileEntry.vue";

import Page from '../views/layouts/Page.vue';

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
            {
                path: '/entries/add',
                name: 'entries-add',
                component: AddEntry,
            },
            {
                path: '/entries/:id',
                name: 'entries-profile',
                component: ProfileEntry,
            }
        ]
    },
];

const router = createRouter({
    history,
    routes,
});

export default router;