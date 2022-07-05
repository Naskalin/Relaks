import {RouteRecordRaw} from "vue-router";
import InfoTemplates from './Info.Templates.vue';
import InfoTemplatesList from './Info.Templates.list.vue';
import InfoTemplateEdit from './Info.Template.Edit.vue';
import InfoTemplatesLeftSidebar from './Info.Templates.LeftSidebar.vue';

export const infoTemplateRoutes: RouteRecordRaw[] = [
    {
        path: '/info-templates',
        name: 'info-templates',
        components: {
            default: InfoTemplates,
            LeftSidebar: InfoTemplatesLeftSidebar
        },
        redirect: (to: any) => {
            return {name: 'info-templates-list'}
        },
        children: [
            {path: '', name: 'info-templates-list', component: InfoTemplatesList},
            {path: ':infoTemplateId/edit', name: 'info-templates-edit', component: InfoTemplateEdit}
        ]
    },
]