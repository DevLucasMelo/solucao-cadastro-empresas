<script setup>
    import { defineProps } from 'vue'

    const props = defineProps({
        estaLogado: Boolean,
        logout: Function
    })
</script>

<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light mb-4">
    <div class="container d-flex align-items-center justify-content-between">

      <!-- Marca (logo + texto) alinhada à esquerda -->
      <router-link class="navbar-brand d-flex align-items-center" to="/">
        <img src="/empresa.svg" alt="Logo" style="height: 50px; margin-right: 8px;" />
        <strong>Cadastro de Empresas</strong>
      </router-link>

      <!-- Botão toggler do menu (para mobile) -->
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Alternar navegação"
      >
        <span class="navbar-toggler-icon"></span>
      </button>

      <!-- Menu colapsável -->
      <div class="collapse navbar-collapse" id="navbarNav">

        <!-- Itens do menu alinhados à esquerda dentro do collapse -->
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <router-link class="nav-link" to="/">Pagina inicial</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/empresas">Cadastrar empresas</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/consultar-empresas">Consultar empresas</router-link>
          </li>
        </ul>

        <!-- Botões (login/logout) alinhados à direita -->
        <div class="d-flex">
          <router-link
            v-if="!props.estaLogado"
            class="btn btn-outline-secondary me-2"
            to="/cadastro"
          >
            Cadastrar Usuario
          </router-link>

          <router-link
            v-if="!props.estaLogado"
            class="btn btn-outline-primary me-2"
            to="/login"
          >
            Login
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
    </div>
  </nav>
</template>