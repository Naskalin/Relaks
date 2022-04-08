import {defineStore} from 'pinia';

export declare type ApiError = {
    errors: {
        [key: string]: string[]
    }
    status: number
    title: string
    traceId: string
    type: string
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