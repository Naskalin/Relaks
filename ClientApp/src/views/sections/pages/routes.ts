import {RouteRecordRaw} from "vue-router";

import Home from './Home.vue';
import HomeSidebar from './HomeSidebar.vue';
import About from './About.vue';
import Test from './Test.vue';

export const pageRoutes: RouteRecordRaw[] = [
    {
        path: '', name: 'page-home', components: {
            default: Home,
            LeftSidebar: HomeSidebar,
        }
    },
    {path: '/about', name: 'page-about', component: About},
    {path: '/test', name: 'page-test', component: Test},
]