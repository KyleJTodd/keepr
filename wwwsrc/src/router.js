import Vue from 'vue'
import Router from 'vue-router'
// @ts-ignore
import Home from './views/Home.vue'
// @ts-ignore
import Login from './views/Login.vue'
import Keeps from './views/Keeps.vue'
import Vault from './views/Vault.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/keeps',
      name: 'keeps',
      component: Keeps

    },
    {
      path: '/vault',
      name: 'vault',
      props: true,
      component: Vault
    }

  ]
})
