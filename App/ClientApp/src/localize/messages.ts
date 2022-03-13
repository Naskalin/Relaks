﻿export const entryTypeTrans = {
    Person: 'Человек',
    Company: 'Компания',
    Meet: 'Встреча',
}
export const entryDescriptionHelpers = {
    Person: 'Бородатый программист',
    Company: 'Занимаются крупногабаритными перевозками',
    Meet: 'Ежегодное совещание с коллегами по работе',
}
export const entryDateFields = {
    Person: {
        startAt: 'День рождения',
        endAt: 'Дата смерти',
    },
    Company: {
        startAt: 'Дата регистрации',
        endAt: 'Дата ликвидации',
    },
    Meet: {
        startAt: 'Дата начала',
        endAt: 'Дата окончания',
    }
}
export const entryTypesSelectOptions = () => {
    const arr = [];
    for (const [key, value] of Object.entries(entryTypeTrans))
    {
        arr.push({value: key, label: value})
        console.log(arr);
    }
    return arr;
}

export const entryProfileTabsTrans = {
    notes: 'Заметки',
    dates: 'Даты',
    files: 'Файлы',
    kinship: 'Родство',
    structures: 'Структуры',
}