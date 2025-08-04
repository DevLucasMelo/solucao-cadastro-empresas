<script setup>
import { defineProps } from 'vue'

const props = defineProps({
  estaLogado: Boolean,
  logout: Function
})
</script>

<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light mb-4">
    <div class="container-fluid position-relative d-flex align-items-center justify-content-between flex-wrap">

      <router-link
        to="/"
        class="d-flex align-items-center me-3 flex-shrink-0"
        style="text-decoration: none; color: inherit; z-index: 2;"
      >
        <img src="/empresa.svg" alt="Logo" style="height: 50px; margin-right: 8px;" />
        <strong>Cadastro de Empresas</strong>
      </router-link>

      <ul
        class="navbar-nav d-flex flex-row flex-wrap justify-content-center mb-0"
        style="position: absolute; left: 0; right: 0; margin: 0 auto; width: 100%; z-index: 1;"
      >
        <li class="nav-item px-2">
          <router-link class="nav-link" to="/">Página inicial</router-link>
        </li>
        <li class="nav-item px-2">
          <router-link class="nav-link" to="/empresas">Cadastrar empresas</router-link>
        </li>
        <li class="nav-item px-2">
          <router-link class="nav-link" to="/consultar-empresas">Consultar empresas</router-link>
        </li>
      </ul>

      <div class="d-flex align-items-center flex-shrink-0" style="z-index: 2;">
        <router-link
          v-if="!props.estaLogado"
          class="btn btn-outline-secondary me-2"
          to="/cadastro"
        >
          Cadastrar Usuário
        </router-link>

        <router-link v-if="!props.estaLogado" class="btn btn-outline-primar me-2" to="/login"> Login
        </router-link>

        <button
          v-else
          class="btn btn-outline-danger"
          @click="props.logout"
        >
          Logout
        </button>
      </div>

    </div>
  </nav>
</template>
