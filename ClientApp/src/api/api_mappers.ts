import {Entry, UpdateEntryRequest} from "./api_types";

export const entryMappers = {
    toUpdateRequest: (entry: Entry): UpdateEntryRequest => {
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
    }
}