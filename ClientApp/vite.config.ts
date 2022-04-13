import {defineConfig, searchForWorkspaceRoot} from 'vite'
import vue from '@vitejs/plugin-vue'
import {quasar, transformAssetUrls} from '@quasar/vite-plugin';
import * as fs from 'fs';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        vue({
            template: {transformAssetUrls}
        }),
        quasar({
            sassVariables: 'src/quasar-variables.sass',
        })
    ],
    server: {
        proxy: {
            // '/files': {target: 'https://localhost:7125', secure: false, changeOrigin: true},
            '/api': {target: 'https://localhost:7125', secure: false, changeOrigin: true},
        },
        fs: {
            // Allow serving files from one level up to the project root
            allow: [
                searchForWorkspaceRoot(fs.realpathSync(process.cwd())),
                'C:/app/RiderProjects/Relaks/ClientApp',
            ]
        }
    }
})