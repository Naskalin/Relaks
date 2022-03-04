export declare interface BaseEntry {
    id: string
    name: string,
    entryType: EntryType,
    reputation: number,
    createdAt: string,
}

export declare interface Person extends BaseEntry {
    birthDay?: string
}

export declare type EntryType = 'person' | 'company' | 'meet';

export declare type Nullable<T> = T | null;