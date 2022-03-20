import {RouteRecordRaw} from "vue-router";

import Home from './Home.vue';
import About from './About.vue';
import Test from './Test.vue';

export const pageRoutes: RouteRecordRaw[] = [
    {component: Home, path: '', name: 'page-home'},
    {path: '/about', name: 'page-about', component: About},
    {path: '/test', name: 'page-test', component: Test},
]