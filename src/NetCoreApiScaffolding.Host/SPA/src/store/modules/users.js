import usersService from "../../api/Users.service";
const state = () => ({
  all: [],
});

const getters = {};
const actions = {
  getAll({ commit }) {
    usersService.getAll().then((result) => {
      commit("setAll", result);
    });
  },
};
const mutations = {
  setAll(state, action) {
    state.all = action;
  },
};
export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
