<script setup>
    import { ref, computed } from 'vue'
    import { useRouter } from 'vue-router'
    import Navbar from './components/Navbar.vue' 

    const router = useRouter()
    const token = ref(localStorage.getItem('token'))

    function logout() {
        localStorage.removeItem('token')
        token.value = null
        router.push('/login')
    }

    const estaLogado = computed(() => !!token.value)
</script>

<template>
    <div>
        <Navbar :estaLogado="estaLogado" :logout="logout" />

        <main>
            <router-view />
        </main>
    </div>
</template>
