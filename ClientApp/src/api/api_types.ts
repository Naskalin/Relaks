import {ApiListRequest} from "./index";

export declare type EntryType = 'Person' | 'Company' | 'Meet';
export declare type TextType = 'Note' | 'Phone' | 'Email' | 'Url';

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
    about: string,
    val: string
    entryId: string
} & ActualTypes
export declare type UpdateEntryDateRequest = {} & CreateEntryDateRequest
export declare type ListEntryDateRequest = {} & ApiListRequest
export declare type EntryDate = { id: string, entry: Entry } & CreateEntryDateRequest & ActualTypes & TimestampTypes;

// EntryText
export declare type CreateEntryTextRequest = {
    entryId: string
    about: string
    val: string
    textType: TextType
} & ActualTypes
export declare type UpdateEntryTextRequest = {} & CreateEntryTextRequest
export declare type ListEntryTextRequest = { textType: TextType } & ApiListRequest
export declare type EntryText = { id: string, entry: Entry } & CreateEntryTextRequest & ActualTypes & TimestampTypes;
