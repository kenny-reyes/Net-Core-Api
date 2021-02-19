import Vue from 'vue';
import Vuex from 'vuex';
import users from './modules/users';

Vue.use(Vuex);

const modules = {
  users,
};

const state = {
  sidebarShow: 'responsive',
  sidebarMinimize: true,
  featureCollection: undefined,
};

const mutations = {
  //   toggleSidebarDesktop(state) {
  //     const sidebarOpened = [true, 'responsive'].includes(state.sidebarShow);
  //     state.sidebarShow = sidebarOpened ? false : 'responsive';
  //   },
  //   toggleSidebarMobile(state) {
  //     const sidebarClosed = [false, 'responsive'].includes(state.sidebarShow);
  //     state.sidebarShow = sidebarClosed ? true : 'responsive';
  //   },
  //   set(state, [variable, value]) {
  //     state[variable] = value;
  //   },
  //   loadFeatureCollection(state, collection) {
  //     state.featureCollection = collection;
  //   },
};

const getters = {};

const actions = {};

export default new Vuex.Store({
  modules,
  state,
  mutations,
  actions,
  getters,
});
