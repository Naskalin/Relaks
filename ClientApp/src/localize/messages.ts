export const entryTypeTrans: {[index: string]: string} = {
    Person: 'Человек',
    Company: 'Компания',
    Meet: 'Встреча',
}
export const entryDescriptionHelpers = {
    Person: 'Бородатый программист',
    Company: 'Крупногабаритные перевозки',
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

export const entryProfileTabs = {
    'entry-note-list': 'Заметки',
    'entry-file-list': 'Файлы',
    // dates: 'Даты',
    // kinship: 'Родство',
    // structures: 'Структуры',
}