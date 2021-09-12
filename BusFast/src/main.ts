import { createApp } from 'vue'
import App from './App.vue'
import Vue from 'vue'
import router from './router'
import { DateMixin } from './components/DateMixin'

createApp(App).use(router).mount('#app');
