// import { createApp } from 'vue'
// import './style.css'
// import App from './App.vue'

// createApp(App).mount('#app')


import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import  from './style.css'

import './index.css' // your tailwind styles or any global css

const app = createApp(App)
app.use(router)
app.mount('#app')
