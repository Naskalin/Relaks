import { createApp } from 'vue'
import App from './App.vue'
import {Quasar} from 'quasar';
// import '@quasar/extras/roboto-font-latin-ext/roboto-font-latin-ext.css'
import '@quasar/extras/material-icons/material-icons.css'
import '@quasar/extras/line-awesome/line-awesome.css'

import 'quasar/src/css/index.sass'
import './css/misc.sass';
import router from './router';
import {searchForWorkspaceRoot} from "vite";
//
// console.log(searchForWorkspaceRoot(process.cwd()));

import { createPinia } from 'pinia';

const myApp = createApp(App)
myApp.use(Quasar, {
    // plugins: {},
    config: {
        // iconSet: 'line-awesome',
        
        // brand: {
        //     // primary: '#e46262',
        //     // ... or all other brand colors
        // },
        // notify: {...}, // default set of options for Notify Quasar plugin
        // loading: {...}, // default set of options for Loading Quasar plugin
        // loadingBar: { ... }, // settings for LoadingBar Quasar plugin
        // // ..and many more (check Installation card on each Quasar component/directive/plugin)
    }
});
myApp.use(router);
myApp.use(createPinia());
myApp.mount('#app');