import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Default from '../views/Default.vue'
import Language from '../views/Language.vue'
import Title from '../views/Title.vue'
import Salutation from '../views/Salutation.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Startseite',
    component: Default
  },
  {
    path: '/language',
    name: 'Sprache',
    component: Language
  },
  {
    path: '/salutation',
    name: 'Anrede',
    component: Salutation
  },
  {
    path: '/title',
    name: 'Titel',
    component: Title
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
