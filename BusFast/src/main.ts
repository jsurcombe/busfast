import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import Location from './components/Location.vue'

createApp(App)
  .use(router)
  .component("location", Location)
  .mount('#app');

