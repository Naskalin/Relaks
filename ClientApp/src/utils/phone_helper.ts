declare interface Phone {
    number: string
    region: string
}

export const phoneHelper = {
    toPhone: (regionWithPhone: string): Phone => {
        const strSplit = regionWithPhone.split('|');
        return {region: strSplit[0], number: strSplit[1]}
    },
    toString: (phone: Phone): string => {
        return phone.region + "|" + phone.number;
    }
}
