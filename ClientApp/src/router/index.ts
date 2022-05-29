import { createRouter, createWebHistory, createWebHashHistory } from 'vue-router';

import {entryRoutes} from "../views/sections/entry/routes";
import {pageRoutes} from "../views/sections/pages/routes";
import Page from '../views/layouts/Page.vue';

const history = createWebHashHistory();
const routes = [
    {
        path: '/',
        component: Page,
        children: [
            ...pageRoutes,
            ...entryRoutes
        ]
    },
];

const router = createRouter({
    history,
    routes,
});

export default router;