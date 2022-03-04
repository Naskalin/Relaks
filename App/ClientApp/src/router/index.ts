import { createRouter, createWebHistory } from 'vue-router';
import Home from '../pages/Home.vue';
import About from '../pages/About.vue';
import ListEntry from '../pages/entry/ListEntry.vue';
import AddEntry from '../pages/entry/AddEntry.vue';

import Page from '../layouts/Page.vue';

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
            }
        ]
    },
];

const router = createRouter({
    history,
    routes,
});

export default router;