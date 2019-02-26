import cookie from 'js-cookie';
import axios from 'axios';
import store from './../../store';
import webConfig from '../config';


const devUrl = 'http://127.0.0.1/service';
const serverUrl = 'http://127.0.0.1/service';

const axiosInstance = axios.create({
  baseURL: serverUrl,
  headers: {
    'content-type': 'application/json',
  },
});

const server = {};
const communication = {};
const privateMembers = {};
server.baseURL = axiosInstance.defaults.baseURL;

const http = {
  get: (url, data, config) => new Promise((resolve, reject) => {
    axios.get(url, { params: data, config }).then((response) => {
      resolve(response.data);
    }).catch((error) => {
      reject(error);
    });
  }),
  post: (url, data, config) => new Promise((resolve, reject) => {
    axios.post(url, data, config).then((response) => {
      resolve(response.data);
    }).catch((error) => {
      reject(error);
    });
  }),
  delete: (url, data, config) => new Promise((resolve, reject) => {
    axios.delete(url, { params: data, config }).then((response) => {
      resolve(response.data);
    }).catch((error) => {
      reject(error);
    });
  }),
  put: (url, data, config) => new Promise((resolve, reject) => {
    axios.put(url, data, config).then((response) => {
      resolve(response.data);
    }).catch((error) => {
      reject(error);
    });
  }),
  patch: (url, data, config) => new Promise((resolve, reject) => {
    axios.patch(url, data, config).then((response) => {
      resolve(response.data);
    }).catch((error) => {
      reject(error);
    });
  }),
};

privateMembers.toFormData = function toFormData(data) {
  if (!data) return null;
  const formData = new FormData();
  for (const key in data) {
    formData.append(key, data[key]);
  }
  return formData;
};

privateMembers.tipError = function tipError(message) {
  // store.LoadStore.actions.hide();
  return privateMembers.Vue.prototype.$notify.error({
    title: '错误',
    message: message || '系统异常，请稍后再试！',
  });
};

// 请求拦截
// axiosInstance.interceptors.request.use((request) => {
//   /* eslint no-param-reassign: "error" */
//   if (!request.headers.common.authorization) request.headers.common.authorization = cookie.get('authorization');
//   return request;
// });

// Http 响应拦截，处理非200状态和异常的请求
axiosInstance.interceptors.response.use((response) => {
  if (response.status !== 200) {
    privateMembers.tipError();
  }
  return response;
}, (error) => {
  privateMembers.tipError();
  return error;
});

communication.install = function install(Vue, options) {
  /* eslint no-param-reassign: "error" */
  privateMembers.Vue = Vue;
  const api = webConfig.serviceApi;
  for (const key in api) {
    server[key] = {};
    const element = api[key];
    for (const subKey in element) {
      const item = element[subKey];
      if (item.method && item.url) {
        server[key][subKey] = (params, config) => new Promise((resolve, reject) => {
          let result = null;
          const method = item.method.toLowerCase();
          if (method === 'get' || method === 'delete') {
            result = axiosInstance[item.method](item.url, { params, config });
          } else {
            result = axiosInstance[item.method](item.url, config && config.paramsType === 'json' ? params : privateMembers.toFormData(params), config);
          }

          result.then((response) => {
            if (response.data) {
              if (response.data.Code === 0) resolve(response.data);
              else {
                if (!config || !config.notNotify) privateMembers.tipError(response.data.Message);
                reject(response.data.Message);
                // if (response.data.code >= 12000) {}
              }
            }
          }).catch((error) => {
            reject(error.message);
          });
        });
      }
    }
  }

  const authorization = cookie.get('authorization');
  if (authorization) axiosInstance.defaults.headers.common.authorization = authorization;
  Vue.prototype.$httpGet = http.get;
  Vue.prototype.$httpPost = http.post;
  Vue.prototype.$httpDelete = http.delete;
  Vue.prototype.$httpPut = http.put;
  Vue.prototype.$httpPatch = http.patch;
  Vue.prototype.$server = server;
};

export default communication;
