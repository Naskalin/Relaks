import {ApiListRequest} from "./index";

export declare type EntryType = 'Person' | 'Company' | 'Meet';
export declare type EntryTextType = 'Note' | 'Phone' | 'Email' | 'Url';

export declare type ActualTypes = {
    actualStartAt: string
    actualEndAt: string | null
    actualStartAtReason: string
    actualEndAtReason: string
}

export declare type TimestampTypes = {
    createdAt: string
    updatedAt: string
}

// Entry
export declare type CreateEntryRequest = {
    name: string
    entryType: EntryType
    description: string
    reputation: number
    startAt: string | null
    endAt: string | null
} & ActualTypes
export declare type UpdateEntryRequest = {} & CreateEntryRequest
export declare type ListEntryRequest = {
    page: number
    perPage: number
    entryType: EntryType } & ApiListRequest
export declare type Entry = { id: string } & CreateEntryRequest & TimestampTypes;

// EntryDate
export declare type CreateEntryDateRequest = {
    title: string,
    val: string
    entryId: string
} & ActualTypes
export declare type UpdateEntryDateRequest = {} & CreateEntryDateRequest
export declare type ListEntryDateRequest = {} & ApiListRequest
export declare type EntryDate = { id: string, entry: Entry } & CreateEntryDateRequest & ActualTypes & TimestampTypes;

// EntryText
export declare type CreateEntryTextRequest = {
    title: string
    val: string
    textType: EntryTextType
} & ActualTypes
export declare type UpdateEntryTextRequest = {} & CreateEntryTextRequest
export declare type ListEntryTextRequest = { textType: EntryTextType } & ApiListRequest
export declare type EntryText = { id: string, entry: Entry } & CreateEntryTextRequest & ActualTypes & TimestampTypes;
