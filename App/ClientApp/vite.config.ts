import {defineConfig, searchForWorkspaceRoot} from 'vite'
import vue from '@vitejs/plugin-vue'
import {quasar, transformAssetUrls} from '@quasar/vite-plugin'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        vue({
            template: {transformAssetUrls}
        }),
        quasar({
            sassVariables: 'src/quasar-variables.sass'
        })
    ],
    server: {
        fs: {
            // Allow serving files from one level up to the project root
            allow: [
                searchForWorkspaceRoot(process.cwd()),
                'C:/app/RiderProjects/Ras/App/ClientApp'
            ]
        }
    },
})
