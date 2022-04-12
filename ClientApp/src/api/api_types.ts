import {ApiListRequest} from "./index";

export declare type EntryType = 'Person' | 'Company' | 'Meet';
export declare type EntryInfoType = 'Note' | 'Phone' | 'Email' | 'Url' | 'Date';

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

export declare type PhoneType = {
    phoneNumber: string
    phoneRegion: string
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
} & SoftDeletableType

export declare type EntryUpdateRequest = {} & EntryCreateRequest
export declare type EntryListRequest = {
    entryType: EntryType
} & PaginateListRequest
export declare type Entry = { id: string } & EntryCreateRequest & TimestampTypes;

// EntryInfo
export declare type EntryInfoCommonFormRequest = {
    title: string
} & SoftDeletableType
export declare type EntryInfo = {
    id: string,
    entryId: string,
    title: string,
    discriminator: EntryInfoType
} & SoftDeletableType & TimestampTypes

// EntryDate
export declare type EntryDateFormRequest = { date: string } & EntryInfoCommonFormRequest
export declare type EntryDate = { date: string } & EntryInfo

// EntryNote
export declare type EntryNoteFormRequest = { note: string } & EntryInfoCommonFormRequest
export declare type EntryNote = { note: string } & EntryInfo

// EntryPhone
export declare type EntryPhoneFormRequest = { phoneNumber: string, phoneRegion: string } & EntryInfoCommonFormRequest
export declare type EntryPhone = {} & PhoneType & EntryInfo

// EntryEmail
export declare type EntryEmailFormRequest = { email: string } & EntryInfoCommonFormRequest
export declare type EntryEmail = { email: string } & EntryInfo

// EntryUrl
export declare type EntryUrlFormRequest = { url: string } & EntryInfoCommonFormRequest
export declare type EntryUrl = { url: string } & EntryInfo

// EntryFile
export declare type EntryFile = {
    id: string
    name: string
    path: string
    contentType: string
    entryId: string
} & TimestampTypes & SoftDeletableType
export declare type EntryFileCreateResponse = {
    count: number
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
