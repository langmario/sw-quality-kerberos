import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Default from '../views/Default.vue'
import Languages from '../views/Languages.vue'
import Title from '../views/Title.vue'
import Salutation from '../views/Salutation.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Default',
    component: Default
  },
  {
    path: '/language',
    name: 'Languages',
    component: Languages
  },
  {
    path: '/salutation',
    name: 'Salutation',
    component: Salutation
  },
  {
    path: '/title',
    name: 'Title',
    component: Title
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
