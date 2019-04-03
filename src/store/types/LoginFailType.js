import removeNamespace from './namespacehandle';

const types = {
  getters: {
    failCount: 'LoginFail/failCount',
  },
  actions: {
    logFail: 'LoginFail/logFail',
  },
  mutations: {
    logFail: 'LoginFail/logFail',
  },
};

const _types = removeNamespace('LoginFail/', types);

export default {
  types,
  _types,
};
