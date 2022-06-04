export const selectHelper = {
    toSelectOptions: (simpleObject: { [index: string]: string }) => {
        const arr = [];
        for (const [key, value] of Object.entries(simpleObject)) {
            arr.push({value: key, label: value})
        }
        return arr;
    },
    arrayToSelectOptions: (options: string[]) => {
        return options.map((val: string) => {
            return {value: val, label: val};
        });
    }
}