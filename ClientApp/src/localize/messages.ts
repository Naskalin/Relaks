import {selectHelper} from "../utils/select_helper";
import {EntryType} from "../api/api_types";

const entryTypeNames = {
    Person: 'Человек',
    Company: 'Компания',
    Meet: 'Встреча',
};
const entryInfoTypes = {
    PHONE: 'Телефон',
    EMAIL: 'E-mail',
    URL: 'Ссылка',
    NOTE: 'Заметка',
    DATE: 'Дата',
}

export const timeStampMessages = {
    createdAt: 'Создано',
    updatedAt: 'Обновлено'
}

export const deletedMessages = {
    deletedAt: 'В архиве с',
    deletedReason: 'Причина архивации'
}

export const entryMessages = {
    nameNull: 'Ф.И.О / Название',
    name: {
        Person: 'Ф.И.О',
        Company: 'Название компании',
        Meet: 'Название встречи'
    },
    entryType: {
        icons: {
            Person: 'las la-user',
            Meet: 'las la-user-friends',
            Company: 'las la-handshake'
        },
        selectOptions: selectHelper.toSelectOptions(entryTypeNames),
        names: entryTypeNames,
        pluralNames: {
            Person: 'Люди',
            Company: 'Компании',
            Meet: 'Встречи'
        },
        descriptionHelpers: {
            Person: 'Бородатый программист',
            Company: 'Крупногабаритные перевозки',
            Meet: 'Ежегодное совещание с коллегами по работе',
        }
    },
    profile: {
        tabs: (entryType: EntryType) => {
            let entryAboutName = 'О человеке';
            if (entryType == 'Meet') entryAboutName = 'О встрече';
            else if (entryType == 'Company') entryAboutName = 'О компании';
            
            return {
                'entry-about': entryAboutName,
                // 'entry-contact-list': 'Контакты',
                'entry-file-list': 'Файлы',
                // dates: 'Даты',
                // kinship: 'Родство',
                // structures: 'Структуры',
            }
        },
    },
    startAtNull: 'Дата рождения/регистрации/начала',
    startAt: {
        Person: 'День рождения',
        Company: 'Дата регистрации',
        Meet: 'Дата начала'
    },
    endAtNull: 'Дата смерти/ликвидации/окончания',
    endAt: {
        Person: 'Дата смерти',
        Company: 'Дата ликвидации',
        Meet: 'Дата окончания'
    },
    reputation: 'Репутация',
    ...timeStampMessages,
    ...deletedMessages,
}

export const entryInfoMessages = {
    ...timeStampMessages,
    ...deletedMessages,
    title: 'Название',
    val: {
        names: entryInfoTypes,
        // selectOptions: selectHelper.toSelectOptions(entryInfoTypes),
    },
}
export const fileFieldNames = {
    name: 'Название',
    contentType: 'Тип',
    ...timeStampMessages,
    ...deletedMessages,
}

// export const actualMessages = {
//     actualStartAt: {
//         name: 'Актуально с'
//     },
//     actualEndAt: {
//         name: 'Не актуально с'
//     },
//     actualStartAtReason: {
//         name: 'Актуально по причине'
//     },
//     actualEndAtReason: {
//         name: 'Не актуально по причине'
//     }
// }