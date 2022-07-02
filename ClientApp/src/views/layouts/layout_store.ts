import {defineStore} from 'pinia';

declare type LayoutStoreState = {
    search: string
    isRightSidebar: boolean,
    isLeftSidebar: boolean,
}
export const useLayoutStore = defineStore('LayoutStore', {
    state: (): LayoutStoreState => {
        return {
            search: '',
            isLeftSidebar: true,
            isRightSidebar: false,
        }
    },
    actions: {}
})