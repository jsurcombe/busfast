import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Home from '../views/Home.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/clusters',
    name: 'Clusters',
    component: () => import(/* webpackChunkName: "clusters" */ '../views/Clusters.vue')
  },
  {
    path: '/clusters/:id',
    name: 'Cluster',
    component: () => import(/* webpackChunkName: "cluster" */ '../views/Cluster.vue')
  },
  {
    path: '/services/:id',
    name: 'Service',
    component: () => import(/* webpackChunkName: "service" */ '../views/Service.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
