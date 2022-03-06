import { defineStore } from 'pinia';

export declare interface PersonCreateDto {
    name: string,
    rating: number
}

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