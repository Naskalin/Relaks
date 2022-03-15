import {defineStore} from 'pinia';

declare type TestStoreState = {
    data: string,
    dateField: string,
}
export const useTestStore = defineStore('TestStore', {
    state: (): TestStoreState => {
        return {
            data: 'Test value 123',
            dateField: '',
        }
    },
    actions: {}
})