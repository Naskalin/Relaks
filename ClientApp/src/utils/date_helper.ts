import {date as qDate} from "quasar";
import {appDefaults} from "../app_defaults";

// ## dayjs
import dayjs from "dayjs";
import timezone from 'dayjs/plugin/timezone';
import utc from 'dayjs/plugin/utc';
import 'dayjs/locale/ru';
import customParseFormat from 'dayjs/plugin/customParseFormat';

dayjs.extend(utc);
dayjs.extend(customParseFormat);
dayjs.extend(timezone);
dayjs.locale('ru')
//-- ## dayjs

const isISO = (str: string): boolean => {
    if (!/\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z/.test(str)) return false;
    const d = new Date(str);
    return d.toISOString() === str;
}

export const dateHelper = {
    toISO: (val: string | null | undefined, format: string = 'DD.MM.YYYY HH:mm'): string | null => {
        if (val && val !== '') {
            // Пропускаем уже форматированные даты
            if (isISO(val)) return val;

            return qDate.extractDate(val, format).toISOString()
        }

        return null;
    },
    utcFormat: (val: string | null, format: string = 'DD.MM.YYYY HH:mm'): string => {
        if (!val || val === '') return '';
        // Переводим дату в utc, если ещё не в utc
        const d = dayjs(val).utc(!isISO(val));
        return d.tz(appDefaults.timeZone).format(format);
    }
}