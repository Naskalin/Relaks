﻿import {ApiListRequest} from "./index";

export declare type EntryType = 'Person' | 'Company' | 'Meet';
export declare type EntryInfoType = 'Note' | 'Phone' | 'Email' | 'Url' | 'Date' | 'Passport' | 'CompanyDetails' | 'Custom';
export declare type EntryInfoObjectType = InfoDate | InfoEmail | InfoNote | InfoUrl | InfoPhone;

// export declare type ActualTypes = {
//     actualStartAt: string
//     actualEndAt: string | null
//     actualStartAtReason: string
//     actualEndAtReason: string
// }

export declare type SoftDeletableType = {
    deletedAt: string | null
    deletedReason: string
}
export declare type TimestampTypes = {
    createdAt: string
    updatedAt: string
}

// export declare type PhoneType = {
//     phoneNumber: string
//     phoneRegion: string
// }

export declare type FtsResultType = {
    snippet?: string
}

export declare type PaginateListRequest = {
    page: number
    perPage: number
} & ApiListRequest

// Entry
export declare type EntryCreateRequest = {
    name: string
    entryType: EntryType
    description: string
    reputation: number
    startAt: string | null
    endAt: string | null
    avatar: string | null
} & SoftDeletableType

export declare type EntryUpdateRequest = {} & EntryCreateRequest
export declare type EntryListRequest = {
    entryType: EntryType | null
} & PaginateListRequest
export declare type Entry = { id: string } & EntryCreateRequest & TimestampTypes & FtsResultType;

// EntryInfo
export declare type EntryInfoFormRequest = {
    title: string
    type: EntryInfoType
    info: EntryInfoObjectType
} & SoftDeletableType
export declare type EntryInfoListRequest = {
    type?: EntryInfoType | EntryInfoType[] | null
} & ApiListRequest

export declare type EntryInfo = {
    id: string,
    entryId: string,
} & SoftDeletableType & TimestampTypes & EntryInfoFormRequest

// EntryDate
// export declare type EntryDateFormRequest = { date: string } & EntryInfoCommonFormRequest
export declare type InfoDate = { date: string }

// EntryNote
// export declare type EntryNoteFormRequest = { note: string } & EntryInfoCommonFormRequest
export declare type InfoNote = { note: string }

// EntryPhone
// export declare type EntryPhoneFormRequest = { phoneNumber: string, phoneRegion: string } & EntryInfoCommonFormRequest
export declare type InfoPhone = {
    number: string
    region: string
}

// EntryEmail
// export declare type EntryEmailFormRequest = { email: string } & EntryInfoCommonFormRequest
export declare type InfoEmail = { email: string }

// EntryUrl
// export declare type EntryUrlFormRequest = { url: string } & EntryInfoCommonFormRequest
export declare type InfoUrl = { url: string }

export declare type FileModel = {
    id: string
    name: string
    path: string
    contentType: string
    category: string
    tags: string[]
} & TimestampTypes & SoftDeletableType
export declare type FileModelUpdateRequest = {
    name: string
    category: string
    tags: string[]
} & SoftDeletableType
// EntryFile
export declare type EntryFile = {
    entryId: string
} & FileModel
export declare type EntryFileCreateResponse = {
    count: number
}

export declare type EntryFileMeta = {
    categories: string[],
    tags: string[]
}

// FileDownload
export declare type FileDownloadRequest = {
    fileId: string,
    imageFilter?: string
}

// export declare type EntryFileCreateRequest = FormData

// export declare type CreateEntryTextRequest = {
//     title: string
//     val: string
//     textType: EntryInfoType
// } & SoftDeletableType
// export declare type UpdateEntryTextRequest = {} & CreateEntryTextRequest
// export declare type ListEntryTextRequest = { textType: EntryInfoType } & ApiListRequest
// export declare type EntryText =
//     { id: string, entry: Entry, entryId: string }
//     & CreateEntryTextRequest
//     & SoftDeletableType
//     & TimestampTypes;
