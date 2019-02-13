import Common from './utility';
import Communication from './communication';
import notice from './notification';
// import './extension';

const global = {
  Common,
};

global.install = function install(Vue, options) {
  // Common.install(Vue, options);
  Communication.install(Vue, options);
  // notice.install(Vue, options);
};

export default global;
