import { createApp } from 'vue'
import App from './App.vue'
import {Quasar, Dialog, Notify} from 'quasar';
import '@quasar/extras/material-icons/material-icons.css'
import '@quasar/extras/line-awesome/line-awesome.css'
import 'quasar/src/css/index.sass'
import 'flag-icons/sass/flag-icons.scss'

import langRu from 'quasar/lang/ru'
import router from './router';
import { createPinia } from 'pinia';

import {VTooltip, Tooltip} from 'floating-vue';
import 'floating-vue/dist/style.css';

const myApp = createApp(App)
myApp.use(Quasar, {
    plugins: {
        Dialog,
        Notify,
    },
    lang: langRu,
    // config: {
    //     lang: langRu,
        // iconSet: 'line-awesome',
        
        // brand: {
        //     // primary: '#e46262',
        //     // ... or all other brand colors
        // },
        // notify: {...}, // default set of options for Notify Quasar plugin
        // loading: {...}, // default set of options for Loading Quasar plugin
        // loadingBar: { ... }, // settings for LoadingBar Quasar plugin
        // // ..and many more (check Installation card on each Quasar component/directive/plugin)
    // }
});
// myApp.use(TooltipPlugin);

myApp.directive('tooltip', VTooltip);
myApp.component('VTooltip', Tooltip)
myApp.use(router);
myApp.use(createPinia());
myApp.mount('#app');