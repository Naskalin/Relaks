import { defineStore } from 'pinia';

export const useEntryListStore = defineStore('entryList', {
    state: () => {
        return {
            persons: {}
        }
    },
    actions: {
        getPersons: async () => {
            
        }
    }
})