import { createRouter, createWebHashHistory } from 'vue-router';

import {entryRoutes} from "../views/sections/entry/routes";
import {pageRoutes} from "../views/sections/pages/routes";
import {infoTemplateRoutes} from "../views/sections/info_templates/routes";
import Layout from '../views/layouts/Layout.vue';

const history = createWebHashHistory();
const routes = [
    {
        path: '/',
        component: Layout,
        children: [
            ...pageRoutes,
            ...entryRoutes,
            ...infoTemplateRoutes
        ]
    },
];

const router = createRouter({
    history,
    routes,
});

export default router;