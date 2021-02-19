import Vue from 'vue';
import App from './App.vue';
import GetVuetify from './plugins/vuetify';
import router from './router';
import jsonFile from '../configuration.json';

Vue.config.productionTip = false;

Vue.prototype.$rmConfig = jsonFile;

const vuetify = GetVuetify(Vue.prototype.$rmConfig);

new Vue({
  vuetify,
  router,
  render: (h) => h(App),
}).$mount('#app');
