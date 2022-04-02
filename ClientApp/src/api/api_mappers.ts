import {Entry, EntryText, UpdateEntryRequest, UpdateEntryTextRequest} from "./api_types";

export const apiMappers = {
    toUpdateEntryRequest: (entry: Entry): UpdateEntryRequest => {
        return {
            name: entry.name,
            entryType: entry.entryType,
            description: entry.description,
            reputation: entry.reputation,
            startAt: entry.startAt,
            endAt: entry.endAt,
            actualStartAt: entry.actualStartAt,
            actualEndAt: entry.actualEndAt,
            actualStartAtReason: entry.actualStartAtReason,
            actualEndAtReason: entry.actualEndAtReason,
        }
    },
    toUpdateEntryTextRequest: (eText: EntryText) : UpdateEntryTextRequest => {
        return {
            val: eText.val,
            title: eText.title,
            textType: eText.textType,
            actualStartAt: eText.actualStartAt,
            actualEndAt: eText.actualEndAt,
            actualStartAtReason: eText.actualStartAtReason,
            actualEndAtReason: eText.actualEndAtReason,
        }
    },
}