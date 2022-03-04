import { createApp } from 'vue'
import App from './App.vue'
import {Quasar} from 'quasar';
import '@quasar/extras/roboto-font-latin-ext/roboto-font-latin-ext.css'
import '@quasar/extras/material-icons/material-icons.css'
// import '@quasar/extras/line-awesome/line-awesome.css'
import 'quasar/src/css/index.sass'
import router from './router';
import {searchForWorkspaceRoot} from "vite";
//
// console.log(searchForWorkspaceRoot(process.cwd()));

import { createPinia } from 'pinia';

const myApp = createApp(App)
myApp.use(Quasar, {
    // plugins: {},
});
myApp.use(router);
myApp.use(createPinia());
myApp.mount('#app');