import {defineStore} from 'pinia';
import {appApi} from "../../../api";

export declare type AppPresetModel = {
    timezone: string,
    phoneRegion: string
}
declare type AppPresetStoreState = {
    dataDir: string,
    appPreset: AppPresetModel | null
}
export const useAppPresetStore = defineStore('AppPresetStore', {
    state: (): AppPresetStoreState => {
        return {
            dataDir: '',
            appPreset: null,
        }
    },
    actions: {
        async getAppPreset() {
            const resp = await appApi.get(['app-preset']);
            if (resp.data) this.appPreset = resp.data;
        },
        async updateDataDir() {
            await appApi.post(['app-data-dir'], {dataDir: this.dataDir})
            this.dataDir = '';
        }
    }
})