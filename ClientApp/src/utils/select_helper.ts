export const selectHelper = {
    toSelectOptions: (simpleObject: { [index: string]: string }) => {
        const arr = [];
        for (const [key, value] of Object.entries(simpleObject))
        {
            arr.push({value: key, label: value})
        }
        return arr;
    }
}