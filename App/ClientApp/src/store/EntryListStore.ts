import { defineStore } from 'pinia';
import {ApiPerson} from "../types/api";

export declare interface PersonCreateDto {
    name: string,
    rating: number
}

export const useEntryStore = defineStore('entries', {
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