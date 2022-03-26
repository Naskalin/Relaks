export declare type PhoneType = {
    number: string
    region: string
}

export const phoneHelper = {
    toPhone: (regionWithPhone: string): PhoneType => {
        if (regionWithPhone === '') throw Error('ToPhone format error, empty string.');
        const strSplit = regionWithPhone.split('|');
        return {region: strSplit[0], number: strSplit[1]}
    },
    toString: (phone: PhoneType): string => {
        return phone.region + "|" + phone.number;
    }
}
