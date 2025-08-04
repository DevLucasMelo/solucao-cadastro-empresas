<script setup>
import { useAuth } from './composables/useAuth.js'
import { useRouter } from 'vue-router'
import Navbar from './components/Navbar.vue'

const router = useRouter()
const { estaLogado, logoutUser } = useAuth()
</script>

<template>
  <div>
    <Navbar :estaLogado="estaLogado" :logout="logoutUser" />
    <main>
      <router-view />
    </main>
  </div>
</template>
