// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import 'element-ui/lib/theme-chalk/index.css';
import 'javascript-extension';
import ElementUI from 'element-ui';
import Vue from 'vue';
import Vuex from 'vuex';
import VueRouter from 'vue-router';
import global from './global';
import modules from './store';
import App from './components/App';
import routes from './router';
import './assets/styles/material-design-iconic-font.min.css';
import './assets/styles/main.css';

Vue.use(ElementUI);
Vue.use(Vuex);
Vue.use(VueRouter);
Vue.use(global);

const store = new Vuex.Store({
  modules,
});

const router = new VueRouter({
  mode: 'history',
  routes,
});

Vue.config.productionTip = false;

/* eslint-disable no-new */
new Vue({
  el: '#app',
  store,
  router,
  components: { App },
  template: '<App/>',
});
