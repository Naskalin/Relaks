import {defineConfig, searchForWorkspaceRoot, loadEnv} from 'vite'
import vue from '@vitejs/plugin-vue'
import {quasar, transformAssetUrls} from '@quasar/vite-plugin';
import * as fs from 'fs';

// https://vitejs.dev/config/#conditional-config
export default defineConfig(({ mode }) => {
    // Load env file based on `mode` in the current working directory
    let proxy = {};
    let allow = [
        searchForWorkspaceRoot(fs.realpathSync(process.cwd())),
    ];
    
    if (mode === 'development') {
        proxy = {
            '/api': {target: 'http://localhost:5125', secure: false, changeOrigin: true},
        };
        allow.push('C:/app/RiderProjects/Relaks/ClientApp');
    }
    return {
        plugins: [
            vue({
                template: {transformAssetUrls}
            }),
            quasar({
                sassVariables: 'src/quasar-variables.sass',
            })
        ],
        server: {
            proxy: proxy,
            fs: {
                allow: allow
            }
        }
    }
})