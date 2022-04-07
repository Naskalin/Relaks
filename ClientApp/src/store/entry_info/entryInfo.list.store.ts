// import {defineStore} from 'pinia';
// import {apiEntryInfo} from "../../api/rerources/api_entry_info";
// import {
//     EntryEmail,
//     EntryDate,
//     EntryNote,
//     EntryPhone,
//     EntryUrl,
//     EntryInfoType,
// } from "../../api/api_types";
// import {ApiListRequest} from "../../api";
//
// declare type EntryInfoListStoreState = {
//     Email: {
//         list: EntryEmail[]
//         isLoading: boolean,
//         request: ApiListRequest
//     },
//     Date: {
//         list: EntryDate[]
//         isLoading: boolean
//         request: ApiListRequest
//     },
//     Phone: {
//         list: EntryPhone[]
//         isLoading: boolean
//         request: ApiListRequest
//     },
//     Note: {
//         list: EntryNote[]
//         isLoading: boolean
//         request: ApiListRequest
//     },
//     Url: {
//         list: EntryUrl[]
//         isLoading: boolean
//         request: ApiListRequest
//     }
// }
// export const useEntryInfoListStore = defineStore('EntryInfoListStore', {
//     state: (): EntryInfoListStoreState => {
//         return {
//             Email: {
//                 list: [],
//                 isLoading: false,
//                 request: {}
//             },
//             Date: {
//                 list: [],
//                 isLoading: false,
//                 request: {}
//             },
//             Phone: {
//                 list: [],
//                 isLoading: false,
//                 request: {}
//             },
//             Url: {
//                 list: [],
//                 isLoading: false,
//                 request: {}
//             },
//             Note: {
//                 list: [],
//                 isLoading: false,
//                 request: {page: 1, perPage: 50, search: '', isDeleted: false}
//             },
//         }
//     },
//     getters: {
//         // activeList: (state) => {
//         //     return (eInfoType: EntryInfoType) => state[eInfoType].list.filter((eInfo: SoftDeletableType) => !eInfo.deletedAt);
//         // }
//         filterNotes: (state) => {return (withDeleted: false) => state.Note.list.filter(eInfo => withDeleted ? eInfo : !eInfo.deletedAt)},
//         filterDates: (state) => {return (withDeleted: false) => state.Date.list.filter(eInfo => withDeleted ? eInfo : !eInfo.deletedAt)},
//         filterPhones: (state) => {return (withDeleted: false) => state.Phone.list.filter(eInfo => withDeleted ? eInfo : !eInfo.deletedAt)},
//         filterUrls: (state) => {return (withDeleted: false) => state.Url.list.filter(eInfo => withDeleted ? eInfo : !eInfo.deletedAt)},
//         filterEmails: (state) => {return (withDeleted: false) => state.Email.list.filter(eInfo => withDeleted ? eInfo : !eInfo.deletedAt)},
//     },
//     actions: {
//         async getAllContacts(entryId: string) {
//             await this.getPhones(entryId);
//             await this.getEmails(entryId);
//             await this.getDates(entryId);
//             await this.getUrls(entryId);
//         },
//         async getBase(entryId: string, entryInfoType: EntryInfoType) {
//             if (this[entryInfoType].isLoading) {
//                 return;
//             }
//             this[entryInfoType].isLoading = true;
//
//             try {
//                 this[entryInfoType].list = await apiEntryInfo[entryInfoType].list(entryId, this[entryInfoType].request);
//             } finally {
//                 this[entryInfoType].isLoading = false;
//             }
//         },
//         async getPhones(entryId: string) {
//             await this.getBase(entryId, 'Phone');
//         },
//         async getUrls(entryId: string) {
//             await this.getBase(entryId, 'Url');
//         },
//         async getEmails(entryId: string) {
//             await this.getBase(entryId, 'Email');
//         },
//         async getNotes(entryId: string) {
//             await this.getBase(entryId, 'Note');
//         },
//         async getDates(entryId: string) {
//             await this.getBase(entryId, 'Date');
//         }
//     }
// })