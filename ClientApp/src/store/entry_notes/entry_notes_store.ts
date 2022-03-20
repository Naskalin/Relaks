// import {defineStore} from "pinia";
// import {ApiGetAllRequest, ApiGetAllResponse, ApiGetRequest, ApiGetResponse} from "../../api/get";
// import {jsonApi} from "../../api";
//
// declare type EntryNotesStoreState = {
//     notes: null | ApiGetAllResponse,
//     notesIsLoading: boolean,
//     note: null | ApiGetResponse
// }
//
// export const useEntryNotesStore = defineStore('EntryNotesStore', {
//     state: () : EntryNotesStoreState => {
//         return {
//             notes: null,
//             notesIsLoading: false,
//             note: null,
//         }
//     },
//     actions: {
//         async getNotes(entryId: string, apiRequest?: ApiGetAllRequest) {
//             if (this.notesIsLoading) {
//                 return;
//             }
//             this.notesIsLoading = true;
//
//             try {
//                 await jsonApi.getAll({resource: 'entries', resourceId: entryId, subResource: 'infos'}, {
//                     pagination: {
//                         number: 1,
//                         size: 50,
//                     }
//                 });
//             } catch (e) {
//                 // log with axios interceptors
//             } finally {
//                 this.notesIsLoading = false;
//             }
//         },
//         async getNote(noteId: string, apiRequest?: ApiGetRequest) {
//             const resp = await jsonApi.get({resource: 'notes', resourceId: noteId}, apiRequest);
//             this.note = resp as ApiGetResponse;
//         }
//     }
// })