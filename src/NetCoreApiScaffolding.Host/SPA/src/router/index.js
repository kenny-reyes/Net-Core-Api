import Vue from 'vue';
import Router from 'vue-router';

const MainLayout = () => import('@/views/MainLayout');
const Dashboard = () => import('@/views/sections/Dashboard');
const UserManager = () => import('@/views/sections/UserManager');
const SettingsView = () => import('@/views/sections/Settings');

Vue.use(Router);

function configRoutes() {
  return [
    {
      path: '/',
      name: 'Home',
      redirect: '/dashboard',
      component: MainLayout,
      children: [
        {
          path: 'dashboard',
          name: 'Dashboard',
          component: Dashboard,
          meta: {
            requiresAuth: true,
          },
        },
        {
          path: 'settings',
          name: 'Settings',
          component: SettingsView,
          meta: {
            requiresAuth: true,
          },
        },
        {
          path: '/users',
          name: 'User Manager',
          component: UserManager,
        },
      ],
    },
  ];
}

export default new Router({
  mode: 'history',
  linkActiveClass: 'active',
  scrollBehavior: () => ({ y: 0 }),
  routes: configRoutes(),
});
