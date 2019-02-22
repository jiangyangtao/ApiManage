import Common from './utility';
import Communication from './communication';
import Notification from './notification';
// import './extension';

const global = {
  Common,
};

global.install = function install(Vue, options) {
  // Common.install(Vue, options);
  Communication.install(Vue, options);
  Notification.install(Vue, options);
};

export default global;
