import {selectHelper} from "../utils/select_helper";

const entryTypeNames = {
    Person: 'Человек',
    Company: 'Компания',
    Meet: 'Встреча',
};
const entryInfoTypes = {
    Phone: 'Телефон',
    Email: 'E-mail',
    Url: 'Ссылка',
    Note: 'Заметка',
    Date: 'Дата'
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
    name: {
        Person: 'Ф.И.О',
        Company: 'Название компании',
        Meet: 'Название встречи'
    },
    entryType: {
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
        tabs: {
            'entry-note-list': 'Заметки',
            // 'entry-contact-list': 'Контакты',
            'entry-file-list': 'Файлы',
            // dates: 'Даты',
            // kinship: 'Родство',
            // structures: 'Структуры',
        }
    },
    startAt: {
        Person: 'День рождения',
        Company: 'Дата регистрации',
        Meet: 'Дата начала'
    },
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