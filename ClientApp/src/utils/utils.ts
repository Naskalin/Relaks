import {date} from "quasar";

const isIsoDateString = (str: string): boolean => {
    if (!/\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z/.test(str)) return false;
    const d = new Date(str);
    return d.toISOString() === str;
}

export const dateToISONormalizer = (val: string | null | undefined, format: string = 'DD.MM.YYYY HH:mm'): string | null => {
    if (val && val !== '') {
        // Пропускаем уже форматированные даты
        if (isIsoDateString(val)) return val;

        return date.extractDate(val, format).toISOString()
    }

    return null;
}