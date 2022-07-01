import {defineStore} from 'pinia';

declare type WithSidebarStoreState = {
    search: string
}
export const useWithSidebarStore = defineStore('WithSidebarStore', {
    state: (): WithSidebarStoreState => {
        return {
            search: ''
        }
    },
    actions: {}
})