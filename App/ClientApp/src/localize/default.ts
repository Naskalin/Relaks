export const entryTypeTrans = {
    'Person': 'Человек',
    'Company': 'Компания',
    'Meet': 'Встреча',
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