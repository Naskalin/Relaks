import {date} from "quasar";

export const dateStrToISOStr = (val: string | null | undefined, format: string = 'YYYY/MM/DD'): string | null => {
    if (val && val !== '') {
        return date.extractDate(val, format).toISOString()
    }

    return null;
}