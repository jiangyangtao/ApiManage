import index from '../components/Index';
import Login from '../components/account/Login';
import Register from '../components/account/Register';

const routes = [
  {
    path: '/',
    name: 'index',
    component: index,
  },
  {
    path: '/login',
    name: 'login',
    component: Login,
  },
  {
    path: '/register',
    name: 'register',
    component: Register,
  },
];

export default routes;
