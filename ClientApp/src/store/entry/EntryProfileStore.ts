// import {defineStore} from 'pinia';
// // import {jsonApi} from "../../api";
// // import {ApiGetRequest, ApiGetResponse} from "../../api/get";
//
// interface IEntryProfile {
//     entry: null | ApiGetResponse,
//     // tab: null | string,
// }
//
// export const useEntryProfileStore = defineStore('entryProfile', {
//     state: (): IEntryProfile => {
//         return {
//             entry: null,
//             // tab: null,
//         }
//     },
//     actions: {
//         async getEntry(entryId: string, apiRequest?: ApiGetRequest) {
//             const resp = await jsonApi.get({resource: 'entries', resourceId: entryId}, apiRequest);
//             this.entry = resp as ApiGetResponse;
//         }
//     }
// })