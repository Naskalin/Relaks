import {defineStore} from 'pinia';

declare type LayoutStoreState = {
    search: string
    isBlockLeaving: boolean,
    isRightSidebar: boolean,
    isLeftSidebar: boolean,
}
export const useLayoutStore = defineStore('LayoutStore', {
    state: (): LayoutStoreState => {
        return {
            search: '',
            isBlockLeaving: false,
            isLeftSidebar: true,
            isRightSidebar: false,
        }
    },
    getters: {
        isBlockLeavingMessage: (state) => state.isBlockLeaving 
            ? 'Поиск и навигация недоступны. Завершите явно добавление/изменение данных.'
            : ''
    },
    actions: {
        
    }
})