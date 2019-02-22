const notice = {};
const notification = {};

notification.buildConfiguration = function buildConfiguration(title, message, type) {
  const config = { title };
  if (type) config.type = type;
  if (message && typeof message === 'string') config.message = message;
  return config;
};

notification.install = function install(Vue, options) {
  /* eslint no-param-reassign: ["error", { "props": false }] */
  const notify = Vue.prototype.$notify;
  notice.info = function info(message) {
    const config = notification.buildConfiguration('提示', message);
    return notify.info(config);
  };

  notice.success = function success(message) {
    const config = notification.buildConfiguration('成功', message, 'success');
    return notify.success(config);
  };

  notice.warning = function warning(message) {
    const config = notification.buildConfiguration('警告', message, 'warning');
    return notify.warning(config);
  };

  notice.error = function error(message) {
    const config = notification.buildConfiguration('错误', message);
    return notify.error(config);
  };
  Vue.prototype.$notice = notice;
};

export default notification;
