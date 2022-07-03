import {EntryInfo, EntryInfoType} from "../api/api_types";

export function filterByType(isShowDeleted: boolean | null, type: EntryInfoType, list: EntryInfo[]) {
    return list.filter(eInfo => {
        if (eInfo.type !== type) return false;
        if (isShowDeleted === true) return eInfo.deletedAt;
        if (isShowDeleted === false) return !eInfo.deletedAt;
        return eInfo;
    });
}