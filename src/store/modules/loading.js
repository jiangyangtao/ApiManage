import loadingType from '../types/loadingType';

const state = {
  loading: false,
};

const getters = {
  [loadingType._types.getters.loading]: state => state.loading,
};

const actions = {// 异步
  [loadingType._types.actions.showLoading](store, text) {
    state.loading = true;
  },
  [loadingType._types.actions.hideLoading](store, text) {
    state.loading = false;
  },
};

const mutations = {// 同步
  [loadingType._types.mutations.showLoading](store, text) {
    state.loading = true;
  },
  [loadingType._types.mutations.hideLoading](store, text) {
    state.loading = false;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
