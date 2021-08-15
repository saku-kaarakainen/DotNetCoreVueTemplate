import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

const app = createApp(App)
app.use(store).use(router).mount('#app')

// TODO: you should hide this inside configurations and not use it in the production
// https://v3.vuejs.org/api/application-config.html#performance
// Set this to true to enable component init, compile, render and patch performance tracing in the browser devtool performance/timeline panel.
app.config.performance = true
