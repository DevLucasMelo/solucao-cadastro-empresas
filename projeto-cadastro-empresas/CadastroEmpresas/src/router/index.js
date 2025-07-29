import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Empresas from '../views/Empresas.vue'
import Login from '../views/Login.vue'
import CadastroUsuario from '../views/CadastroUsuario.vue' 
import ListagemEmpresas from '../views/ListagemEmpresas.vue'

const routes = [
    { path: '/', component: Home },
    { path: '/login', component: Login },
    {
        path: '/empresas',
        component: Empresas,
        meta: { requiresAuth: true }
    },
    {
        path: '/cadastro',
        component: CadastroUsuario
    },
    {
        path: '/consultar-empresas',
        component: ListagemEmpresas,
        meta: { requiresAuth: true }
    }

]

const router = createRouter({
    history: createWebHistory(),
    routes
})

router.beforeEach((to, from, next) => {
    const token = localStorage.getItem('token')
    if (to.meta.requiresAuth && !token) {
        next('/login')
    } else {
        next()
    }
})

export default router
