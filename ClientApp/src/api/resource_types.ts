export declare type EntryType = 'Person' | 'Company' | 'Meet';
export declare type TextType = 'Note' | 'Phone' | 'Email' | 'Url';

export declare type ActualPart = {
    actualStartAt: string
    actualEndAt: string | null
    actualStartAtReason: string
    actualEndAtReason: string
}

export declare type CreatedUpdatedPart = {
    createdAt: string
    updatedAt: string
}

export declare type Entry = {
    id: string
    name: string
    entryType: EntryType
    description: string
    reputation: number

    startAt: string | null
    endAt: string | null
} & ActualPart & CreatedUpdatedPart;

export declare type EntryDate = {
    id: string
    entryId: string
    entry: Entry
    textType: TextType
    about: string
    val: string
}

export declare type EntryText = {
    textType: TextType
} & EntryDate & ActualPart & CreatedUpdatedPart;