import {RouteRecordRaw} from "vue-router";

import ListEntry from './Entry.List.vue';
import Profile from './Entry.Profile.vue';
import ProfileSidebar from './Entry.Profile.Sidebar.vue';
import EntryFileList from '../entry_files/EntryFile.List.vue';
import EntryAbout from '../entry_about/EntryAbout.vue';

import {aboutRoutes} from "../entry_about/routes";

import Structures from '../structure/Structures.vue';
import ProfileRightSidebar from '../../sections/Profile.Right.Sidebar.vue';

export const entryRoutes: RouteRecordRaw[] = [
    {path: '/entries', component: ListEntry, name: 'entry-list'},
    {
        path: '/entries/:entryId', name: 'entry-profile', components: {
            default: Profile,
            LeftSidebar: ProfileSidebar,
            RightSidebar: ProfileRightSidebar,
        },
        redirect: (to: any) => {
            return {name: 'entry-about', params: {entryId: to.params.entryId}}
        },
        children: [
            ...aboutRoutes,
            {component: EntryFileList, path: 'files', name: 'entry-file-list'},
            {
                path: 'structures', name: 'entry-structures', components: {default: Structures}
            },
        ]
    }
];
