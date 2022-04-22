﻿import {
    Entry,
    EntryUpdateRequest,
    EntryEmailFormRequest,
    EntryNoteFormRequest,
    EntryPhoneFormRequest,
    EntryUrlFormRequest,
    EntryDateFormRequest,
    EntryEmail,
    EntryInfo,
    EntryInfoCommonFormRequest,
    EntryNote,
    EntryUrl,
    EntryPhone,
    EntryDate,
    FileModel, FileModelUpdateRequest,
} from "./api_types";

export const apiMappers = {
    toFileModelUpdateRequest: (model: FileModel): FileModelUpdateRequest => {
        return {
            name: model.name,
            deletedAt: model.deletedAt,
            deletedReason: model.deletedReason,
        }  
    },
    toEntryUpdateRequest: (entry: Entry): EntryUpdateRequest => {
        return {
            name: entry.name,
            entryType: entry.entryType,
            description: entry.description,
            reputation: entry.reputation,
            startAt: entry.startAt,
            endAt: entry.endAt,
            deletedAt: entry.deletedAt,
            deletedReason: entry.deletedReason,
            avatar: entry.avatar,
        }
    },
    toEntryInfoFormRequest: {
        toCommon: (eInfo: EntryInfo): EntryInfoCommonFormRequest => {
            return {
                title: eInfo.title,
                deletedAt: eInfo.deletedAt,
                deletedReason: eInfo.deletedReason,
            }
        },
        Email: (eInfo: EntryEmail): EntryEmailFormRequest => {
            return {
                ...apiMappers.toEntryInfoFormRequest.toCommon(eInfo),
                email: eInfo.email,
            }
        },
        Note: (eInfo: EntryNote): EntryNoteFormRequest => {
            return {
                ...apiMappers.toEntryInfoFormRequest.toCommon(eInfo),
                note: eInfo.note,
            }
        },
        Url: (eInfo: EntryUrl): EntryUrlFormRequest => {
            return {
                ...apiMappers.toEntryInfoFormRequest.toCommon(eInfo),
                url: eInfo.url,
            }
        },
        Phone: (eInfo: EntryPhone): EntryPhoneFormRequest => {
            return {
                ...apiMappers.toEntryInfoFormRequest.toCommon(eInfo),
                phoneRegion: eInfo.phoneRegion,
                phoneNumber: eInfo.phoneNumber,
            }
        },
        Date: (eInfo: EntryDate): EntryDateFormRequest => {
            return {
                ...apiMappers.toEntryInfoFormRequest.toCommon(eInfo),
                date: eInfo.date,
            }
        },
    },
    
}