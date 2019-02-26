import LoadType from '../types/LoginFail';

const state = {
  failCount: 0,
};

const getters = {
  [LoadType._types.getters.failCount]: state => state.failCount,
};

const actions = {// 异步
  [LoadType._types.actions.logFail](store, text) {
    state.failCount += 1;
  },
};

const mutations = {// 同步
  [LoadType._types.mutations.logFail](state, text) {
    state.failCount += 1;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
