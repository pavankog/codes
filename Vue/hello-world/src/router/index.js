import { createRouter, createWebHistory } from 'vue-router'
import EmployeeDetails from '../views/EmployeeDetails.vue'

const routes = [
  {
    path: '/', 
    name: 'employeedetails',
    component: EmployeeDetails
  },
  {
    path: '/employee',
    name: 'employee',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Employee.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
