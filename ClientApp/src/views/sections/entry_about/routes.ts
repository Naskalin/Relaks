import {RouteRecordRaw} from "vue-router";

export const aboutRoutes: RouteRecordRaw[] = [
    {
        path: '',
        name: 'entry-about',
        component: () => import('./EntryAbout.vue'),
        redirect: (to: any) => {
            return {name: 'entry-about-list', params: {entryId: to.params.entryId}}
        },
        children: [
            {path: '', name: 'entry-about-list', component: () => import('./EntryAbout.List.vue')},
            {path: 'new', name: 'entry-about-new', component: () => import('./EntryAbout.New.vue')},
            {path: ':entryInfoId/edit', name: 'entry-about-edit', component: () => import('./EntryAbout.Edit.vue')},
        ]
    },
]