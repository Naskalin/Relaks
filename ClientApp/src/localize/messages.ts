import {selectHelper} from "../utils/select_helper";

const entryTypeNames = {
    Person: 'Человек',
    Company: 'Компания',
    Meet: 'Встреча',
};

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
            'entry-contacts': 'Контакты',
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
    reputation: 'Репутация'
}

export const actualMessages = {
    actualStartAt: {
        name: 'Актуально с'
    },
    actualEndAt: {
        name: 'Не актуально с'
    },
    actualStartAtReason: {
        name: 'Актуально по причине'
    },
    actualEndAtReason: {
        name: 'Не актуально по причине'
    }
}