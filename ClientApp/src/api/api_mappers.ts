import {
    Entry,
    EntryUpdateRequest,
    FileModel, FileModelUpdateRequest,
} from "./api_types";

export const apiMappers = {
    toFileModelUpdateRequest: (model: FileModel): FileModelUpdateRequest => {
        return {
            name: model.name,
            deletedAt: model.deletedAt,
            deletedReason: model.deletedReason,
            category: model.category,
            tags: model.tags
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
}