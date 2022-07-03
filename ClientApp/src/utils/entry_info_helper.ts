import {EntryInfo, EntryInfoType} from "../api/api_types";

export function filterByType(list: EntryInfo[], type: EntryInfoType, isShowDeleted?: boolean | null) {
    return list.filter(eInfo => {
        if (eInfo.type !== type) return false;
        if (isShowDeleted === true) return eInfo.deletedAt;
        if (isShowDeleted === false) return !eInfo.deletedAt;
        return eInfo;
    });
}