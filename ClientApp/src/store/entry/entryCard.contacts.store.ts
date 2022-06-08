// import {defineStore} from 'pinia';
// import {apiEntryInfo} from "../../api/rerources/api_entry_info";
// import {EntryInfo, EntryInfoType} from "../../api/api_types";
//
// declare type EntryCardContactsStoreState = {
//     isShowDeleted: boolean | null,
//     isLoading: boolean,
//     list: EntryInfo[]
// }
// export const useEntryCardContactsStore = defineStore('EntryCardContactsStore', {
//     state: (): EntryCardContactsStoreState => {
//         return {
//             isShowDeleted: false,
//             isLoading: false,
//             list: [],
//         }
//     },
//     // getters: {
//     //     dates: (state) => state.list.filter(eInfo => {
//     //         if (state.isShowDeleted === true) return eInfo.deletedAt;
//     //         if (state.isShowDeleted === false) return !eInfo.deletedAt;
//     //         return eInfo;
//     //     }),
//     //     phones: (state) => state.Phone.list.filter(eInfo => {
//     //         if (state.isShowDeleted === true) return eInfo.deletedAt;
//     //         if (state.isShowDeleted === false) return !eInfo.deletedAt;
//     //         return eInfo;
//     //     }),
//     //     emails: (state) => state.Email.list.filter(eInfo => {
//     //         if (state.isShowDeleted === true) return eInfo.deletedAt;
//     //         if (state.isShowDeleted === false) return !eInfo.deletedAt;
//     //         return eInfo;
//     //     }),
//     //     urls: (state) => state.Url.list.filter(eInfo => {
//     //         if (state.isShowDeleted === true) return eInfo.deletedAt;
//     //         if (state.isShowDeleted === false) return !eInfo.deletedAt;
//     //         return eInfo;
//     //     }),
//     // },
//     actions: {
//         filterByType (type: EntryInfoType) {
//             return this.list.filter(eInfo => {
//                 if (this.isShowDeleted === true) return eInfo.deletedAt && eInfo.type === type;
//                 if (this.isShowDeleted === false) return !eInfo.deletedAt && eInfo.type === type;
//                 return eInfo.type === type;
//             })
//         },
//         async getAllContacts(entryId: string) {
//             await this.getPhones(entryId);
//             await this.getEmails(entryId);
//             await this.getDates(entryId);
//             await this.getUrls(entryId);
//         },
//         async getBase(entryId: string, key: 'PHONE' | 'EMAIL' | 'URL' | 'DATE') {
//             if (this[key].isLoading) {
//                 return;
//             }
//             this[key].isLoading = true;
//
//             try {
//                 this[key].list = await apiEntryInfo[key].list(entryId, {});
//             } finally {
//                 this[key].isLoading = false;
//             }
//         },
//         async getPhones(entryId: string) {
//             await this.getBase(entryId, 'PHONE');
//         },
//         async getUrls(entryId: string) {
//             await this.getBase(entryId, 'URL');
//         },
//         async getEmails(entryId: string) {
//             await this.getBase(entryId, 'EMAIL');
//         },
//         async getDates(entryId: string) {
//             await this.getBase(entryId, 'DATE');
//         }
//     }
// })