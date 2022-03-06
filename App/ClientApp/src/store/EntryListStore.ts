import { defineStore } from 'pinia';
// import JsonApi from 'devour-client';
//
// const jsonApi = new JsonApi({apiUrl:'https://localhost:7125/api'});
// jsonApi.define('persons', {
//     title: '',
//     content: '',
//     tags: []
// })

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