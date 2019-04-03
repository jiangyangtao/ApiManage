import removeNamespace from './namespacehandle';

const types = {
  getters: {
    loading: 'Loading/loading',
  },
  actions: {
    showLoading: 'Loading/showLoading',
    hideLoading: 'Loading/hideLoading',
  },
  mutations: {
    showLoading: 'Loading/showLoading',
    hideLoading: 'Loading/hideLoading',
  },
};

const _types = removeNamespace('Loading/', types);

export default {
  types,
  _types,
};
