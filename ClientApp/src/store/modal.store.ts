import {defineStore} from 'pinia';

declare type ModalStoreState = {
    isShow: boolean
}
export const useModalStore = defineStore('ModalStore', {
    state: (): ModalStoreState => {
        return {
            isShow: false,
        }
    },
})