import {SoftDeletableType, TimestampTypes} from "../api_types";
import {generateDefaultApi} from "../api_generic";
import {appApi} from "../index";

export declare type FileCategory = {
    id: string
    title: string
    discriminator: string
    parentId: string | null
} & TimestampTypes & SoftDeletableType

export declare type EntryFileCategory = {
    entryId: string
} & FileCategory

export declare type FileCategoryFormRequest = {
    title: string
    entryId: string
    parentId: string | null
    deletedAt: string | null
    deletedReason: string
}

export declare type FileCategoryTree = {
    id: string
    data: FileCategory,
    children: FileCategoryTree[],
    isRoot: boolean,
    isLeaf: boolean,
    level: number
}

export declare type FileCategoryListRequest = {
    entryId: string
    isTree?: boolean | null
    isDeleted?: boolean | null
}

export const apiFileCategories = generateDefaultApi<
    FileCategory,
    FileCategoryFormRequest,
    FileCategoryFormRequest,
    FileCategoryListRequest,
    {}>(['file-categories']);