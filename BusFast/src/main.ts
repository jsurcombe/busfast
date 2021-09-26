import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import Location from './components/Location.vue'
import Title from './components/Title.vue';

createApp(App)
  .use(router)
  .component("location", Location)
  .component("vue-title", Title)
  .mount('#app');

