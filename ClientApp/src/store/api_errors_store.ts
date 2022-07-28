import {defineStore} from 'pinia';

export declare type ApiError = {
    errors: {
        [key: string]: string[]
    }
    title: string,
    status: number
}
declare type ApiErrorsStoreState = {
    apiError: null | ApiError
}
export const useApiErrorsStore = defineStore('ApiErrorsStore', {
    state: (): ApiErrorsStoreState => {
        return {
            apiError: null
        }
    },
})