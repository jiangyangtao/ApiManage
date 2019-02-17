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

privateMembers.tipError = function tipError(describe) {
  // store.LoadStore.actions.hide();
  // iView.Notice.error({
  //   title: '错误',
  //   desc: describe || '系统异常，请稍后再试！',
  //   duration: 4,
  // });
};

// 请求拦截
axiosInstance.interceptors.request.use((request) => {
  /* eslint no-param-reassign: "error" */
  const token = cookie.get('token');
  // if (!token)location.href = `${server.baseURL}/account/login`;
  request.headers.common.token = token;
  request.headers.common.authorization = cookie.get('authorization');
  return request;
});

// Http 响应拦截，处理非200状态和异常的请求
axiosInstance.interceptors.response.use((response) => {
  if (response.status !== 200) {
    privateMembers.tipError();
  }
  if (!response.headers.token) {
    privateMembers.tipError('异常操作');
  }
  cookie.set('token', response.headers.token);
  return response;
}, (error) => {
  privateMembers.tipError();
  return error;
});

communication.install = function install(Vue, options) {
  privateMembers.Vue = Vue;
  const api = webConfig.serviceApi;
  for (const key in api) {
    server[key] = {};
    const element = api[key];
    for (const item of element) {
      if (item.name && item.method && item.url) {
        server[key][item.name] = (params, config) => new Promise((resolve, reject) => {
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
                privateMembers.tipError(response.data.Data);
                reject(response.data.Data);
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

  // axiosInstance.defaults.headers.common.token = sessionStorage.getItem('token');
  // axiosInstance.defaults.headers.common.authorization = sessionStorage.getItem('authorization');
  Vue.prototype.$httpGet = http.get;
  Vue.prototype.$httpPost = http.post;
  Vue.prototype.$httpDelete = http.delete;
  Vue.prototype.$httpPut = http.put;
  Vue.prototype.$httpPatch = http.patch;
  Vue.prototype.$server = server;
};

export default communication;
