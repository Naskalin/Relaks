import {RouteRecordRaw} from "vue-router";

export const infoTemplateRoutes: RouteRecordRaw[] = [
    {
        path: '/info-templates',
        name: 'info-templates',
        components: {
            default: () => import('./Info.Templates.vue'),
            LeftSidebar: () => import ('./Info.Templates.LeftSidebar.vue'),
        },
        redirect: (to: any) => {
            return {name: 'info-templates-list'}
        },
        children: [
            {path: '', name: 'info-templates-list', component: () => import('./Info.Templates.list.vue')},
            {path: ':infoTemplateId/edit', name: 'info-templates-edit', component: () => import('./Info.Template.Edit.vue')},
            {path: 'new', name: 'info-templates-new', component: () => import('./Info.Template.New.vue')},
        ]
    },
]